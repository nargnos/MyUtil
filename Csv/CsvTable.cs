/*
 * 一个轻量级的csv解析类,可以解析由excel输出的和符合RFC4180的csv,可把自身结构转成csv
 * Unity可以使用
 * 使用CsvTable.Parse解析
 * 解析前需要确定文本为utf8编码
 * 解析后可以使用LINQ读取行列
 * 
 * 
 * 最后更新时间 2015/4/23
 * NarGnos
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csv
{
    public partial class CsvTable: IEnumerable<CsvRow>
    {
        private readonly bool hasHeader;
        // private readonly Dictionary<string, int> headerIndexs = new Dictionary<string, int>();
        private readonly List<CsvRow> table;

        public CsvTable(List<CsvRow> table, bool hasHeader)
        {
            if (table == null)
            {
                return;
            }
            table.ForEach(row => row.Father = this); // 为每一列设置标志防止混用

            // 对齐
            var maxColCount = table.Select(tmpRow => tmpRow.Count).Max();
            foreach (var tmpRow in table)
            {
                for (var delta = maxColCount - tmpRow.Count; delta > 0; delta--)
                {
                    tmpRow.Add(string.Empty);
                }
            }
            this.hasHeader = hasHeader;
            // 取标题
            if (hasHeader)
            {
                if (table.Count > 0)
                {
                    Header = table.First();
                    table.RemoveAt(0);
                }
            }

            this.table = table;
            RowCount = table.Count;
            ColCount = maxColCount;
        }

        public CsvRow Header { get; private set; }

        public List<CsvRow> Table
        {
            get { return table; }
        }

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        /// <summary>
        ///     返回行
        /// </summary>
        /// <param name="index">行号,以0起始</param>
        /// <returns>行内容</returns>
        public CsvRow this[int index]
        {
            get { return table[index]; }
        }

        /// <summary>
        ///     返回指定行列的内容
        /// </summary>
        /// <param name="index">行号,以0起始</param>
        /// <param name="colName">列名</param>
        /// <returns>字段内容</returns>
        public string this[int index, string colName]
        {
            get
            {
                var tmpIndex = FindHeader(colName);
                if (tmpIndex < 0)
                {
                    return null;
                }
                return this[index][tmpIndex];
            }
            set {
                var tmpIndex = FindHeader(colName);
                if (tmpIndex < 0)
                {
                    throw new ArgumentException("找不到该列");
                }
                this[index][tmpIndex] = value;
            }
        }

        /// <summary>
        ///     返回指定坐标的字段
        /// </summary>
        /// <param name="row">行坐标,以0起始</param>
        /// <param name="col">列坐标,以0起始</param>
        /// <returns>字段内容</returns>
        public string this[int row, int col]
        {
            get { return table[row][col]; }
            set { table[row][col] = value; }
        }

        /// <summary>
        ///     返回列满足条件的整行
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="str">搜索值</param>
        /// <returns>行内容,找不到返回null</returns>
        public CsvRow this[string colName, string str]
        {
            get
            {
                var tmpIndex = FindHeader(colName);
                return tmpIndex < 0 ? null : table.Find(list => list[tmpIndex] == str);
            }
        }

        IEnumerator<CsvRow> IEnumerable<CsvRow>.GetEnumerator()
        {
            return table.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return table.GetEnumerator();
        }

        public CsvRow GetRow(int index)
        {
            return this[index];
        }

        // 只匹配第一个colName
        private int FindHeader(string colName)
        {
            if (!hasHeader)
            {
                throw new ArgumentException("该表没有表头");
            }
            return Header.IndexOf(colName); ;
        }

        public string GetField(int rowIndex, string colName)
        {
            return this[rowIndex, colName];
        }

        public string GetField(int row, int col)
        {
            return this[row, col];
        }

        internal string GetField(string colName, CsvRow row)
        {
            if (row.Father != this)
            {
                throw new Exception("传入了非该表的行");
            }
            var tmpIndex = FindHeader(colName);
            return tmpIndex < 0 ? null : row[tmpIndex];
        }

        public CsvRow GetRow(string colName, string str)
        {
            return this[colName, str];
        }

        // 输出csv string
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (hasHeader)
            {
                AddRow(sb, Header);
            }
            table.ForEach(row => AddRow(sb, row));
            sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            return sb.ToString();
        }

        private void AddRow(StringBuilder sb, CsvRow row)
        {
            foreach (var field in row)
            {
                sb.AppendFormat("{0},", FormatField(field));
            }
            sb.Replace(",", Environment.NewLine, sb.Length - 1, 1);
        }

        private static string FormatField(string field)
        {
            if (conditions.Keys.Any(key => field.Contains(key)))
            {
                field = "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }
    }
}