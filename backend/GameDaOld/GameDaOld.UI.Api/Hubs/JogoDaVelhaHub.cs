using Microsoft.AspNetCore.SignalR;

namespace GameDaOld.UI.Api.Hubs;

public class JogoDaVelhaHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
