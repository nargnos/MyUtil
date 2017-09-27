using System.ComponentModel.Composition;

namespace ForeachFileLib.Presenter
{
    public abstract class PresenterBase : IPresenter
    {
        [Import(typeof(ICore))]
        public ICore Core { get; private set; }
    }
}
