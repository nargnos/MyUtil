using MakeUnique.Lib.Detail;
using System;
using System.Collections.Concurrent;
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
        protected IEnumerable<string> GetExistDir()
        {
            return (from path in dirs_ where Directory.Exists(path) select path).Distinct();
        }
        // 得到的路径绝对不会重复
        protected ParallelQuery<string> GetAllFiles(string pattern, SearchOption option)
        {
            IEnumerable<string> dirs = GetExistDir();
            if (option == SearchOption.AllDirectories)
            {
                var subDirs = from dir in GetExistDir()
                              from subDir in Directory.EnumerateDirectories(dir, pattern, option)
                              select subDir;
                dirs = subDirs.Concat(dirs).Distinct();
            }
            return (from dir in dirs.AsParallel()
                    from file in Directory.EnumerateFiles(dir, pattern, SearchOption.TopDirectoryOnly)
                    select file).AsParallel().AsUnordered();
        }
        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IFileInfoReader fileInfoReader)
        {
            return fileInfoReader.GetDuplicateFiles(GetAllFiles(pattern, option));
        }
        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IFileInfoReader fileInfoReader, CancellationTokenSource cancel)
        {
            return GetDuplicateFiles(pattern, option, fileInfoReader).WithCancellation(cancel.Token);
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
