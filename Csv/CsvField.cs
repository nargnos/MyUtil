using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csv
{
	public static class CsvField
	{
		public static T AsType<T>(this string val)
		{
			return (T)Convert.ChangeType(val, typeof(T));
		}
	}
}
