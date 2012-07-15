using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Caching
{
    public static class CacheExtensions
    {
        public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator) where T : class
        {
            if (ConfigurationManager.AppSettings["DISABLE_CACHE"] == "True")
                return generator() as T;

            T value = cache[key] as T;
            if (value == null)
            {
                value = generator();
                cache.Insert(key, value, null, DateTime.MaxValue, Cache.NoSlidingExpiration);
            }
            return value;
        }
    }

}