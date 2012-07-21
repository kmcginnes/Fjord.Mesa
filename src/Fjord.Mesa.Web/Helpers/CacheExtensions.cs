using System.Configuration;
using System.Diagnostics;

namespace System.Web.Caching
{
    public static class CacheExtensions
    {
        public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator) where T : class
        {
            if (ConfigurationManager.AppSettings["CacheEnabled"] == "False")
                return generator();

            T value = cache[key] as T;
            if (value == null)
            {
                Debug.WriteLine(string.Format("Key '{0}' not found in cache. Generating and storing value.", key));
                value = generator();
                cache.Insert(key, value, null, DateTime.MaxValue, Cache.NoSlidingExpiration);
            }
            else
            {
                Debug.WriteLine(string.Format("Key '{0}' found in cache.", key));
            }
            return value;
        }
    }

}