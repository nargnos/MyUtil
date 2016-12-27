using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;
using DataRowConvert;
using System.Diagnostics;
using System.Collections.Generic;

namespace DataRowConvertTest
{
    public struct MyStruct
    {
        [ConvertorEmit.ConvertField("ID")]
        public int? ID { get; set; }
        [ConvertorEmit.ConvertField("Value")]
        public string Text { get; set; }
        public int? Ignore { get; set; }
        // 可以设置多变量接收数据，作为赋值到row的话会保留最后一个重复数据
        [ConvertorEmit.ConvertField("Value")]
        public string TextCopy { get; set; }
    }
    public struct MyStruct2
    {
        [ConvertorEmit.ConvertField("ID")]
        public int? A { get; set; }
        [ConvertorEmit.ConvertField("Value")]
        public string B { get; set; }
        // [ConvertorEmit.ConvertField("Value")]
        public string C { get; set; }
    }
    [TestClass]
    public class DataRowConvertTest
    {
        [TestMethod]
        public void TestEmitConvertor()
        {
            var rowToStruct = ConvertorEmit.EmitRowConvert<MyStruct>();
            var fillRow = ConvertorEmit.EmitFillRow<MyStruct2>();

            DataTable tb = new DataTable();
            tb.Columns.Add("ID", typeof(int));
            tb.Columns.Add("Value", typeof(string));

            List<MyStruct2> addList = new List<MyStruct2>()
            {
                new MyStruct2() { A=null, B="================", C=null },
                new MyStruct2() { A=99999, B="================", C="ignore" },
                new MyStruct2() { A=00, B="Hello", C="ignore" },
                new MyStruct2() { A=2, B="!!", C="ignore" },
                new MyStruct2() { A=1, B="World", C="ignore" },
            };

            int addSum = 0;
            // 不同结构不同字段只要设置特性就可以互用
            foreach (var item in addList)
            {
                addSum += item.A.GetValueOrDefault();
                var row = tb.NewRow();
                fillRow(row, item);
                tb.Rows.Add(row);
            }

            var objs = from DataRow row in tb.Rows let obj = rowToStruct(row) orderby obj.ID select obj;
            var sum = objs.Sum((obj) => obj.ID.GetValueOrDefault());
            Assert.IsTrue(addSum == sum);
            var output = string.Join(" ", from text in objs select text.Text);

            Debug.WriteLine(output);
        }

        [TestMethod]
        public void TestGenerateAssembly()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ID", typeof(int));
            tb.Columns.Add("Value", typeof(string));
            tb.Rows.Add(1, "2");
            tb.Rows.Add(3, "4");

            var convertor = AssemblyGenerator.GenerateConvertor<MyStruct2>();
            var s = (MyStruct2)convertor.Convert(tb.Rows[0]);
            convertor.Fill(tb.Rows[1], s);
            Assert.IsTrue(tb.Rows[1].Field<int?>("ID")== s.A);
            Assert.AreEqual(tb.Rows[1].Field<string>("Value"),s.B);

            var convertor2 = AssemblyGenerator.GenerateConvertor<MyStruct>();
            var s2 = (MyStruct)convertor2.Convert(tb.Rows[0]);
            Assert.IsTrue(tb.Rows[0].Field<int?>("ID") == s2.ID);
            Assert.AreEqual(tb.Rows[0].Field<string>("Value"), s2.Text);
        }
    }
}
