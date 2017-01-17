using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRowConvert
{
    // 这个方式比直接写文本代码需要写的还多
    public static class ConvertorCodeDom
    {
        public static object GenerateConvertor<TResult>()
           where TResult : new()
        {
            var ccu = new CodeCompileUnit();
            var resultType = typeof(TResult);

            ccu.ReferencedAssemblies.AddRange(GetReferencedAssemblies(resultType).ToArray());

            var ns = new CodeNamespace();
            ns.Imports.AddRange(GetNamespaces(resultType).ToArray());
            ccu.Namespaces.Add(ns);

            var classDef = CreateClass();
            ns.Types.Add(classDef);
            classDef.Members.Add(CreateFillFunc(resultType));
            classDef.Members.Add(CreateReadRowFunc(resultType));
            return CreateInstance(ccu);
        }

        private static object CreateInstance(CodeCompileUnit ccu)
        {            
            //var pvd = CodeDomProvider.CreateProvider("Cpp");
            //var ms = new MemoryStream();
            //var sw = new StreamWriter(ms);
            //sw.AutoFlush = true;
            //pvd.GenerateCodeFromCompileUnit(ccu, sw, new CodeGeneratorOptions());
            
            //ms.Seek(0, SeekOrigin.Begin);
            //var sr = new StreamReader(ms);
            //var code = sr.ReadToEnd();

            var cmp = provider.CompileAssemblyFromDom(new CompilerParameters() { GenerateInMemory = true }, ccu);
            var assembly = cmp.CompiledAssembly;
            return assembly.CreateInstance(className_);
        }

        private static IEnumerable<string> GetReferencedAssemblies(Type resultType)
        {
            return refAssemblyNames.Concat(
                from item in resultType.Assembly.Modules
                select item.Name).Concat(
                from item in resultType.GetProperties().SelectMany((prop) => prop.PropertyType.Assembly.Modules)
                select item.Name
                ).Distinct();
        }

        private static IEnumerable<CodeNamespaceImport> GetNamespaces(Type resultType)
        {
            var result = new List<CodeNamespaceImport>();

            result.Add(new CodeNamespaceImport(resultType.Namespace));
            result.Add(new CodeNamespaceImport(typeof(DataRow).Namespace));
            result.Add(new CodeNamespaceImport(typeof(DataRowExtensions).Namespace));
            result.AddRange(from item in resultType.GetProperties() select new CodeNamespaceImport(item.PropertyType.Namespace));
            return result.Distinct();
        }

        private static CodeTypeDeclaration CreateClass()
        {
            return new CodeTypeDeclaration(className_)
            {
                IsStruct = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
        }


        private static CodeMemberMethod CreateFillFunc(Type resultType)
        {
            var result = new CodeMemberMethod()
            {
                Name = "Fill",
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                ReturnType = new CodeTypeReference(typeof(void))
            };
            var rowName = "row";
            var objName = "obj";

            result.Parameters.Add(new CodeParameterDeclarationExpression(typeof(DataRow), rowName));
            result.Parameters.Add(new CodeParameterDeclarationExpression(resultType, objName));

            var paramRow = new CodeArgumentReferenceExpression(rowName);
            var paramObj = new CodeArgumentReferenceExpression(objName);

            var assigns = from info in GetConvertInfo(resultType)
                          let invokeMtd = new CodeMethodReferenceExpression(paramRow, "SetField", new CodeTypeReference(info.Item3))
                          let code = new CodeMethodInvokeExpression(invokeMtd,
                          new CodePrimitiveExpression(info.Item1),
                          new CodePropertyReferenceExpression(paramObj, info.Item2)
                          )
                          select code;

            foreach (var item in assigns)
            {
                result.Statements.Add(item);
            }

            return result;
        }
        private static CodeMemberMethod CreateReadRowFunc(Type resultType)
        {
            var result = new CodeMemberMethod()
            {
                Name = "Convert",
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                ReturnType = new CodeTypeReference(resultType)
            };
            var paramRowName = "row";
            result.Parameters.Add(new CodeParameterDeclarationExpression(typeof(DataRow), paramRowName));
            var varName = "result";
            var v = new CodeVariableDeclarationStatement(resultType, varName);
            result.Statements.Add(v);
            var varRef = new CodeVariableReferenceExpression(varName);
            var newVar = new CodeAssignStatement(varRef, new CodeObjectCreateExpression(resultType));
            var paramRow = new CodeArgumentReferenceExpression(paramRowName);
            result.Statements.Add(newVar);

            var assigns = from info in GetConvertInfo(resultType)
                          let prop = new CodePropertyReferenceExpression(varRef, info.Item2)
                          let invokeMtd = new CodeMethodReferenceExpression(paramRow, "Field", new CodeTypeReference(info.Item3))
                          let code = new CodeMethodInvokeExpression(invokeMtd, new CodePrimitiveExpression(info.Item1))
                          select new CodeAssignStatement(prop, code);

            foreach (var item in assigns)
            {
                result.Statements.Add(item);
            }
            result.Statements.Add(new CodeMethodReturnStatement(varRef));
            return result;
        }
        // 列名、字段名、类型
        internal static List<Tuple<string, string, Type>> GetConvertInfo(Type resultType)
        {
            return (from property in resultType.GetProperties()
                    let convertFields = ConvertorExpression.GetConvertFieldAttrs(property)
                    where ConvertorExpression.CheckConvertFieldAttr(convertFields)
                    select new Tuple<string, string, Type>(
                        ConvertorExpression.GetColName(convertFields),
                        property.Name,
                        property.PropertyType
                    )).ToList();
        }
        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        private static string className_ = "Ignore";
        private static string[] refAssemblyNames =
        {
            "System.dll",
            typeof(DataRow).Module.Name,
            typeof(DataRowExtensions).Module.Name
        };
    }
}
