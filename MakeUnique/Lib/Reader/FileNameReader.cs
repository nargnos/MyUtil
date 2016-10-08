using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{
    public class FileNameReader : FileInfoReaderBase
    {
        public override string ConvertGroupKey(string key)
        {
            return $"文件名: {key}";
        }

        protected override string GetInfo(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
