using System;
using System.Collections.Generic;

namespace MakeUnique.Lib.Plugin
{
    public interface IPluginManager
    {
        IEnumerable<Lazy<IPlugin>> Plugins { get; }
    }
}