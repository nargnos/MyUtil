using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{



    public class FileSizeReader : FileInfoReaderBase
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr StrFormatByteSizeW(long qdw, StringBuilder pszBuf, int cchBuf);

        public override string ConvertGroupKey(string key)
        {
            var size = Convert.ToInt64(key);
            StringBuilder sb = new StringBuilder(32);
            StrFormatByteSizeW(size, sb, sb.Capacity);

            return $"文件大小: {sb.ToString()}";
        }

        protected override string GetInfo(string path)
        {
            return new FileInfo(path).Length.ToString();
        }
    }
}
