using System.Collections.Generic;
using System.Linq;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    abstract class DuplicateFinderBase : IPlugin
    {
        protected abstract string GroupName { get; }
        private static string namePrefix = "查找重复";
        public string Name => $"{namePrefix}({GroupName})";
        public abstract ParallelQuery<IGrouping<string, string>> Do(HashSet<string> inputFiles);
    }
}
