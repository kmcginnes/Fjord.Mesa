using System;
using System.Web;
using System.Web.Caching;

namespace Fjord.Mesa.Helpers
{
    public class AppCache
    {
        public static T FromCache<T>(Func<T> create) where T : class
        {
            return FromCache(typeof(T).FullName, create);
        }

        public static T FromCache<T>(string name, Func<T> create) where T : class
        {
            return HttpRuntime.Cache.GetOrStore(name, create);
        }
    }
}