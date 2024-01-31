using GameDaOld.Aplication.JogoDaVelha;
using Microsoft.AspNetCore.SignalR;

namespace GameDaOld.UI.Api.Hubs;

public class JogoDaVelhaHub : Hub
{
    public readonly IJogoDaVelhaAppService _jogoDaVelhaAppService;
    public JogoDaVelhaHub(IJogoDaVelhaAppService jogoDaVelhaAppService)
    {
        _jogoDaVelhaAppService = jogoDaVelhaAppService;
    }
    public async Task SendMessage(JogoDaVelhaHubInputModel jogadaInpitModel)
    {
        _jogoDaVelhaAppService.Teste();
        jogadaInpitModel.IndexLinha = 3;
        jogadaInpitModel.IndexColuna = 3;
        await Clients.All.SendAsync("ReceiveMessage", jogadaInpitModel);
    }
}
