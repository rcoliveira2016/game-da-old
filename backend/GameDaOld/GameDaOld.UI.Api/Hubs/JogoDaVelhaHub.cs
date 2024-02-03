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

    public async Task IniciarNovoJogo()
    {
        var identificador = Guid.NewGuid().ToString();
        _jogoDaVelhaAppService.IniciarNovoJogo(IndentificadorJogoVelhaInputModel.Create(identificador, this.Context.ConnectionId));
        await Groups.AddToGroupAsync(this.Context.ConnectionId, identificador);
        await Clients.Group(identificador).SendAsync("JogoAberto", identificador);
    }

    public async Task ConectarPartida(string identificador)
    {
        _jogoDaVelhaAppService.ConectarPartida(IndentificadorJogoVelhaInputModel.Create(identificador, this.Context.ConnectionId));
        await Groups.AddToGroupAsync(this.Context.ConnectionId, identificador);
        await Clients.Group(identificador).SendAsync("JogoIniciado", identificador);
    }

    public async Task SetarJogada(JogoDaVelhaHubInputModel jogoDaVelhaHubInputModel)
    {
        var sessao = _jogoDaVelhaAppService.SetarJogada(
            new ()
            {
                Coluna = jogoDaVelhaHubInputModel.IndexColuna,
                Linha = jogoDaVelhaHubInputModel.IndexLinha,
                JogadorID = this.Context.ConnectionId,
                Identificador = jogoDaVelhaHubInputModel.Identificador
            }
        );
        if (sessao == null) return;

        await Clients.Group(jogoDaVelhaHubInputModel.Identificador)
            .SendAsync("SetarJogada", ObterBoard(sessao), sessao.ProximoJogador().ToString().ToUpper(), sessao.ObterVencedor().ToString().ToUpper());
    }

    private string[][] ObterBoard(SessaoJogoVelha? sessao)
    {
        if (sessao == null) return null;
        var retorno = new string[3][]{
            new string[3],
            new string[3],
            new string[3],
        };
        for (int i = 0; i < sessao.Tabuleiro.GetLength(0); i++)
        {
            for (int j = 0; j < sessao.Tabuleiro.GetLength(1); j++)
            {
                var valor = sessao.Tabuleiro[i, j];
                if (valor == eJogadorSessaoJogoVelha.Nenhum) continue;
                retorno[i][j] = sessao.Tabuleiro[i, j].ToString().ToUpper();
            }
        }

        return retorno;
    }
}
