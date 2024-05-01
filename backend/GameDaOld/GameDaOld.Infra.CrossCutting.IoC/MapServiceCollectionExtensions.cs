using GameDaOld.Aplication.SessoesJogoVelha.Services;
using GameDaOld.Domain.Core;
using GameDaOld.Domain.SessoesJogoVelha.Repository;
using GameDaOld.Infra.Data.Repository.Cache.Repository;
using GameDaOld.Infra.Integration.CacheService;

namespace Microsoft.Extensions.DependencyInjection;
public static class MapServiceCollectionExtensions
{

    public static IServiceCollection AddInjecoesDepedencias(
         this IServiceCollection services)
    {
        services.AddScoped<IJogoDaVelhaAppService, JogoDaVelhaAppService>();
        services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        services.AddIoCCacheService();

        MapCacheRepository(services);

        return services;
    }

    private static void MapCacheRepository(IServiceCollection services)
    {
        services.AddScoped<ISessaoJogoVelhaCacheRepository, SessaoJogoVelhaCachRepository>();
    }
}