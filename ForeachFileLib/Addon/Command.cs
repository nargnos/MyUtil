using System.Collections.Generic;

namespace ForeachFileLib.Addon
{
    public delegate void CommandHandler(IResult r, string key, IEnumerable<string> lines);
    public class Command
    {
        private CommandHandler h_;
        public Command(string name, CommandHandler h)
        {
            h_ = h;
            Name = name;
        }
        public void Run(IResult r, string key, IEnumerable<string> lines)
        {
            h_(r, key, lines);
        }
        public string Name { get; private set; }
    }
}
