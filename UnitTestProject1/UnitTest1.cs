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
        public void TestCSV()
        {
            var text = File.ReadAllText(@"path");
            var csv = CsvTable.Parse(text, true);
            var s1 = csv[0];
            var s2 = csv[1, 0].AsType<int>();
            var s3 = csv[0, "NAME"];
            var s4 = csv["ID", "10000"];

            var str = csv.ToString();
            Debug.WriteLine(str);
        }

        [TestMethod]
        public void TestTTree()
        {
            // 字符串
            var strTree = new TireTree.TTree<char, int>();
            var cb = new Func<int, int, int>((oldVal, newVal) => oldVal + 1);
            strTree.Add("HelloWorld!", 1, cb);
            strTree.Add("HelloCSharp!", 1, cb);
            strTree.Add("HelloWorld!", 1, cb);
            strTree.Add("Hi!", 1, cb);
            strTree.CreateNode("TireTree").Value = 4;

            var find = strTree.FindNode("HelloWorld!");
            Debug.Assert(find != null && find.HasValue);
            Debug.Assert(find.Value == 2);

            find = strTree.FindNode("TireTree");
            Debug.Assert(find.Value == 4);
            // 枚举当前节点的下一个路径
            foreach (var item in strTree.ChildrenKeys)
            {
                Debug.WriteLine(item);
            }
            Debug.Assert(strTree.Children.Count == 2);
            // 搜索到的最长路径
            List<char> output;
            strTree.FindNode("HelloWWW", out output);
            Debug.WriteLine(new string(output.ToArray()));
            // 错误输入
            strTree.FindNode(string.Empty, out output);
            Debug.Assert(output == null);
            strTree.FindNode(null, out output);
            Debug.Assert(output == null);
            strTree.FindNode("Cant find", out output);
            Debug.Assert(output == null);
            strTree.FindNode("H", out output);
            Debug.Assert(output.Count == 1 && output[0] == 'H');
            Debug.Assert(strTree.FindNode(null) == null);
            Debug.Assert(strTree.FindNode("HelloX") == null);
            // 无节点
            var node = strTree.FindNode("Cant find");
            Debug.Assert(node == null);
            // 有节点无值
            node = strTree.FindNode("H");
            Debug.Assert(node.HasValue == false);
            // 其它对象
            var objTree = new TireTree.TTree<ConsoleColor, Action>();

            var key = new List<ConsoleColor>() { ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Cyan };
            objTree.Add(key, () => Debug.WriteLine("Cyan-DarkCyan-Cyan"));

            var key2 = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Green };
            objTree.CreateNode(key2).Value = () => Debug.WriteLine("Red-Green");


            var finder = objTree.CreateFinder();
            finder.SearchNext(ConsoleColor.Red);
            finder.SearchNext(ConsoleColor.Green);
            finder.CurrentNode.Value();
            Debug.Assert(finder.CurrentNode.Children == null);
            Debug.Assert(finder.CurrentNode.ChildrenKeys == null);

            Debug.Assert(finder.SearchNext(ConsoleColor.Cyan) == false);
            Debug.Assert(finder.SearchNext(ConsoleColor.Gray) == false);
            Debug.Assert(finder.CurrentElement == null);
            foreach (var item in finder.ValidKeys)
            {
                Debug.WriteLine(item);
            }
            // 其它
            objTree.Add(null, null);
            try
            {
                objTree.Add(key, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finder.Reset();
            objTree.Clear();
            /*是不是写错了什么覆盖率不到100%....*/
        }
    }
}
