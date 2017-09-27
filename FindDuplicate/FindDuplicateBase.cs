using ForeachFileLib.Addon;
using System.Collections.Generic;

namespace FindDuplicate
{
    abstract class FindDuplicateBase<T> : FileAddonBase<T>
    {
        public FindDuplicateBase()
        {
            Filter = DelOneMemberKeyFilter;
        }
    }
}
