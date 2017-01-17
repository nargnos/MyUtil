using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeUnique.Lib.Plugin.DuplicateFinder
{
    class SizeDuplicate : DuplicateFinderBase
    {
        private static string grpName_ = "文件大小";
        protected override string GroupName
        {
            get
            {
                return grpName_;
            }
        }

        protected ParallelQuery<IGrouping<long, string>> GroupingFiles(HashSet<string> files)
        {
            return (from fileName in files.AsParallel()
                    group fileName by Utils.GetFileSize(fileName) into grp
                    where grp.Count() > 1
                    select grp).AsUnordered();
        }

        
        
        public override ParallelQuery<IGrouping<string, string>> Do(HashSet<string> inputFiles)
        {
            return (from grp in GroupingFiles(inputFiles)
                    select new GroupingKeyConverter<long, string, string>(grp, keyConvertFunc) as IGrouping<string, string>).AsUnordered();
        }

        private Func<long, string> keyConvertFunc = (size) =>
        {
            StringBuilder sb = new StringBuilder(32);
            NativeMethods.StrFormatByteSizeW(size, sb, sb.Capacity);
            return $"{grpName_}: {sb.ToString()}";
        };

    }
}
