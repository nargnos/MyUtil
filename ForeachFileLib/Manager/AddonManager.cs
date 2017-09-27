using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.Collections;
using ForeachFileLib.Util;
using ForeachFileLib.Addon;

namespace ForeachFileLib.Manager
{
    [Export(typeof(IAddonMng))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class AddonManager : IAddonMng
    {
        public AddonManager()
        {
            addons_ = new Lazy<Dictionary<string, IAddon>>(LoadAddons, true);
        }

        private Dictionary<string, IAddon> LoadAddons()
        {
            var addons = Composition.Container.GetExports<IAddon>();
            var load = from item in addons
                       orderby item.Value.Name ascending, item.Value.Ver descending
                       group item by item.Value.Name into tmpGrp
                       select new { Name = tmpGrp.Key, Addon = tmpGrp.First(), Ignore = tmpGrp.Skip(1) };

            var relList = (from item in load select item.Ignore).SelectMany((i) => i);

            ReleaseExport(relList);

            return load.ToDictionary(item => item.Name, (item) => item.Addon.Value);
        }

        private static void ReleaseExport(IEnumerable<Lazy<IAddon>> relList)
        {
            foreach (var item in relList)
            {
                Composition.Container.ReleaseExport(item);
            }
        }

        public IReadOnlyDictionary<string, IAddon> Addons
        {
            get
            {
                return addons_.Value;
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return Addons.Keys;
            }
        }

        public IEnumerable<IAddon> Values
        {
            get
            {
                return Addons.Values;
            }
        }

        public int Count
        {
            get
            {
                return Addons.Count;
            }
        }

        public IAddon this[string key]
        {
            get
            {
                return Addons[key];
            }
        }


        public bool ContainsKey(string key)
        {
            return Addons.ContainsKey(key);
        }

        public bool TryGetValue(string key, out IAddon value)
        {
            return Addons.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, IAddon>> GetEnumerator()
        {
            return Addons.GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Addons.GetEnumerator();
        }

        private Lazy<Dictionary<string, IAddon>> addons_;
    }
}
