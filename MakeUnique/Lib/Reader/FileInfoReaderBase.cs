using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Detail
{
    public abstract class FileInfoReaderBase : IFileInfoReader
    {
        protected abstract string GetInfo(string path);

        public virtual ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files)
        {
            return (from fileName in files
                    group fileName by GetInfo(fileName) into grp
                    where grp.Count() > 1
                    select grp).AsUnordered();
        }

        public abstract string ConvertGroupKey(string key);
    }
}
