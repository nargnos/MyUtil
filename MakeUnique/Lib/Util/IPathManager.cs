using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
namespace MakeUnique.Lib.Util
{
    public interface IPathManager : ISet<string>
    {
        Task<HashSet<string>> GetFiles(string pattern, SearchOption option, CancellationToken token);
    }
}