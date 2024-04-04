﻿using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Aplication.Services.JogoDaVelha.InputModel;
using GameDaOld.Aplication.Services.JogoDaVelha.OutputModel;
using GameDaOld.Domain.Core;
using GameDaOld.Infra.Integration.CacheService;
using static GameDaOld.Aplication.JogoVelhaHelper;

namespace GameDaOld.Aplication;

public class JogoDaVelhaAppService : IJogoDaVelhaAppService
{
    private string _namespaceKeyIndentificadorSeesao = "IndentificadorSessao";
    public readonly ICacheService _cacheService;
    private readonly IDomainNotificationHandler _domainNotificationHandler;
    public JogoDaVelhaAppService(ICacheService cacheService, IDomainNotificationHandler domainNotificationHandler)
    {
        _cacheService = cacheService;
        _domainNotificationHandler = domainNotificationHandler;
    }

    public SessaoJogoVelha? IniciarNovoJogo(IniciarJogoVelhaInputModel inputModel)
    {
        var identificador = Guid.NewGuid().ToString();

        var sessao = new SessaoJogoVelha
        {
            Identificador = identificador,
            Status = eStatusSessaoJogoVelha.Iniciando,
            JogadorAtual = eJogadorSessaoJogoVelha.X
        }.AdicionarJogador(eJogadorSessaoJogoVelha.X, inputModel.JogadorID);

        CommitSessao(sessao);

        return sessao;
    }

    public void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel)
    {
        if (!Validar(inputModel)) return;
        if (!TentarObterSessao(inputModel.Identificador, out var sessao)) 
        { 
            _domainNotificationHandler.NotifyError("A partida não foi iniciada");
            return; 
        }
        if(sessao.Jogadores.Count>1) {
            _domainNotificationHandler.NotifyError("Partida ja iniciada");
            return;
        };

        sessao.AdicionarJogador(eJogadorSessaoJogoVelha.O, inputModel.JogadorID);
        sessao.Status = eStatusSessaoJogoVelha.Iniciando;
        CommitSessao(sessao);
    }

    public JogadaSetadaOutputModel? SetarJogada(SetarJogadaInputModel inputModel)
    {
        if (!TentarObterSessao(inputModel.Identificador, out var sessao)) return null;
        if (sessao.Status == eStatusSessaoJogoVelha.EmAndamento)
            return null;
        CommitStatusSessao(sessao, eStatusSessaoJogoVelha.EmAndamento);

        var jogadorNovo = sessao.ObterTipoJogador(inputModel.JogadorID);
        if (!ValidarJogada(sessao, jogadorNovo, inputModel))
        {
            CommitStatusSessao(sessao, eStatusSessaoJogoVelha.FinalizadoJogada);
            return null;
        }

        sessao.JogadorAtual = jogadorNovo;
        sessao.NumerosDeJogadas++;
        sessao.SetarJogadaTabuleiro(inputModel.Linha, inputModel.Coluna, jogadorNovo);

        sessao.Status = eStatusSessaoJogoVelha.FinalizadoJogada;
        CommitSessao(sessao);
        return JogadaSetadaOutputModel.Create(sessao);
    }

    private bool ValidarJogada(SessaoJogoVelha sessao, eJogadorSessaoJogoVelha jogadorNovo, SetarJogadaInputModel inputModel)
    {
        return sessao.ValidarJogada(inputModel.Linha, inputModel.Coluna, jogadorNovo);
    }

    private void CommitStatusSessao(SessaoJogoVelha sessaoJogoVelha, eStatusSessaoJogoVelha status)
    {
        sessaoJogoVelha.Status = status;
        CommitSessao(sessaoJogoVelha);
    }

    private void CommitSessao(SessaoJogoVelha sessaoJogoVelha)
    {
        _cacheService.SetSerializable(ObterCacheKeyIndentificador(sessaoJogoVelha.Identificador), sessaoJogoVelha);
    }

    private SessaoJogoVelha ObterSessao(string identificador)
    {
        return _cacheService.GetSerializable<SessaoJogoVelha>(ObterCacheKeyIndentificador(identificador));
    }

    private string ObterCacheKeyIndentificador(string identificador)
    {
        return $"{_namespaceKeyIndentificadorSeesao}:{identificador}";
    }

    private bool TentarObterSessao(string identificador, out SessaoJogoVelha sessaoJogoVelha)
    {
        sessaoJogoVelha = ObterSessao(identificador);
        return sessaoJogoVelha != null;
    }
    private bool Validar(IndentificadorJogoVelhaInputModel inputModel)
    {

        if (string.IsNullOrEmpty(inputModel.Identificador))
        {
            _domainNotificationHandler.NotifyError("Identificar está vazio");
            return false;
        }

        if (string.IsNullOrEmpty(inputModel.JogadorID))
        {
            _domainNotificationHandler.NotifyError("JogadorID está vazio");
            return false;
        }

        return true;
    }

    public EncerrarJogoOutputModel? EncerrarJogo(string identificador)
    {
        if (string.IsNullOrEmpty(identificador))
        {
            _domainNotificationHandler.NotifyError("Identificar está vazio");
            return null;
        }
        if (!TentarObterSessao(identificador, out var sessao))
        {
            _domainNotificationHandler.NotifyError("A partida não foi iniciada");
            return null;
        }

        return new()
        {
            IdentificadoresJogadores = sessao.Jogadores.Values.ToArray()
        };
    }
}
