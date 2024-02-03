using MessagePack;
using Microsoft.Extensions.Caching.Distributed;

namespace GameDaOld.Infra.Integration.CacheService;

public class CacheSeviceGeneric : ICacheService
{

    private readonly IDistributedCache _distributedCache;

    private static DistributedCacheEntryOptions slidingExpiration = new()
    {
        SlidingExpiration = TimeSpan.FromSeconds(5)
    };

    public CacheSeviceGeneric(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;    
    }

    public void SetString(string key, string value)
    {
        _distributedCache.SetString(key, value);
    }

    public void SetSerializable<T>(string key, T value)
    {
        byte[] bytes = MessagePackSerializer.Serialize(value);
        _distributedCache.Set(key, bytes);
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public T GetSerializable<T>(string key)
    {
        var bytes = _distributedCache.Get(key);
        if(bytes == null) return default(T);
        return MessagePackSerializer.Deserialize<T>(bytes);
    }
}
