using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
namespace ForeachFileLib.Util
{
    public static class Composition
    {

        public static string PluginPath { get; } = ".\\Addons";

        public static void ComposeParts(params object[] parts)
        {
            try
            {
                Container?.ComposeParts(parts);
            }
            catch
            {

            }
        }

        public static CompositionContainer Container
        {
            get
            {
                return container_.Value;
            }
        }
        private static Lazy<CompositionContainer> container_ = new Lazy<CompositionContainer>(() =>
         {
             var ac = new AggregateCatalog();
             //ac.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
             
             ac.Catalogs.Add(new ApplicationCatalog());

             try
             {
                 if (Directory.Exists(PluginPath))
                 {
                     ac.Catalogs.Add(new DirectoryCatalog(PluginPath));
                 }
             }
             catch
             {
                 // 无法载入就忽略
             }
             return new CompositionContainer(ac);
         }, true);
    }
}
