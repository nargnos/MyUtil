using ForeachFileLib.Addon;
using ForeachFileLib.Manager;
using ForeachFileLib.Presenter;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace ForeachFileLib
{
    public interface ICore
    {
        IAddonMng AddonManager { get; }
        IPathMng PathManager { get; }
        string FileFilter { get; set; }
        bool IsIncSubDir { get; set; }
        bool UseRegex { get; set; }
        bool TryFindPresenter<T>(out T ret) where T : class, IPresenter;
        Task<IResult> Do(IAddon addon, CancellationToken token);
    }
}
