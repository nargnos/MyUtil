using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class NameDuplicate : DuplicateFinderBase
    {
        private static string grpName_ = "文件名";
        protected override string GroupName
        {
            get
            {
                return grpName_;
            }
        }

        public override ParallelQuery<IGrouping<string, string>> Do(HashSet<string> inputFiles)
        {
           return (from fileName in inputFiles.AsParallel()
                   group fileName by GetFileName(fileName) into grp
                   where grp.Count() > 1
                   select grp).AsUnordered();
        }
        
        protected string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
