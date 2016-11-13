using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Reader
{
    class GroupingKeyConverter<TSourceKey, TKey, TElememt> : IGrouping<TKey, TElememt>
    {
        public TKey Key
        {
            get; private set;
        }
        public GroupingKeyConverter(IGrouping<TSourceKey, TElememt> grp, Func<TSourceKey, TKey> convert)
        {
            grp_ = grp;
            Key = convert(grp.Key);
        }


        public IEnumerator<TElememt> GetEnumerator()
        {
            return grp_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return grp_.GetEnumerator();
        }
        IGrouping<TSourceKey, TElememt> grp_;
    }
}
