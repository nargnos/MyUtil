using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataRowConvert
{
    // 第二种方法转换对象，动态生成代码编译，也是编译成固定的代码
    // 这种方法无论如何最后都要用dynamic或反射，不比表达式树的方法方便
    // Emit IL 的太麻烦不写了, 找了一圈没有找到把lambda或表达式树打包成hex的方法，看来如果需要动态创建代码发给别的程序执行还需要用这个方式
    public static class AssemblyGenerator
    {
        private static string className = "Convertor";
        private static string usingStr = "$Using$";
        private static string fillRowStr = "$FillRow$";
        private static string readRowStr = "$ReadRow$";
        private static string resultTypeStr = "$ResultType$";
        private static string columnNameStr = "$ColumnName$";
        private static string typeStr = "$Type$";
        private static string fieldNameStr = "$FieldName$";

        private static string fillRowTemplate = $"row.SetField<{typeStr}>(\"{columnNameStr}\", obj.{fieldNameStr});";
        private static string readRowTemplate = $"result.{fieldNameStr} = row.Field<{typeStr}>(\"{columnNameStr}\");";

        private static string codeTemplate = $@"
using System; 
using System.Data;
{usingStr}
public class {className}
{{
    public void Fill(DataRow row, {resultTypeStr} obj)
    {{
        // FillRow
        {fillRowStr}
    }}
    public {resultTypeStr} Convert(DataRow row)
    {{
        var result = new {resultTypeStr}();
        // ReadRow
        {readRowStr}
        return result;
    }}
}}
";

        private static CSharpCodeProvider provider = new CSharpCodeProvider();
        private static string[] refAssemblyNames =
        {
            "System.dll",
            typeof(DataRow).Module.Name,
            typeof(DataRowExtensions).Module.Name
        };
        // 返回对象可使用
        // void Fill(DataRow row, TResult obj)
        // TResult Convert(DataRow row)
        public static dynamic GenerateConvertor<TResult>()
           where TResult : new()
        {
            var resultType = typeof(TResult);
            if (!resultType.IsPublic)
            {
                throw new ArgumentException();
            }
            var parameters = CreateParameters(resultType.Assembly);

            var usingCode = GetUsingCode(resultType);

            var convertInfo = GetConvertInfo(resultType);
            var fillRowCode = GetFillRowCode(convertInfo);
            var readRowCode = GetReadRowCode(convertInfo);
            var code = GenerateCode(resultType.Name, usingCode, fillRowCode, readRowCode);

            var ret = provider.CompileAssemblyFromSource(parameters, code);
            if (ret.Errors.Count == 0)
            {
                return ret.CompiledAssembly.CreateInstance(className);
            }
            throw new NotSupportedException();
        }

        private static string GetUsingCode(Type resultType)
        {
            return string.IsNullOrEmpty(resultType.Namespace) ? string.Empty : $"using { resultType.Namespace};";
        }

        private static CompilerParameters CreateParameters(params Assembly[] assemblies)
        {
            var result = new CompilerParameters(refAssemblyNames)
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false
            };
            var names = from item in assemblies.SelectMany((asm) => asm.Modules)
                        select item.Name;
            result.ReferencedAssemblies.AddRange(names.ToArray());
            return result;
        }
        private static string GenerateCode(string resultTypeName, string usingCode, string fillRowCode, string readRowCode)
        {
            return codeTemplate.Replace(usingStr, usingCode).
                Replace(resultTypeStr, resultTypeName).
                Replace(fillRowStr, fillRowCode).
                Replace(readRowStr, readRowCode);
        }

        private static List<Tuple<string, string, string>> GetConvertInfo(Type resultType)
        {
            // 列名、字段名、类型
            return (from property in resultType.GetProperties()
                    let convertFields = ConvertorEmit.GetConvertFieldAttrs(property)
                    where ConvertorEmit.CheckConvertFieldAttr(convertFields)
                    select new Tuple<string, string, string>(
                        ConvertorEmit.GetColName(convertFields),
                        property.Name,
                        GetTypeName(property.PropertyType)
                    )).ToList();
        }

        private static string GetTypeName(Type type)
        {
            var result = type.Name;
            if (result.StartsWith("Nullable") && type.IsGenericType && type.GenericTypeArguments.Count() == 1)
            {
                result = $"{type.GenericTypeArguments.First().Name }?";
            }
            return result;
        }
        private static string GetReadRowCode(List<Tuple<string, string, string>> convertInfo)
        {
            // result.fieldName = row.Field<type>(colName);
            return string.Join(Environment.NewLine + "\t",
               from item in convertInfo
               select ReplaceStr(readRowTemplate, item));
        }
        private static string GetFillRowCode(List<Tuple<string, string, string>> convertInfo)
        {
            // row.SetField<type>(obj.fieldName,obj.filename);
            return string.Join(Environment.NewLine + "\t",
                from item in convertInfo
                select ReplaceStr(fillRowTemplate, item));
        }
        private static string ReplaceStr(string template, Tuple<string, string, string> info)
        {
            return template.Replace(columnNameStr, info.Item1).Replace(fieldNameStr, info.Item2).Replace(typeStr, info.Item3);
        }
    }



}
