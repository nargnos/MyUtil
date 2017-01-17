using System;
using System.Collections.Generic;
using System.Linq;
using MakeUnique.Lib.Detail;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class Md5Duplicate : SizeDuplicate
    {
        private static string grpName_ = "MD5";
        protected override string GroupName
        {
            get
            {
                return grpName_;
            }
        }

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

        

        public override ParallelQuery<IGrouping<string, string>> Do(HashSet<string> inputFiles)
        {
            return (from md5Grp in GroupingFiles(inputFiles)
                     .SelectMany((grp) => grp)
                     .GroupBy((path) => Utils.GetMD5(path), new MD5KeyComparer())
                          where md5Grp.Count() > 1
                          select new GroupingKeyConverter<byte[], string, string>(md5Grp, md5ConvertFunc) as IGrouping<string, string>).AsUnordered();
        }

        private Func<byte[], string> md5ConvertFunc = (md5) =>
            $"{grpName_}: {BitConverter.ToString(md5).Replace("-", string.Empty)}";

    }
}
