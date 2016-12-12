using MakeUnique.Lib.Detail;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Finder
{

    class SizeDuplicate : IGetDuplicate
    {
        public string Name => "文件大小";

        public string ConvertGroupKey(string key)
        {
            var size = Convert.ToInt64(key);
            StringBuilder sb = new StringBuilder(32);
            FileUtils.StrFormatByteSizeW(size, sb, sb.Capacity);

            return $"{Name}: {sb.ToString()}";
        }

        public ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files)
        {
            return (from grp in GroupingFiles(files)
                   select new GroupingKeyConverter<long, string, string>(grp, keyConvertFunc) as IGrouping<string, string>).AsUnordered();
        }
        protected ParallelQuery<IGrouping<long, string>> GroupingFiles(ParallelQuery<string> files)
        {
            return (from fileName in files
                    group fileName by GetFileSize(fileName) into grp
                    where grp.Count() > 1
                    select grp).AsUnordered();
        }

        protected long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }
        private Func<long, string> keyConvertFunc = (size) => Convert.ToString(size);
    }
}
