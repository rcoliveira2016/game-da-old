using GameDaOld.Infra.Integration.CacheService.Common;

namespace GameDaOld.Infra.Integration.CacheService;

public interface ICacheService
{
    T GetSerializable<T>(string key);
    void Remove(string key);
    void SetString(string key, string value, CacheOptions? cacheOptions = null);
    void SetSerializable<T>(string key, T value, CacheOptions? cacheOptions = null);
}
