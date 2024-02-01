namespace GameDaOld.Infra.Integration.CacheService;

public interface ICacheService
{
    void SetString(string key, string value);
    void SetSerializable<T>(string key, T value);
    T GetSerializable<T>(string key);
    void Remove(string key);
}
