using ForeachFileLib.Properties;
using ForeachFileLib.Util;
using System.Collections.Generic;

namespace ForeachFileLib.Addon
{
    public static class AddonDefaultFileCommand
    {
        public static Dictionary<string, Command> CreateCommands()
        {
            var ret = new Dictionary<string, Command>()
            {
                [OpenFileCmd.Name] = OpenFileCmd,
                [LocateFileCmd.Name] = LocateFileCmd,
                [DelFileCmd.Name] = DelFileCmd
            };
            return ret;
        }
        public static Command OpenFileCmd { get; private set; } =
            new Command(Resources.OpenFile, (ret, key, paths) => Util.Util.Run(paths));
        public static Command LocateFileCmd { get; private set; } =
            new Command(Resources.LocateFile, (ret, key, paths) => Util.Util.OpenFolderAndSelectFile(paths));
        public static Command DelFileCmd { get; private set; } =
            new Command(Resources.DelFile, (ret, key, paths) =>
            {
                Util.Util.RecycleFile(paths);
                ret.Remove(key, paths);
            });

    }
}
