using GameDaOld.Infra.Integration.CacheService.Common;
using System.Threading.Tasks;

namespace GameDaOld.Infra.Integration.CacheService;

public interface ICacheService
{
    Task<T> GetSerializableAsync<T>(string key);
    Task RemoveAsync(string key);
    Task SetStringAsync(string key, string value, CacheOptions? cacheOptions = null);
    Task SetSerializableAsync<T>(string key, T value, CacheOptions? cacheOptions = null);
}
