﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataRowConvert
{
    // 注意转换时成员要声明成public
    public static class ConvertorExpression
    {
        // 对应目标的映射关系
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class ConvertFieldAttribute : Attribute
        {
            readonly string fieldName_;
            public ConvertFieldAttribute(string fieldName)
            {
                fieldName_ = fieldName;
            }

            public string ColumnName
            {
                get { return fieldName_; }
            }
        }
        public static Action<DataRow, TResult> EmitFillRow<TResult>()
        {
            var typeResult = typeof(TResult);
            var typeDataRow = typeof(DataRow);

            var row = Expression.Parameter(typeDataRow, "row");
            var obj = Expression.Parameter(typeResult, "obj");

            // row.SetField(colName, obj.field)
            var assigns = from property in typeResult.GetProperties()
                          let convertFields = GetConvertFieldAttrs(property)
                          where CheckConvertFieldAttr(convertFields)
                          let columnName = GetColName(convertFields)
                          let field = GetProp(obj, property)
                          select AssignRowField(row, columnName, field, property.PropertyType);

            return Expression.Lambda<Action<DataRow, TResult>>(Expression.Block(assigns), row, obj).Compile();
        }

        private static MethodCallExpression AssignRowField(ParameterExpression paramRow, string columnName, Expression value, Type valueType)
        {
            return Expression.Call(typeof(DataRowExtensions), "SetField", new Type[] { valueType }, paramRow, MakeConstStr(columnName), value);
        }

        // 生成转换函数, 需要预先定义列名
        public static Func<DataRow, TResult> EmitRowConvert<TResult>()
            where TResult : new()
        {
            var typeResult = typeof(TResult);
            var typeDataRow = typeof(DataRow);

            var param = Expression.Parameter(typeDataRow, "row");
            // new TResult()
            var newObj = Expression.New(typeResult);
            // TResult ret
            var result = Expression.Variable(typeResult, "ret");
            // TResult ret = new TResult();
            var initResult = Expression.Assign(result, newObj);
            // ret.Field(columnName)
            var assigns = from property in typeResult.GetProperties()
                          let convertFields = GetConvertFieldAttrs(property)
                          where CheckConvertFieldAttr(convertFields)
                          let columnName = GetColName(convertFields)
                          let field = GetProp(result, property)
                          let fieldValue = GetRowField(param, columnName, property.PropertyType)
                          select Expression.Assign(field, fieldValue);
            var blocks = Expression.Block(
                new ParameterExpression[] { result },
                new Expression[] { initResult }.Concat(assigns).Concat(new Expression[] { result }));

            return Expression.Lambda<Func<DataRow, TResult>>(blocks, param).Compile();
        }
        // 读Reader, 跟其它有冗余，之后在改
        public static Func<IDataRecord, TResult> EmitDataRecordConvert<TResult>()
            where TResult : new()
        {
            var typeResult = typeof(TResult);
            var typeRecord = typeof(IDataRecord);
            var param = Expression.Parameter(typeRecord, "row");
            var newObj = Expression.New(typeResult);
            var result = Expression.Variable(typeResult, "ret");
            var initResult = Expression.Assign(result, newObj);
            var assigns = from property in typeResult.GetProperties()
                          let convertFields = GetConvertFieldAttrs(property)
                          where CheckConvertFieldAttr(convertFields)
                          let columnName = GetColName(convertFields)
                          let field = GetProp(result, property)
                          let fieldValue = GetReaderField(param, columnName, property.PropertyType)
                          select Expression.Assign(field, fieldValue);
            var blocks = Expression.Block(
                new ParameterExpression[] { result },
                new Expression[] { initResult }.Concat(assigns).Concat(new Expression[] { result }));
            return Expression.Lambda<Func<IDataRecord, TResult>>(blocks, param).Compile();
        }
        private static UnaryExpression GetReaderField(ParameterExpression rec, string columnName, Type valueType)
        {
            var prop = Expression.Property(rec, "Item", MakeConstStr(columnName));
            return Expression.Convert(prop, valueType);
        }
        private static MethodCallExpression GetRowField(ParameterExpression paramRow, string columnName, Type valueType)
        {
            return Expression.Call(typeof(DataRowExtensions), "Field", new Type[] { valueType }, paramRow, MakeConstStr(columnName));
        }

        private static ConstantExpression MakeConstStr(string str)
        {
            return Expression.Constant(str, typeof(string));
        }

        private static MemberExpression GetProp(ParameterExpression result, PropertyInfo property)
        {
            return Expression.Property(result, property.Name);
        }

        internal static string GetColName(object[] convertFields)
        {
            return (convertFields.First() as ConvertFieldAttribute).ColumnName;
        }

        internal static bool CheckConvertFieldAttr(object[] convertFields)
        {
            return convertFields.Count() == 1 && convertFields.First() is ConvertFieldAttribute;
        }

        internal static object[] GetConvertFieldAttrs(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ConvertFieldAttribute), true);
        }

    }
}
