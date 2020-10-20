using System.Web;

namespace Damacon.SharedWeb.Helpers
{
    public static class CacheManager
    {
        public static void AddToCache(string key, object value)
        {
            HttpContext.Current.Cache.Insert(key, value);
        }

        public static object GetFromCache(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        public static object RemoveFromCache(string key)
        {
            return HttpContext.Current.Cache.Remove(key);
        }
    }
}