using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib
{
    public interface IGetDuplicate
    {
        ParallelQuery<IGrouping<string, string>> GetDuplicateFiles(ParallelQuery<string> files);
        string ConvertGroupKey(string key);
        string Name { get; }
    }
}
