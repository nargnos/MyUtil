using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.ComponentModel.Composition;
using System;

namespace MakeUnique.Lib
{
    [Export(typeof(IPluginUtil))]
    public class PluginUtil : ICollection<string>, IPluginUtil
    {
        public int Count
        {
            get
            {
                return paths_.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Add(string item)
        {
            if (Directory.Exists(item) || File.Exists(item))
            {
                return paths_.Add(item);
            }
            return false;
        }


        public void Clear()
        {
            paths_.Clear();
        }

        public bool Contains(string item)
        {
            return paths_.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            paths_.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return paths_.GetEnumerator();
        }

        public bool Remove(string item)
        {
            return paths_.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return paths_.GetEnumerator();
        }

        private HashSet<string> paths_ = new HashSet<string>();
        [ImportMany(typeof(IPlugin))]
        private IEnumerable<Lazy<IPlugin>> plugins_;
        public IReadOnlyCollection<IPlugin> GetPlugins()
        {
            return (from plugin in plugins_
                    select plugin.Value).ToList();
        }
        public string this[int index] => GetPath(index);
        public string GetPath(int index)
        {
            return paths_.ElementAt(index);
        }

        private IEnumerable<string> GetDirs()
        {
            return from path in paths_ where Directory.Exists(path) select path;
        }
        private IEnumerable<string> GetFiles()
        {
            return from path in paths_ where File.Exists(path) select path;
        }
        public HashSet<string> GetAllFiles(string pattern, SearchOption option)
        {
            var dirs = GetDirs();
            if (option == SearchOption.AllDirectories)
            {
                // 去除子包含
                var subDirs = from dir in dirs
                              from subDir in Directory.EnumerateDirectories(dir, pattern, option)
                              select subDir;
                dirs = subDirs.Concat(dirs).Distinct();
            }
            HashSet<string> result = new HashSet<string>();

            foreach (var item in GetFiles().Concat(dirs.SelectMany((dirPath) =>
                Directory.EnumerateFiles(dirPath, pattern, SearchOption.TopDirectoryOnly))))
            {
                result.Add(item);
            }
            return result;
        }

        void ICollection<string>.Add(string item)
        {
            paths_.Add(item);
        }

        public ParallelQuery<IGrouping<string, string>> Do(IPlugin plugin, string pattern, bool isIncludeSub)
        {
            if (Count == 0)
            {
                return null;
            }
            var paths = GetAllFiles(pattern, isIncludeSub ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (paths.Count == 0)
            {
                return null;
            }
            return plugin.Do(paths);
        }

    }
}
