using MakeUnique.Lib.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Plugin
{
    public abstract class PluginBase<TKey> : IPlugin
    {
        public abstract string Name { get; }

        public virtual string CommandName { get; } = "清理";

        public virtual async Task<IDictionary<string, IEnumerable<string>>> MakeGroup(HashSet<string> files, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                var dic = new ConcurrentDictionary<TKey, ConcurrentBag<string>>();
                try
                {
                    var query = PluginDo(files).AsUnordered();
                    if (token != null)
                    {
                        query = query.WithCancellation(token);
                    }
                    query.ForAll(item =>
                        dic.AddOrUpdate(
                            item.Key,
                            str => new ConcurrentBag<string>(item),
                            (str, bag) =>
                            {
                                foreach (var path in item)
                                {
                                    bag.Add(path);
                                }
                                return bag;
                            })
                    );
                }
                catch (Exception)
                {
                    // 发生错误或中止照样把已处理到的数据输出
                }
                // 先分组再转换key
                return dic.ToDictionary(item => GroupNameConvert(item.Key), item => (IEnumerable<string>)item.Value);
            });
        }
        // key 为组名， value 为数据
        // 之后会分组
        internal protected abstract ParallelQuery<IGrouping<TKey, string>> PluginDo(HashSet<string> files);
        internal protected abstract string GroupNameConvert(TKey key);

        public virtual async Task<bool> Do(IEnumerable<string> files)
        {
            if (files == null)
            {
                return false;
            }
            try
            {
                await Task.Run(() => files.AsParallel().ForAll(path => Utils.RecycleFile(path)));
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
