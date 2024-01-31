
using GameDaOld.Aplication;
using GameDaOld.Aplication.JogoDaVelha;

namespace Microsoft.Extensions.DependencyInjection;
public static class MapServiceCollectionExtensions
{

    public static IServiceCollection AddInjecoesDepedencias(
         this IServiceCollection services)
    {
        services.AddScoped<IJogoDaVelhaAppService, JogoDaVelhaAppService>();

        return services;
    }
}