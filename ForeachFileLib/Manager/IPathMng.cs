using System.Collections.Generic;
using System.Threading;

namespace ForeachFileLib.Manager
{
    public interface IPathMng : ISet<string>
    {
        HashSet<string> GetFiles(string pattern,bool useRegex, bool isIncSubDir, CancellationToken token);
    }
}
