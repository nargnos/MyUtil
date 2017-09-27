using ForeachFileLib.Addon;
using System.Collections.Generic;

namespace ForeachFileLib.Manager
{
    public interface IAddonMng : IReadOnlyDictionary<string, IAddon>
    {
    }
}
