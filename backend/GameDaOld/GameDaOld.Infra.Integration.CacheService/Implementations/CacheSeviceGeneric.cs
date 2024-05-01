using GameDaOld.Infra.Integration.CacheService.Common;
using MessagePack;
using Microsoft.Extensions.Caching.Distributed;

namespace GameDaOld.Infra.Integration.CacheService;

public class CacheSeviceGeneric : ICacheService
{

    private readonly IDistributedCache _distributedCache;

    private static DistributedCacheEntryOptions slidingExpiration = new();

    public CacheSeviceGeneric(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task SetStringAsync(string key, string value, CacheOptions? cacheOptions = null)
    {
        await _distributedCache.SetStringAsync(key, value, ObterCache(cacheOptions));
    }

    public async Task SetSerializableAsync<T>(string key, T value, CacheOptions? cacheOptions = null)
    {
        byte[] bytes = MessagePackSerializer.Serialize(value);
        await _distributedCache.SetAsync(key, bytes, ObterCache(cacheOptions));
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    public async Task<T> GetSerializableAsync<T>(string key)
    {
        var bytes = await _distributedCache.GetAsync(key);
        if(bytes == null) return default(T);
        return MessagePackSerializer.Deserialize<T>(bytes);
    }

    private DistributedCacheEntryOptions ObterCache(CacheOptions? cacheOptions)
    {
        if (cacheOptions == null) return slidingExpiration;
        return new()
        {
            AbsoluteExpirationRelativeToNow = cacheOptions.AbsoluteExpirationRelativeToNow,
            SlidingExpiration = cacheOptions.SlidingExpiration
        };
    }
}
