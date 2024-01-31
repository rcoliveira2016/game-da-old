
using GameDaOld.Aplication;
using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Infra.Integration.CacheService;

namespace Microsoft.Extensions.DependencyInjection;
public static class MapServiceCollectionExtensions
{

    public static IServiceCollection AddInjecoesDepedencias(
         this IServiceCollection services)
    {
        services.AddScoped<IJogoDaVelhaAppService, JogoDaVelhaAppService>();
        services.AddIoCCacheService();
        return services;
    }
}