using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Csv
{
    public partial class CsvTable
    {
        private static readonly int[,] statusMap =
        {
            {4,2,0,1,7},
            {1,2,3,1,7},
            {4,2,3,1,7},
            {4,2,0,1,7},
            {6,5,5,5,8},
            {6,5,5,5,8},
            {5,2,3,8,7}
        };

        private static readonly Dictionary<char, int> conditions = new Dictionary<char, int>()
        {
            {'"', 0},
            {',', 1},
            {'\r', 2},
            {'\n', 2}
            // 其它字符为3
            // EOF为4
        };
		private delegate void Function(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat);

		private static readonly Function[] actionTable =  
		{
			DoNothing, // 0
			Function1, // 1
			Function2, // 2
			Function3, // 3
			DoNothing, // 4
			Function5, // 5
			DoNothing, // 6
			Function7, // 7
			Function8  // 8
		}; 

        public static CsvTable Parse(string csvStr, bool hasHead)
        {
            var stat = 0;
            var data = csvStr;
            var index = 0;
			var table = new List<CsvRow>();
            var tmpRow = new CsvRow();
            var tmpField = new StringBuilder();
            while (index <= data.Length && stat != 7 && stat != 8)
            {
                int tmpCondition;
                var cr = '\0';
                if (index < data.Length)
                {
                    cr = data[index++];
                    tmpCondition = conditions.ContainsKey(cr) ? conditions[cr] : 3;
                }
                else
                {
                    tmpCondition = 4;
                }
                var preStat = stat;
                stat = statusMap[stat, tmpCondition];
                // 处理stat
                actionTable[stat](table, ref tmpRow, tmpField, cr, preStat);
            }
            return new CsvTable(table, hasHead);
        }

		
		private static void DoNothing(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat) { }

		private static void Function1(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
			// 读取一项的一个字符
            tmpField.Append(cr);
        }

		private static void Function2(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
            tmpRow.Add(tmpField.ToString());
			// 项读取完毕
            tmpField.Remove(0, tmpField.Length);
        }

		private static void Function3(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
            Function2(table, ref tmpRow, tmpField, cr, preStat);
            table.Add(tmpRow); 
			// 行读取完毕
			tmpRow = new CsvRow();
        }

		private static void Function5(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
            Function1(table, ref tmpRow, tmpField, cr, preStat);
        }

		private static void Function7(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
			if (preStat == 0 || preStat == 3) return;
			tmpRow.Add(tmpField.ToString());
			// 项读取完毕(另一个状态)
			table.Add(tmpRow);
			// 行读取完毕(另一个状态)
        }

		private static void Function8(List<CsvRow> table, ref CsvRow tmpRow, StringBuilder tmpField, char cr, int preStat)
        {
			// 读取错误,清空输出
            table.Clear();
        }
    }
}
