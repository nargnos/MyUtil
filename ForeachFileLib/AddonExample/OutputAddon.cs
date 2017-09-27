using ForeachFileLib.Properties;
using ForeachFileLib.Addon;
using System;

namespace ForeachFileLib.AddonExample
{

    class OutputAddon : FileAddonBase<string>
    {
        public OutputAddon()
        {
            Name = Resources.AddonNameOutputAll;
            Ver = 0;
        }

        public override bool Equals(string x, string y)
        {
            return string.Equals(x, y);
        }

        public override int GetHashCode(string obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        protected override string GetGroupData(string path)
        {
            return string.Empty;
        }

        protected override string ConvertGroupName(string key)
        {
            return key;
        }
    }
}
