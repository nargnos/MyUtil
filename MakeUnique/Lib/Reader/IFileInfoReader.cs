using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Detail
{
    public interface IFileInfoReader
    {
        ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files);
        string ConvertGroupKey(string key);
    }
}
