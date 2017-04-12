using MakeUnique.Lib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Plugin
{
    [Export(typeof(IPluginManager))]
    class PluginManager : IPluginManager
    {
        [ImportMany(typeof(IPlugin))]
        public IEnumerable<Lazy<IPlugin>> Plugins { get; private set; }
    }
}
