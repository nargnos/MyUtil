using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ForeachFileLib.Addon
{
    public abstract class AddonBase<TGroupType> : IAddon, IEqualityComparer<TGroupType>
    {
        public IReadOnlyDictionary<string, Command> Commands { get; protected set; }
        public Command DefaultCommand { get; protected set; }
        public string Name { get; protected set; }
        public int Ver { get; protected set; }

        protected Func<ConcurrentDictionary<TGroupType, ConcurrentBag<string>>,
            IEnumerable<KeyValuePair<TGroupType, ConcurrentBag<string>>>> Filter
        { get; set; }

        public IResult GetResult(HashSet<string> paths, CancellationToken token)
        {
            return DoGetResult(paths, token);
        }
        // 添加一个这个方便做hook，可以提供筛选功能
        protected virtual IResult DoGetResult(IEnumerable<string> paths, CancellationToken token)
        {
            if (paths == null)
            {
                throw new ArgumentNullException("paths");
            }
            var datas = GetDatas(paths);
            var dict = Grouping(datas, token);
            return CreateResult(dict);
        }


        protected virtual Result CreateResult(Dictionary<string, HashSet<string>> dict)
        {
            return new Result(dict);
        }


        protected virtual Dictionary<string, HashSet<string>> Grouping(
            IEnumerable<Tuple<TGroupType, string>> datas, CancellationToken token)
        {
            var cdict = DoGrouping(datas, token);
            return ToDictionary(cdict);
        }

        protected virtual ConcurrentDictionary<TGroupType, ConcurrentBag<string>> DoGrouping(
            IEnumerable<Tuple<TGroupType, string>> datas, CancellationToken token)
        {
            var cdict = new ConcurrentDictionary<TGroupType, ConcurrentBag<string>>(this);
            datas.AsParallel().AsUnordered().WithCancellation(token).ForAll(item =>
            {
                var bag = cdict.GetOrAdd(item.Item1, ignore => new ConcurrentBag<string>());
                bag.Add(item.Item2);
            });
            return cdict;
        }

        protected abstract string ConvertGroupName(TGroupType key);

        protected virtual Dictionary<string, HashSet<string>> ToDictionary(
            ConcurrentDictionary<TGroupType, ConcurrentBag<string>> cdict)
        {
            var ret = new Dictionary<string, HashSet<string>>();
            foreach (var item in Filter?.Invoke(cdict) ?? cdict)
            {
                var tmpSet = new HashSet<string>();
                ret.Add(ConvertGroupName(item.Key), new HashSet<string>(item.Value));
            }
            return ret;
        }

        private IEnumerable<Tuple<TGroupType, string>> GetDatas(IEnumerable<string> paths)
        {
            return from path in paths select new Tuple<TGroupType, string>(GetGroupData(path), path);
        }

        protected abstract TGroupType GetGroupData(string path);

        // 删除只有一个成员的key
        protected IEnumerable<KeyValuePair<TGroupType, ConcurrentBag<string>>> DelOneMemberKeyFilter(
           ConcurrentDictionary<TGroupType, ConcurrentBag<string>> dict)
        {
            return from item in dict where item.Value.Count > 1 select item;
        }

        public abstract bool Equals(TGroupType x, TGroupType y);
        public abstract int GetHashCode(TGroupType obj);
    }
}
