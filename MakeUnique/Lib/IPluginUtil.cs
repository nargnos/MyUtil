using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MakeUnique.Lib
{
    public interface IPluginUtil
    {
        string this[int index] { get; }

        int Count { get; }
        bool IsReadOnly { get; }

        bool Add(string item);
        void Clear();
        bool Contains(string item);
        void CopyTo(string[] array, int arrayIndex);
        ParallelQuery<IGrouping<string, string>> Do(IPlugin plugin, string pattern, bool isIncludeSub);
        HashSet<string> GetAllFiles(string pattern, SearchOption option);
        IEnumerator<string> GetEnumerator();
        string GetPath(int index);
        IReadOnlyCollection<IPlugin> GetPlugins();
        bool Remove(string item);
    }
}