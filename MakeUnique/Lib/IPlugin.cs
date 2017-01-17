using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
namespace MakeUnique.Lib
{
    [InheritedExport(typeof(IPlugin))]
    public interface IPlugin
    {
        string Name { get; }
        ParallelQuery<IGrouping<string, string>> Do(HashSet<string> inputFiles);
    }

   
}
