using ForeachFileLib.Addon;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ForeachFileLib.Presenter
{
    public delegate void TaskBeginEventHandler(object sender, TaskBeginEventArgs e);
    public delegate void TaskEndEventHandler(object sender, TaskEndEventArgs e);

    public class TaskBeginEventArgs : EventArgs
    {
        public CancellationTokenSource Cancellation { get; private set; }
        public TaskBeginEventArgs(CancellationTokenSource c)
        {
            Cancellation = c;
        }
    }

    public class TaskEndEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public bool IsSucceeded { get; private set; }
        public IReadOnlyDictionary<string, Command> Commands { get; private set; }
        public Command DefaultCommand { get; private set; }
        public IResult Result { get; private set; }
        public TaskEndEventArgs(Exception e)
        {
            IsSucceeded = false;
            Exception = e;
        }
        public TaskEndEventArgs(IResult ret, Command dcmd, IReadOnlyDictionary<string, Command> cmds)
        {
            IsSucceeded = true;
            Result = ret;
            DefaultCommand = dcmd;
            Commands = cmds;
        }
    }

    public interface ITaskPresenter : IPresenter
    {
        IEnumerable<string> Addons { get; }

        event TaskBeginEventHandler TaskBegin;
        event TaskEndEventHandler TaskEnd;
        Task Do(string addon);
    }
}