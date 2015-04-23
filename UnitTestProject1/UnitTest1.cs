using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;

namespace Csv
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = File.ReadAllText(@"");
            var csv = CsvTable.Parse(text,true);
            var s1 = csv[0];
            var s2 = csv[1, 0].AsType<int>();
            var s3 = csv[0, "NAME"];
            var s4 = csv["ID", "10000"];

            var str = csv.ToString();
            Debug.WriteLine(str);
        }
    }
}
