using ForeachFileLib.Addon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace ForeachFileLib.Presenter
{
    [Export(typeof(ITaskPresenter))]
    class TaskPresenter : PresenterBase, ITaskPresenter
    {
        public IEnumerable<string> Addons
        {
            get
            {
                return Core.AddonManager.Keys;
            }
        }
        private IAddon GetAddon(string name)
        {
            return Core.AddonManager[name];
        }
        public event TaskBeginEventHandler TaskBegin;
        public event TaskEndEventHandler TaskEnd;



        public async Task Do(string addonName)
        {
            try
            {
                using (var cts = new CancellationTokenSource())
                {
                    var bgn = new TaskBeginEventArgs(cts);
                    var token = cts.Token;
                    TaskBegin?.Invoke(this, bgn);
                    var addon = GetAddon(addonName);

                    var ret = await Core.Do(addon, token);

                    TaskEnd?.Invoke(this, new TaskEndEventArgs(ret, addon.DefaultCommand, addon.Commands));
                }
            }
            catch (AggregateException e) when (e.InnerException is OperationCanceledException)
            {
                TaskEnd?.Invoke(this, new TaskEndEventArgs(new Exception(Properties.Resources.TaskCanceled)));
            }
            catch (Exception e)
            {
                TaskEnd?.Invoke(this, new TaskEndEventArgs(e));
            }
        }

    }
}
