using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeUnique.Lib;
using System.Reflection;

namespace MakeUnique.Lib.Finder
{
    public static class FinderFactory
    {
        public static IReadOnlyCollection<IGetDuplicate> GetDuplicateFinders()
        {
            return finders_;
        }
        static FinderFactory()
        {
            finders_ = (from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsClass && !type.IsAbstract && typeof(IGetDuplicate).IsAssignableFrom(type)
                        select Activator.CreateInstance(type) as IGetDuplicate).ToList();
        }
        static List<IGetDuplicate> finders_;
    }
}
