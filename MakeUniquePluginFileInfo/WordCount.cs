using MakeUnique.Lib.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeUniquePluginFileInfo
{
    class WordCount : PluginBase<string>
    {
        public override string Name { get; } = "统计字符数";

        protected override string GroupNameConvert(string key)
        {
            return key;
        }

        protected override ParallelQuery<IGrouping<string, string>> PluginDo(HashSet<string> files)
        {
            int lineCount = 0;
            int wordCount = 0;

            var count = from item in files.AsParallel()
                        let lines = from line in File.ReadAllLines(item) where !string.IsNullOrWhiteSpace(line) select line
                        let lc = Interlocked.Add(ref lineCount, lines.Count())
                        let wc = Interlocked.Add(ref wordCount, lines.AsParallel().Sum((str) => str.Length))
                        select 1;

            return from i in new List<string>() { "统计" }.AsParallel()
                   let c = count.Sum()
                   group $"共 {c} 个文件，{lineCount} 行，{wordCount} 字符" by i;

        }
    }
}
