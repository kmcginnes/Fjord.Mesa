using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Fjord.Mesa.Helpers
{
    public class AppCache
    {
        public static T FromCache<T>(Func<T> create) where T : class
        {
            return FromCache<T>(typeof(T).FullName, create);
        }

        public static T FromCache<T>(string name, Func<T> create) where T : class
        {
            return HttpRuntime.Cache.GetOrStore(name, create);
        }
    }
}