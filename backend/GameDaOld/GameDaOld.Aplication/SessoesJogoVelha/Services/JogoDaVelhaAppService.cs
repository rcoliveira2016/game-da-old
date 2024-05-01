using GameDaOld.Aplication.SessoesJogoVelha.InputModel;
using GameDaOld.Aplication.SessoesJogoVelha.OutputModel;
using GameDaOld.Domain.Core;
using GameDaOld.Domain.SessoesJogoVelha.Model;
using GameDaOld.Domain.SessoesJogoVelha.Repository;
using GameDaOld.Infra.Integration.CacheService;

namespace GameDaOld.Aplication.SessoesJogoVelha.Services;

public class JogoDaVelhaAppService : IJogoDaVelhaAppService
{
    public readonly ISessaoJogoVelhaCacheRepository _sessaoJogoVelhaCacheRepository;
    private readonly IDomainNotificationHandler _domainNotificationHandler;
    public JogoDaVelhaAppService(ISessaoJogoVelhaCacheRepository sessaoJogoVelhaCacheRepository, IDomainNotificationHandler domainNotificationHandler)
    {
        _sessaoJogoVelhaCacheRepository = sessaoJogoVelhaCacheRepository;
        _domainNotificationHandler = domainNotificationHandler;
    }

    public IniciarJogoOutputModel IniciarNovoJogo(IniciarJogoVelhaInputModel inputModel)
    {
        var sessao = SessaoJogoVelha.IniciarJogo();
        sessao.AdicionarJogador(eJogadorSessaoJogoVelha.X, inputModel.JogadorID);

        CommitSessao(sessao);

        return IniciarJogoOutputModel.Create(sessao);
    }

    public void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel)
    {
        if (!Validar(inputModel)) return;
        if (!TentarObterSessao(inputModel.Identificador, out var sessao))
        {
            _domainNotificationHandler.NotifyError("A partida não foi iniciada");
            return;
        }
        if (sessao.Jogadores.Count > 1)
        {
            _domainNotificationHandler.NotifyError("Partida ja iniciada");
            return;
        }

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
        _sessaoJogoVelhaCacheRepository.Set(sessaoJogoVelha.Identificador, sessaoJogoVelha);
    }


    private bool TentarObterSessao(string identificador, out SessaoJogoVelha sessaoJogoVelha)
    {
        var sessao = _sessaoJogoVelhaCacheRepository.Get(identificador).Result;
        if(sessao == null)
        {
            sessaoJogoVelha = null;
            return false;
        }

        sessaoJogoVelha = sessao;
        return true;
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
