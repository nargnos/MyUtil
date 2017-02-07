using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MakeUnique.Lib.Util
{
    public static class CatalogHelper
    {

        public static string PluginPath { get; set; } = ".\\Plugins";

        public static void ComposeParts(params object[] parts)
        {
            // 这里如果插件实现错误的话还会有异常
            Container?.ComposeParts(parts);
        }
        
        public static CompositionContainer Container { get { return container_.Value; } }


        private static Lazy<CompositionContainer> container_ = new Lazy<CompositionContainer>(() =>
        {
            try
            {
                var ac = new AggregateCatalog();
                ac.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
                if (Directory.Exists(PluginPath))
                {
                    ac.Catalogs.Add(new DirectoryCatalog(PluginPath));
                }
                return new CompositionContainer(ac);
            }
            catch (Exception)
            {
                // 忽略初始化错误
            }
            return null;
        }, true);

    }
}
