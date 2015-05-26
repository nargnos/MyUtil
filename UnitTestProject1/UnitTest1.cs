using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using Fill;
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



        #region 测试用结构
        public struct MyStruct
        {
            public static int ff;
            public int a;
            private int myVar;

            public int MyProperty
            {
                get { return myVar; }
                set { myVar = value; }
            }
            public void m()
            { }
        }
        public enum MyEnum
        {
            A, B, C
        }
        public class A<T>
        {
            public int Item22 { get; set; }
            private int gg = 1;
            public T TT { get; set; }
            public int AS { get { return gg; } }
            public int this[int i]
            {
                get { return gg; }
                set { gg = value; }
            }
            public double this[int i, string k]
            {
                //get { return gg; }
                set { gg = (int)value; }
            }
            public string XX { get; set; }
            public int a;
            public MyEnum ss;
        }
        public class MyClass
        {
            public MyClass(int a)
            {
                b = a;
            }
            private int b;
            public int C { get; set; }
        }
        long tmpCount = 0;
        private void Set(ref object val)
        {
           
           val=tmpCount++;
        }
        
#endregion
        [TestMethod]
        public void TestFill()
        {

            var re = new RandomEngine();
            // 创建空类型
            var create0 = typeof(HashSet<string>).CreateInstance(new TypeCreator());
            // 创建结构(有时候会key重复...)
            //re.Setter.SetRandomSetter(typeof(SortedList<byte, Int64>), SetSortedList);
            var create1 = re.CreateRandomInstance(typeof(SortedList<byte,Int64>));

            // 类型测试
            var type0 = typeof(MyStruct).CreateTestInstance(re);
            var type1 = typeof(MyEnum).CreateTestInstance(re);
            var type2 = typeof(bool).CreateTestInstance(re);
            var type3 = typeof(string).CreateTestInstance(re);
            var type4 = typeof(int).CreateTestInstance(re);
            var type5 = typeof(decimal).CreateTestInstance(re);
            var type6 = typeof(int[]).CreateTestInstance(re);

            
           // var type7 = typeof(Tuple<int>).CreateTestInstance(re); // 元组不支持

            var type8 = typeof(char).CreateTestInstance(re);

            // 带参构造函数(可先实例化好或设置创建器)
            re.Creator.SetTypeCtorParam(typeof(MyClass), 1);
            var type9 = typeof(MyClass).CreateTestInstance(re);

            var type10 = re.CreateRandomInstance<Stack<int>>(); // Stack不支持待改进
            // 赋值已有类型
            object classTest = new A<int>();
           
            var ct = classTest.GetType().CreateTestInstance(re);
            // 设置构造器
            re.Creator.DefaultArrayLength = 2;
            // 复杂类型测试
            var collectionTest = typeof(Dictionary<List<byte[, , , ,]>, string>).CreateTestInstance(re);

            // 直接调用随机器设置变量
            object structTest = new MyStruct();
            re.SetRandomValue(ref structTest);

            
            object longLinkedList = new LinkedList<long>();// LinkedList不支持
            re.SetRandomValue(ref longLinkedList);

            // 设置随机方法干预随机过程
            var setter = new RandomSetter.Setter(Set);
            re.Setter.SetRandomSetter(typeof(long), setter);

            object longArray = new long[10];
            re.SetRandomValue(ref longArray);

            object array = new int[2, 3];
            re.SetRandomValue(ref array);
            object array2 = new int[2][];
            re.SetRandomValue(ref array);

        }
    }
}
