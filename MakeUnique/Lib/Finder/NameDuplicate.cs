using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Finder
{
    class NameDuplicate : IGetDuplicate
    {
        public string Name => "文件名";

        public string ConvertGroupKey(string key)
        {
            return $"{Name}: {key}";
        }

        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files)
        {
            return (from fileName in files
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
