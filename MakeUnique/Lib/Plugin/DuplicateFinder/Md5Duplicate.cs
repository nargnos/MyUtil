using System;
using System.Collections.Generic;
using System.Linq;
using MakeUnique.Lib.Detail;
using MakeUnique.Lib.Util;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class Md5Duplicate : PluginBase<byte[]>, IEqualityComparer<byte[]>
    {
        public const string GrpName = "MD5";
        public override string Name { get; } = "查找重复 (" + GrpName + ")";

        public byte[] GetMD5(string path)
        {
            try
            {
                return Utils.GetMD5(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Equals(byte[] x, byte[] y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.SequenceEqual(y);
        }

        public int GetHashCode(byte[] obj)
        {
            return obj.Sum(val => val);
        }

        private static SizeDuplicate sizeObj_ = new SizeDuplicate();

        internal protected override ParallelQuery<IGrouping<byte[], string>> PluginDo(HashSet<string> files)
        {
            var sizeGrp = sizeObj_.PluginDo(files);

            // 按大小分的每个组再按md5分组，再展开
            return (from grpItem in sizeGrp
                    select
                        (from path in grpItem
                         let md5 = GetMD5(path)
                         where md5 != null
                         select new { MD5 = md5, Path = path }).GroupBy(item => item.MD5, item => item.Path, this)).
                    SelectMany(item => item).Where(grp => grp.Count() > 1);
        }

        internal protected override string GroupNameConvert(byte[] key)
        {
            return $"{GrpName}: {BitConverter.ToString(key)}";
        }

    }
}
