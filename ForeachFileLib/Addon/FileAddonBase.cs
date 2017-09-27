namespace ForeachFileLib.Addon
{
    public abstract class FileAddonBase<TGroupType> : AddonBase<TGroupType>
    {
        public FileAddonBase()
        {
            Commands = AddonDefaultFileCommand.CreateCommands();
            DefaultCommand = AddonDefaultFileCommand.OpenFileCmd;
        }
    }
}
