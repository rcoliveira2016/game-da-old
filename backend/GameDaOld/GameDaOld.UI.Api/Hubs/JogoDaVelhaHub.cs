using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Infra.Integration.CacheService;
using Microsoft.AspNetCore.SignalR;

namespace GameDaOld.UI.Api.Hubs;

public class JogoDaVelhaHub : Hub
{
    public readonly IJogoDaVelhaAppService _jogoDaVelhaAppService;
    public readonly ICacheService _cacheService;
    public JogoDaVelhaHub(IJogoDaVelhaAppService jogoDaVelhaAppService, ICacheService cacheService)
    {
        _jogoDaVelhaAppService = jogoDaVelhaAppService;
        _cacheService = cacheService;
    }
    public async Task SendMessage(JogoDaVelhaHubInputModel jogadaInpitModel)
    {
        _cacheService.SetString("Teste", "Teste");
        _jogoDaVelhaAppService.Teste();
        jogadaInpitModel.IndexLinha = 3;
        jogadaInpitModel.IndexColuna = 3;
        await Clients.All.SendAsync("ReceiveMessage", jogadaInpitModel);
    }
}
