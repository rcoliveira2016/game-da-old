using GameDaOld.Aplication;
using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Aplication.Services.JogoDaVelha.InputModel;
using GameDaOld.Domain.Core;
using GameDaOld.Infra.Integration.CacheService;
using Microsoft.AspNetCore.SignalR;

namespace GameDaOld.UI.Api.Hubs;

public class JogoDaVelhaHub : Hub
{
    private readonly IJogoDaVelhaAppService _jogoDaVelhaAppService;
    private readonly IDomainNotificationHandler _domainNotificationHandler;

    private const string methodJogoAberto = "JogoAberto";
    private const string methodJogoIniciado = "JogoIniciado";
    private const string methodSetarJogada = "SetarJogada";
    private const string methodNotificacoesDominio = "NotificacoesDominio";

    public JogoDaVelhaHub(IJogoDaVelhaAppService jogoDaVelhaAppService, IDomainNotificationHandler domainNotificationHandler)
    {
        _jogoDaVelhaAppService = jogoDaVelhaAppService;
        _domainNotificationHandler = domainNotificationHandler;
    }

    public async Task IniciarNovoJogo()
    {
        var sessao = _jogoDaVelhaAppService.IniciarNovoJogo(IniciarJogoVelhaInputModel.Create(this.Context.ConnectionId));
        if (!await ValidarNotifiacoesDominio() || sessao==null) return;

        await Groups.AddToGroupAsync(this.Context.ConnectionId, sessao.Identificador);
        await Clients.Group(sessao.Identificador).SendAsync(methodJogoAberto, sessao.Identificador);
    }

    public async Task ConectarPartida(string identificador)
    {
        _jogoDaVelhaAppService.ConectarPartida(IndentificadorJogoVelhaInputModel.Create(identificador, this.Context.ConnectionId));
        if (!await ValidarNotifiacoesDominio()) return;

        await Groups.AddToGroupAsync(this.Context.ConnectionId, identificador);
        await Clients.Group(identificador).SendAsync(methodJogoIniciado, identificador);
    }

    public async Task SetarJogada(JogoDaVelhaHubInputModel jogoDaVelhaHubInputModel)
    {
        var outputModel = _jogoDaVelhaAppService.SetarJogada(
            new ()
            {
                Coluna = jogoDaVelhaHubInputModel.IndexColuna,
                Linha = jogoDaVelhaHubInputModel.IndexLinha,
                JogadorID = this.Context.ConnectionId,
                Identificador = jogoDaVelhaHubInputModel.Identificador
            }
        );
        if(!await ValidarNotifiacoesDominio()) return;
        if (outputModel == null) return;

        await Clients
            .Group(jogoDaVelhaHubInputModel.Identificador)
            .SendAsync(
                methodSetarJogada,
                outputModel);
    }

    private async Task<bool> ValidarNotifiacoesDominio(){
        var erros = _domainNotificationHandler.GetNotificationsError();
        if (!erros.Any()) return true;

        await Clients.Caller.SendAsync(methodNotificacoesDominio, erros);
        return false;
    }

}
