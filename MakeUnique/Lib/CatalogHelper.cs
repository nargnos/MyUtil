using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MakeUnique.Lib
{
    public static class CatalogHelper
    {

        public static string PluginPath { get; set; } = ".\\Plugins";
       
        public static void ComposeParts(params object[] parts)
        {
            container_.Value?.ComposeParts(parts);
        }
        private static Lazy<CompositionContainer> container_ = new Lazy<CompositionContainer>(() =>
        {
            CompositionContainer result = null;
            try
            {
                var ac = new AggregateCatalog();
                ac.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
                if (Directory.Exists(PluginPath))
                {
                    ac.Catalogs.Add(new DirectoryCatalog(PluginPath));
                }
                result = new CompositionContainer(ac);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "插件载入错误");
            }
            return result;
        }, true);

    }
}
