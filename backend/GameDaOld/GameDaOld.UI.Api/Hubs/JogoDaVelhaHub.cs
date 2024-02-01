using GameDaOld.Aplication;
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

    public async Task IniciarJogo()
    {        
        var identificador = Guid.NewGuid().ToString();
        _jogoDaVelhaAppService.IniciarJogo(this.Context.ConnectionId, identificador);
        await Groups.AddToGroupAsync(this.Context.ConnectionId, identificador);
        await Clients.Group(identificador).SendAsync("JogoIniciado", identificador);
    }

    public async Task AdicionarJogador(string identificador){
        await Groups.AddToGroupAsync(this.Context.ConnectionId, identificador);
        _jogoDaVelhaAppService.AdicionarJogador(this.Context.ConnectionId, identificador);
        await Clients.Group(identificador).SendAsync("JogoIniciado", identificador);
    }

    public async Task Jogar(int linha, int coluna)
    {
        
    }
}
