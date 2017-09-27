using System;

namespace ForeachFileLib.Presenter
{
    public class PathPresenterEventArgs : EventArgs
    {
        public int PathCount { get; private set; }
        public PathPresenterEventArgs(IPathPresenter sender)
        {
            PathCount = sender.Count;
        }
    }
    public delegate void PathListCountChangedEventHandler(object sender, PathPresenterEventArgs e);
    public interface IPathPresenter : IPresenter
    {

        bool AddPath(params string[] path);
        bool RemovePath(params string[] path);
        void ClearPath();
        string GetPath(int index);
        int Count { get; }
        event PathListCountChangedEventHandler PathListCountChanged;
    }
}
