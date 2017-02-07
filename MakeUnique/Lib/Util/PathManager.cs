using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Util
{
    [Export(typeof(IPathManager))]
    class PathManager : HashSet<string>, IPathManager
    {
        public new bool Add(string path)
        {
            if (Utils.IsPathExist(path))
            {
                return base.Add(path);
            }
            return false;
        }

        public async Task<HashSet<string>> GetFiles(string pattern, SearchOption option, CancellationToken token)
        {
            if (!this.Any())
            {
                return default(HashSet<string>);
            }
            var files = from path in this
                        where File.Exists(path)
                        select path;
            var dirs = from path in this
                       where Directory.Exists(path)
                       select path;

            var subDirFiles = from path in dirs.AsParallel().SelectMany(item => Directory.EnumerateFiles(item, pattern, option))
                              select path;
            if (token != null)
            {
                subDirFiles = subDirFiles.WithCancellation(token);
            }
            return await Task.Run(() =>
            {
                var bag = new ConcurrentBag<string>(files);
                try
                {
                    subDirFiles.ForAll(path => bag.Add(path));
                }
                catch (Exception)
                {
                    // 继续将读到的东西输出
                }
                return new HashSet<string>(bag);
            });
        }
    }
}
