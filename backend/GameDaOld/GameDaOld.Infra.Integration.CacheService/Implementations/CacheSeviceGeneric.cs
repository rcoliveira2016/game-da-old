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

    public void Add<T>(string key, T value)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public void Update(string key, string value)
    {
        throw new NotImplementedException();
    }
}
