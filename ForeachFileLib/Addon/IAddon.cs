using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;

namespace ForeachFileLib.Addon
{

    [InheritedExport(typeof(IAddon))]
    public interface IAddon
    {
        string Name { get; }
        int Ver { get; }
        // 前是组名，后是值，无组名就空
        IResult GetResult(HashSet<string> paths, CancellationToken token);
        IReadOnlyDictionary<string, Command> Commands { get; }
        // 默认命令会在双击时执行
        Command DefaultCommand { get; }
    }
}
