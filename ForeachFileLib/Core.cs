using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using ForeachFileLib.Addon;
using ForeachFileLib.Manager;
using ForeachFileLib.Presenter;
using ForeachFileLib.Util;

namespace ForeachFileLib
{

    [Export(typeof(ICore))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class Core : ICore
    {
        public Task<IResult> Do(IAddon addon, CancellationToken token)
        {
            CheckImpl();
            if (addon==null)
            {
                throw new ArgumentNullException("addon");
            }
            return Task.Run(() => PathManager.GetFiles(FileFilter, UseRegex, IsIncSubDir, token)).
                ContinueWith(paths => addon.GetResult(paths.Result, token));
        }

        private void CheckImpl()
        {
            if (AddonManager == null ||
                PathManager == null)
            {
                throw new ApplicationException("组件载入失败");
            }
        }


        public bool TryFindPresenter<T>(out T ret)
            where T : class, IPresenter
        {
            CheckImpl();
            ret = null;
            var name = typeof(T).FullName;
            try
            {
                lock (psts_)
                {
                    IPresenter find = null;
                    if (!psts_.TryGetValue(name, out find))
                    {
                        find = Composition.Container.GetExportedValue<T>();
                        if (find != null)
                        {
                            psts_.Add(name, find);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    ret = (T)find;
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        private Dictionary<string, IPresenter> psts_ = new Dictionary<string, IPresenter>();

        public bool IsIncSubDir { get; set; } = true;
        public string FileFilter { get; set; } = string.Empty;
        public bool UseRegex { get; set; } = false;
        [Import(typeof(IAddonMng))]
        public IAddonMng AddonManager { get; private set; }

        [Import(typeof(IPathMng))]
        public IPathMng PathManager { get; private set; }

    }
}
