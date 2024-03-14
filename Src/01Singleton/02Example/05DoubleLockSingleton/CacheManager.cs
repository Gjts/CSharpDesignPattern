using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Singleton._02Example._05DoubleLockSingleton
{
    // 双验证缓存管理器
    public class CacheManager<T>
    {
        private static volatile CacheManager<T>? instance;
        private static readonly object lockObject = new object();

        // 这个缓存可以使用Memory Cache 或者其他的缓存容器
        private Dictionary<string, CacheItem<T>> cache;
        private TimeSpan defaultExpirationTime = TimeSpan.FromMinutes(30);
        private int maxCacheSize = 100;

        // 保护方法别被外界干扰
        private CacheManager()
        {
            cache = new Dictionary<string, CacheItem<T>>();
        }

        public static CacheManager<T> GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new CacheManager<T>();
                    }
                }
            }

            return instance;
        }

        public string AddCache(string key, T value, TimeSpan? expirationTime = null)
        {
            if (value == null)
            {
                return "提供了一个空值";
            }

            if (cache.Count >= maxCacheSize)
            {
                return "超出缓存最佳大小";
            }

            CacheItem<T> cacheItem = new CacheItem<T>(value, expirationTime ?? defaultExpirationTime);
            cache[key] = cacheItem;
            return $"添加key{key}和{value}缓存成功";
        }

        public T? GetCache(string key)
        {
            if (cache.ContainsKey(key))
            {
                CacheItem<T> cacheItem = cache[key];
                if (cacheItem.ExpirationTime > DateTime.Now)
                {
                    return cacheItem.Value;
                }
                else
                {
                    // 缓存过期删除缓存
                    cache.Remove(key);
                }
            }

            return default(T);
        }

        public string Remove(string key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }

            return $"删除缓存{key}和值{cache[key]}";
        }

        public string Clear()
        {
            cache = new Dictionary<string, CacheItem<T>>();
            return "清空缓存";
        }
    }
}
