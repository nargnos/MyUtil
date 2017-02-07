using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Plugin
{    
    [InheritedExport(typeof(IPlugin))]
    public interface IPlugin
    {
        string Name { get; }
        // 插件的命令名称, 空表示没特殊命令
        string CommandName { get; }
        Task<IDictionary<string, IEnumerable<string>>> MakeGroup(HashSet<string> files, CancellationToken token);
        // true 表示处理成功，否则失败
        Task<bool> Do(IEnumerable<string> files);
    }
}
