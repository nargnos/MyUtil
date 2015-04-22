/*
 * 一个轻量级的csv解析类,可以解析由excel输出的csv和符合RFC4180的csv
 * Unity可以使用
 * 使用CsvTable.Parse解析
 * 解析前需要确定文本为utf8编码
 * 解析后可以使用LINQ读取行列
 * csv转string的没写, 有时间再补
 * 
 * 最后更新时间 2015/4/22
 * NarGnos
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Csv
{
	public partial class CsvTable : IEnumerable<CsvRow>
    {
		public CsvTable(List<CsvRow> table, bool hasHeader)
        {
            if (table==null)
            {
                return;
            }
			table.ForEach(row => row.Father = this);// 为每一列设置标志防止混用
			
            // 对齐
            var maxColCount = table.Select(tmpRow => tmpRow.Count).Max();
            foreach (var tmpRow in table)
            {
                for (var delta = maxColCount - tmpRow.Count; delta>0 ; delta--)
                {
                    tmpRow.Add(string.Empty);
                }
            }
            // 取标题
            HeadIndexs = new Dictionary<string, int>();
            if (hasHeader)
            {
                if (table.Count>0)
                {
                    for (var i = 0; i < maxColCount; i++)
                    {
                        var tmpHead = table[0][i];
                        HeadIndexs.Add(string.IsNullOrEmpty(tmpHead) ? i.ToString() : tmpHead,i);
                    }
                }
                table.RemoveAt(0);
            }
            else
            {
                for (var i = 0; i < maxColCount; i++)
                {
                    HeadIndexs.Add(i.ToString(),i);
                }
            }
            this.table = table;
            this.RowCount = table.Count;
            this.ColCount = maxColCount;
        }

		private readonly List<CsvRow> table;
		
		public List<CsvRow> Table
        {
            get { return table; }
        }

        public Dictionary<string, int> HeadIndexs { get; private set; }

        public int RowCount
        {
            get;
			private set;
        }
        public int ColCount
        {
            get;
            private set;
        }

		/// <summary>
		/// 返回行
		/// </summary>
		/// <param name="index">行号,以0起始</param>
		/// <returns>行内容</returns>
		public CsvRow this[int index]
		{
			get { return table[index]; }
		}
		public CsvRow GetRow(int index)
		{
			return this[index];
		}
		/// <summary>
		/// 返回指定行列的内容
		/// </summary>
		/// <param name="index">行号,以0起始</param>
		/// <param name="colName">列名</param>
		/// <returns>字段内容</returns>
		public string this[int index, string colName]
		{
			get
			{
				return this[index][HeadIndexs[colName]];
			}
		}
		public string GetField(int rowIndex, string colName)
		{
			return this[rowIndex, colName];
		}

		/// <summary>
		/// 返回指定坐标的字段
		/// </summary>
		/// <param name="row">行坐标,以0起始</param>
		/// <param name="col">列坐标,以0起始</param>
		/// <returns>字段内容</returns>

		public string this[int row, int col]
		{
			get { return table[row][col]; }
		}
		public string GetField(int row, int col)
		{
			return this[row, col];
		}
		/// <summary>
		/// 返回列满足条件的整行
		/// </summary>
		/// <param name="colName">列名</param>
		/// <param name="str">搜索值</param>
		/// <returns>行内容,找不到返回null</returns>
		public CsvRow this[string colName, string str]
		{
			get
			{
				try
				{
					var colIndex = HeadIndexs[colName];
					return table.Find((list) => list[colIndex] == str);
				}
				catch
				{
					return null;
				}
			}
		}
		
		internal string GetField(string colName, CsvRow row)
		{
			if (row.Father != this)
			{
				throw new Exception("传入了非该表的行");
			}
			return row[HeadIndexs[colName]];
		}

		public CsvRow GetRow(string colName, string str)
		{
			return this[colName, str];
		}

		IEnumerator<CsvRow> IEnumerable<CsvRow>.GetEnumerator()
		{
			return table.GetEnumerator();
		}



		IEnumerator IEnumerable.GetEnumerator()
		{
			return table.GetEnumerator();
		}

		// 输出csv string
		public override string ToString()
		{
			// TODO: csv转string
			return base.ToString();
		}

	}
}
