using MakeUnique.Lib.Plugin;
using MakeUnique.Lib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeUnique
{
    static class Program
    {
        //void InitializePlugin()
        //{
        //    try
        //    {
        //        CatalogHelper.ComposeParts(this);
        //        if (pathManager_ != null && pluginManager_ != null)
        //        {
        //            return;
        //        }
        //        throw new ApplicationException("无法初始化");
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(this, e.Message, "插件载入失败");
        //    }

        //    Application.Exit();
        //}
        //[Import(typeof(IPathManager))]
        //private IPathManager pathManager_;


        //[Import(typeof(IPluginManager))]
        //private IPluginManager pluginManager_;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(CatalogHelper.Container.GetExportedValue<GUI>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
