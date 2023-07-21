using Core.Application.Pipelines.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace UserEventsChallenge.API.Business.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;
        private readonly List<string> _cacheKeys;

        public MemoryCacheManager(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
            _cacheKeys = new();
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object value)
        {
            _memoryCache.Set(key, value, TimeSpan.FromDays(_cacheSettings.Duration));
            _cacheKeys.Add(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            _cacheKeys.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keysToRemove = _cacheKeys.Where(c=>c.Contains(pattern)).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                _cacheKeys.Remove(key);
            }
        }
    }
}
