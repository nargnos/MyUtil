using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MakeUnique.Lib.Reader;

namespace MakeUnique.Lib
{
    public class FileMd5Reader : FileSizeReader, IFileInfoReader
    {
        protected class MD5KeyComparer : EqualityComparer<byte[]>
        {
            public override bool Equals(byte[] x, byte[] y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                else if (x == null || y == null)
                {
                    return false;
                }
                return x.SequenceEqual(y);
            }

            public override int GetHashCode(byte[] obj)
            {
                return obj.Sum((val) => val);
            }
        }
        public new string ConvertGroupKey(string key)
        {
            return $"MD5: {key}";
        }
        public new ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files)
        {
            return (from md5Grp in GroupingFiles(files)
                   .SelectMany((grp) => grp)
                   .GroupBy((path) => GetMD5(path), new MD5KeyComparer())
                   where md5Grp.Count() > 1
                   select new GroupingKeyConverter<byte[], string, string>(md5Grp, md5ConvertFunc) as IGrouping<string, string>).AsUnordered();
            
        }
        public static byte[] GetMD5(string path)
        {
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(file);
            }
        }
        private Func<byte[], string> md5ConvertFunc = (md5) => BitConverter.ToString(md5).Replace("-", string.Empty);
    }
}
