using Microsoft.AspNetCore.SignalR;

namespace GameDaOld.UI.Api.Hubs;

public class JogoDaVelhaHub : Hub
{
    public async Task SendMessage(JogoDaVelhaHubInputModel jogadaInpitModel)
    {
        jogadaInpitModel.IndexLinha = 3;
        jogadaInpitModel.IndexColuna = 3;
        await Clients.All.SendAsync("ReceiveMessage", jogadaInpitModel);
    }
}
