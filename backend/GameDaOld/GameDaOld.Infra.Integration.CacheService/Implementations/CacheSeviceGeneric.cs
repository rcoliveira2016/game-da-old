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

    public void SetString(string key, string value, CacheOptions? cacheOptions = null)
    {
        _distributedCache.SetString(key, value, ObterCache(cacheOptions));
    }

    public void SetSerializable<T>(string key, T value, CacheOptions? cacheOptions = null)
    {
        byte[] bytes = MessagePackSerializer.Serialize(value);
        _distributedCache.Set(key, bytes, ObterCache(cacheOptions));
    }

    public void Remove(string key)
    {
        _distributedCache.Remove(key);
    }

    public T GetSerializable<T>(string key)
    {
        var bytes = _distributedCache.Get(key);
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
