using ForeachFileLib.Addon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace ForeachFileLib.AddonExample
{
    class CountAddon : AddonBase<Tuple<long, long>>
    {
        public CountAddon()
        {
            Name = "统计所有文件字数";
            Ver = 0;
        }

        public override bool Equals(Tuple<long, long> x, Tuple<long, long> y)
        {
            return true;
        }


        public override int GetHashCode(Tuple<long, long> obj)
        {
            return obj.GetHashCode();
        }

        protected override string ConvertGroupName(Tuple<long, long> key)
        {
            throw new NotImplementedException();
        }

        // 返回行数/字数
        protected override Tuple<long, long> GetGroupData(string path)
        {
            var lines = (from line in File.ReadAllLines(path) where !string.IsNullOrWhiteSpace(line) select line).ToList();

            return new Tuple<long, long>(lines.Count, lines.Sum(line => (long)line.Length));
        }
        // 将这个函数修改成整体统计
        protected override Dictionary<string, HashSet<string>> Grouping(
            IEnumerable<Tuple<Tuple<long, long>, string>> datas, CancellationToken token)
        {
            long words = 0;
            long lines = 0;
            foreach (var item in datas)
            {
                lines += item.Item1.Item1;
                words += item.Item1.Item2;
            }
            
            var ret = new Dictionary<string, HashSet<string>>();
            var set = new HashSet<string>();
            set.Add($"行数 {lines} 字数 {words}");
            ret.Add("统计", set);
            return ret;
        }

    }
}
