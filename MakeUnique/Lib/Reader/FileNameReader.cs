using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{
    public class FileNameReader : IFileInfoReader
    {
        public string ConvertGroupKey(string key)
        {
            return $"文件名: {key}";
        }

        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(HashSet<string> files)
        {
            return (from fileName in files.AsParallel()
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
