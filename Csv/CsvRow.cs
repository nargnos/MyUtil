using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csv
{
	public class CsvRow : List<string>
	{
		
		internal CsvTable Father { get; set; }
		public CsvRow()
		{ }
		public CsvRow(List<string> row)
		{
			AddRange(row);
		}
		public string this[string colName]
		{
			get
			{
				return Father.GetField(colName, this);
			}
		}
		public string GetField(string colName)
		{
			return this[colName];
		}
	}
}
