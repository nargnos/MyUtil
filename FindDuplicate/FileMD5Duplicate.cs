
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeachFileLib.Addon;
using System.Threading;
using System.Collections.Concurrent;
using ForeachFileLib.Util;

namespace FindDuplicate
{
    class FileMD5Duplicate : FileSizeDuplicate, IEqualityComparer<byte[]>
    {

        public FileMD5Duplicate()
        {
            Name = "查找MD5重复文件";
            Ver = 0;
        }

        public bool Equals(byte[] x, byte[] y)
        {
            return x?.SequenceEqual(y) ?? false;
        }

        public int GetHashCode(byte[] obj)
        {
            return obj.Sum(item => item);
        }

        protected override Dictionary<string, HashSet<string>> Grouping(
            IEnumerable<Tuple<long, string>> datas, CancellationToken token)
        {
            // 先按大小分组，筛选出大小重复的
            var grpBySize = base.DoGrouping(datas, token);
            // 删掉单个成员key
            var grps = DelOneMemberKeyFilter(grpBySize);

            var bag = new ConcurrentBag<ConcurrentDictionary<byte[], ConcurrentBag<string>>>();
            grps.AsParallel().AsUnordered().ForAll(grp =>
            {
                var md5Grp = new ConcurrentDictionary<byte[], ConcurrentBag<string>>(this);
                // 对每一分组求md5分组
                grp.Value.AsParallel().AsUnordered().ForAll(path =>
                {
                    var md5 = FileMD5.GetMD5(path);
                    var tmpBag = md5Grp.GetOrAdd(md5, ign => new ConcurrentBag<string>());
                    tmpBag.Add(path);
                });
                // 删除单一分组

                var oneValueKeys = from item in md5Grp where item.Value.Count <= 1 select item.Key;
                foreach (var item in oneValueKeys.ToList())
                {
                    ConcurrentBag<string> ignore = null;
                    md5Grp.TryRemove(item, out ignore);
                }
                if (md5Grp.Count > 0)
                {
                    // 并入总容器
                    bag.Add(md5Grp);
                }
            });

            return ToDictionary(bag);
        }

        private static Dictionary<string, HashSet<string>> ToDictionary(
            ConcurrentBag<ConcurrentDictionary<byte[], ConcurrentBag<string>>> bag)
        {
            var ret = new Dictionary<string, HashSet<string>>();
            foreach (var item in bag.SelectMany(i => i))
            {
                var key = ConvertKey(item.Key);
                HashSet<string> tmpSet = null;
                if (!ret.TryGetValue(key, out tmpSet))
                {
                    tmpSet = new HashSet<string>();
                    ret.Add(key, tmpSet);
                }
                foreach (var val in item.Value)
                {
                    tmpSet.Add(val);
                }
            }

            return ret;
        }

        private static string ConvertKey(byte[] key)
        {
            return $"MD5: {BitConverter.ToString(key)}";
        }
    }
}
