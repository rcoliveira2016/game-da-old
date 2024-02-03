using System.Security.Cryptography.X509Certificates;
using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Infra.Integration.CacheService;
using static GameDaOld.Aplication.JogoVelhaHelper;

namespace GameDaOld.Aplication;

public class JogoDaVelhaAppService : IJogoDaVelhaAppService
{
    public readonly ICacheService _cacheService;
    public JogoDaVelhaAppService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public void IniciarNovoJogo(IndentificadorJogoVelhaInputModel inputModel)
    {
        var sessao = new SessaoJogoVelha
        {
            Identificador = inputModel.Identificador,
            Status = eStatusSessaoJogoVelha.Iniciando,
            JogadorAtual = eJogadorSessaoJogoVelha.X
        }.AdicionarJogador(eJogadorSessaoJogoVelha.X, inputModel.JogadorID);

        CommitSessao(sessao);
    }

    public void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel)
    {
        if (!TentarObterSessao(inputModel.Identificador, out var sessao)) return;

        sessao.AdicionarJogador(eJogadorSessaoJogoVelha.O, inputModel.JogadorID);
        sessao.Status = eStatusSessaoJogoVelha.Iniciando;
        CommitSessao(sessao);
    }

    public SessaoJogoVelha? SetarJogada(SetarJogadaInputModel inputModel)
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
        return sessao;
    }

    private bool ValidarJogada(SessaoJogoVelha sessao, eJogadorSessaoJogoVelha jogadorNovo, SetarJogadaInputModel inputModel)
    {
        if (jogadorNovo == sessao.JogadorAtual && sessao.NumerosDeJogadas > 1) return false;
        if (sessao.ObterVencedor() != eVencedorSessaoJogoVelha.JogoEmAndamento) return false;

        return true;
    }

    private void CommitStatusSessao(SessaoJogoVelha sessaoJogoVelha, eStatusSessaoJogoVelha status)
    {
        sessaoJogoVelha.Status = status;
        CommitSessao(sessaoJogoVelha);
    }

    private void CommitSessao(SessaoJogoVelha sessaoJogoVelha)
    {
        _cacheService.SetSerializable(sessaoJogoVelha.Identificador, sessaoJogoVelha);
    }

    private SessaoJogoVelha ObterSessao(string identificador)
    {
        return _cacheService.GetSerializable<SessaoJogoVelha>(identificador);
    }

    private bool TentarObterSessao(string identificador, out SessaoJogoVelha sessaoJogoVelha)
    {
        sessaoJogoVelha = ObterSessao(identificador);
        return sessaoJogoVelha != null;
    }
}
