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
        public int Count()
        {
            return dirs_.Count;
        }
        public string this[int index] => GetDir(index);

        public string GetDir(int index)
        {
            return dirs_.ElementAt(index);
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

        private IEnumerable<string> GetDir()
        {
            return from path in dirs_ where Directory.Exists(path) select path;
        }
        // 得到的路径绝对不会重复
        private ParallelQuery<string> GetAllFiles(string pattern, SearchOption option)
        {
            IEnumerable<string> dirs = GetDir();
            if (option == SearchOption.AllDirectories)
            {
                var subDirs = from dir in dirs
                              from subDir in Directory.EnumerateDirectories(dir, pattern, option)
                              select subDir;
                dirs = subDirs.Concat(dirs).Distinct();
            }
            return (from dir in dirs.AsParallel()
                    from file in Directory.EnumerateFiles(dir, pattern, SearchOption.TopDirectoryOnly)
                    select file).AsParallel().AsUnordered();
        }

        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IGetDuplicate finder)
        {
            return finder.GetDuplicateFiles(GetAllFiles(pattern, option));
        }
        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(string pattern, SearchOption option, IGetDuplicate finder, CancellationTokenSource cancel)
        {
            return GetDuplicateFiles(pattern, option, finder).WithCancellation(cancel.Token);
        }

        private HashSet<string> dirs_;
    }
}
