using Microsoft.Extensions.Caching.Memory;
using OkalaCryptoCurrency.Domain.Interfaces.CacheService;

namespace OkalaCryptoCurrency.Infrastructure.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly MemoryCache _cache;

        public CacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }


        public Task<T> GetDataFromCacheAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out T cachedData))
            {
                return Task.FromResult(cachedData);
            }

            return null;
        }


        public Task SetOrUpdateDataInCacheAsync<T>(T data, string key)
        {
            _cache.Set(key , data);
            return Task.CompletedTask;
        }
    }

}
