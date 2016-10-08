using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{
    public class FileMd5Reader : FileSizeReader
    {

        public override string ConvertGroupKey(string key)
        {
            return $"MD5: {key}";
        }
        public override ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files)
        {
            return (from path in base.GetDuplicateFiles(files).SelectMany((val) => val)
                    group path by GetMD5(path) into md5Grp
                    where md5Grp.Count() > 1
                    select md5Grp).AsUnordered();
        }
        protected string GetMD5(string path)
        {
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read))
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var result = md5.ComputeHash(file);
                return BitConverter.ToString(result).Replace("-", "");
            }
        }
    }
}
