using MakeUnique.Lib.Detail;
using MakeUnique.Lib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class SizeDuplicate : PluginBase<long>
    {
        public const string GrpName = "文件大小";
        public override string Name { get; } = "查找重复 (" + GrpName + ")";

        internal protected override ParallelQuery<IGrouping<long, string>> PluginDo(HashSet<string> files)
        {
            return from path in files.AsParallel().AsUnordered()
                   let size = GetFileSize(path)
                   where size >= 0
                   group path by size into result
                   where result.Count() > 1
                   select result;
        }
        // 发生错误返回-1
        public static long GetFileSize(string path)
        {
            try
            {
                return Utils.GetFileSize(path);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        internal protected override string GroupNameConvert(long key)
        {
            StringBuilder sb = new StringBuilder(32);
            NativeMethods.StrFormatByteSizeW(key, sb, sb.Capacity);
            return $"{GrpName}: {sb.ToString()} ({Convert.ToString(key)} Bytes)";
        }
        
    }
}
