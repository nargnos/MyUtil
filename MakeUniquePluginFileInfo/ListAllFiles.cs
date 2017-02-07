using MakeUnique.Lib.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUniquePluginFileInfo
{
    class ListAllFiles : PluginBase<string>
    {
        public override string Name
        {
            get
            {
                return "列出所有文件";
            }
        }

        protected override string GroupNameConvert(string key)
        {
            return key;
        }

        protected override ParallelQuery<IGrouping<string, string>> PluginDo(HashSet<string> files)
        {
            return from path in files.AsParallel()
                   group path by string.Empty;
        }
    }
}
