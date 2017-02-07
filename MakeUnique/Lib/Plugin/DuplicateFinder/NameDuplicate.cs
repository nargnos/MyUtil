using MakeUnique.Lib.Detail;
using MakeUnique.Lib.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class NameDuplicate : PluginBase<string>
    {
        public const string GrpName = "文件名";
        public override string Name { get; } = "查找重复 (" + GrpName + ")";


        public static string GetFileName(string path)
        {
            try
            {
                return Path.GetFileName(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal protected override ParallelQuery<IGrouping<string, string>> PluginDo(HashSet<string> files)
        {
            return from path in files.AsParallel().AsUnordered()
                   let fileName = GetFileName(path)
                   where !string.IsNullOrEmpty(fileName)
                   group path by fileName into result
                   where result.Count() > 1
                   select result;
        }
        
        internal protected override string GroupNameConvert(string key)
        {
            return $"{GrpName}: {key}";
        }
    }
}
