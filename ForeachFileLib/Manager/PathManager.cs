using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.ComponentModel.Composition;
using ForeachFileLib.Util;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace ForeachFileLib.Manager
{
    [Export(typeof(IPathMng))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class PathManager : HashSet<string>, IPathMng
    {
        public new bool Add(string path)
        {
            if (Util.Util.IsPathExist(path))
            {
                return base.Add(path);
            }
            return false;
        }

        public HashSet<string> GetFiles(string pattern, bool useRegex, bool isIncSubDir, CancellationToken token)
        {
            if (!this.Any())
            {
                return new HashSet<string>();
            }
            var option = isIncSubDir ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var files = from path in this
                        where File.Exists(path)
                        select path;
            var dirs = from path in this
                       where Directory.Exists(path)
                       select path;
            var subDirFiles = from path in
                                  dirs.AsParallel().AsUnordered().WithCancellation(token).
                                    SelectMany(item => Directory.EnumerateFiles(item, "*", option))
                              select path;
            // Distinct会在创建set的时候再执行一次，但是先执行可以加快后面的匹配速度
            var paths = subDirFiles.Concat(files.AsParallel()).Distinct();
            
            if (!string.IsNullOrEmpty(pattern))
            {
                if (useRegex)
                {
                    paths = from item in paths where Regex.IsMatch(item, pattern) select item;
                }
                else
                {
                    paths = from item in paths where item.Contains(pattern) select item;
                }
            }
            var retData = new ConcurrentBag<string>();
            paths.ForAll(item => retData.Add(item));


            var ret = new HashSet<string>(retData);

            return ret;
        }
    }
}
