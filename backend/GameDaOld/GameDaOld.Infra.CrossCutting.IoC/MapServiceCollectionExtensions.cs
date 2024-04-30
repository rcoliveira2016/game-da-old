using GameDaOld.Aplication.SessoesJogoVelha.Services;
using GameDaOld.Domain.Core;
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
        return services;
    }
}