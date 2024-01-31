using Microsoft.Extensions.DependencyInjection;

namespace GameDaOld.Infra.Integration.CacheService;

public static class IoC
{
    public static IServiceCollection AddIoCCacheService(
        this IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheSeviceGeneric>();

        return services;
    }
}
