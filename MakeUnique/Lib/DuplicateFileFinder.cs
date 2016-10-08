using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{
    public class DuplicateFileFinder
    {
        public DuplicateFileFinder()
        {
            dirs_ = new HashSet<string>();
        }

        public bool Add(string path)
        {
            return dirs_.Add(path);
        }
        public bool Remove(string path)
        {
            return dirs_.Remove(path);
        }
        public void Clear()
        {
            dirs_.Clear();
        }
        protected ParallelQuery<string> GetAllFiles(string pattern, SearchOption option)
        {
            return (from dir in dirs_.AsParallel().AsUnordered()
                    from file in Directory.EnumerateFiles(dir, pattern, option).AsParallel().AsUnordered()
                    select file).AsUnordered().Distinct();
        }
        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IFileInfoReader fileInfoReader)
        {
            return fileInfoReader.GetDuplicateFiles(GetAllFiles(pattern, option));
        }
        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IFileInfoReader fileInfoReader, CancellationTokenSource cancel)
        {
            return fileInfoReader.GetDuplicateFiles(GetAllFiles(pattern, option)).WithCancellation(cancel.Token);
        }
        public int Count()
        {
            return dirs_.Count;
        }
        public string ElementAt(int index)
        {
            return dirs_.ElementAt(index);
        }

        protected HashSet<string> dirs_;
    }
}
