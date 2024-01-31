namespace GameDaOld.Infra.Integration.CacheService;

public interface ICacheService
{
    void SetString(string key, string value);
    void Add<T>(string key, T value);
    void Remove(string key);
}
