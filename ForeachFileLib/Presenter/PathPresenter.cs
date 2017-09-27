using ForeachFileLib.Manager;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ForeachFileLib.Presenter
{
    [Export(typeof(IPathPresenter))]
    class PathPresenter : PresenterBase, IPathPresenter
    {
        public int Count
        {
            get
            {
                return GetPathManager().Count;
            }
        }

        public event PathListCountChangedEventHandler PathListCountChanged;


        private void OnCountChanged()
        {
            PathListCountChanged?.Invoke(this, new PathPresenterEventArgs(this));
        }

        public string GetPath(int index)
        {
            return GetPathManager().ElementAt(index);
        }

        private IPathMng GetPathManager()
        {
            return Core.PathManager;
        }

        public bool RemovePath(params string[] path)
        {
            return ForeachPath(path, GetPathManager().Remove);
        }

        public void ClearPath()
        {
            if (!GetPathManager().Any())
            {
                return;
            }
            GetPathManager().Clear();
            OnCountChanged();
        }

        public bool AddPath(params string[] path)
        {
            return ForeachPath(path, GetPathManager().Add);
        }

        private bool ForeachPath(string[] path, Func<string, bool> cb)
        {
            if (path == null || path.Length == 0)
            {
                return false;
            }
            bool ret = false;
            foreach (var item in path)
            {
                if (cb(item))
                {
                    ret = true;
                }
            }
            if (ret)
            {
                OnCountChanged();
            }
            return ret;
        }
    }
}
