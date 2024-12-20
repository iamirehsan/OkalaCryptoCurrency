namespace OkalaCryptoCurrency.Domain.Interfaces.CacheService
{
    public interface ICacheService
    {
        public Task<T> GetDataFromCacheAsync<T>(string key);
        public Task SetOrUpdateDataInCacheAsync<T>(T data, string key);

    }
}
