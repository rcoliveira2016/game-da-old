using System.Security.Cryptography.X509Certificates;
using GameDaOld.Aplication.JogoDaVelha;
using GameDaOld.Infra.Integration.CacheService;

namespace GameDaOld.Aplication;

public class JogoDaVelhaAppService : IJogoDaVelhaAppService
{
    public readonly ICacheService _cacheService;
    public JogoDaVelhaAppService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public void IniciarNovoJogo(string identificador, string jogadorID)
    {
        var sessao = new SessaoJogoVelha
        {
            Identificador = identificador,
            Status = eStatusSessaoJogoVelha.Iniciando,
            JogadorAtual = eJogadorSessaoJogoVelha.X
        }.AdicionarJogador(eJogadorSessaoJogoVelha.X, jogadorID);

        CommitSessao(sessao);
    }

    public void ConectarPartida(string identificador, string jogadorID)
    {
        var sessao = ObterSessao(identificador);
        if (sessao == null) return;
        sessao.AdicionarJogador(eJogadorSessaoJogoVelha.O, jogadorID);
        sessao.Status = eStatusSessaoJogoVelha.Iniciando;
        CommitSessao(sessao);
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

    public SessaoJogoVelha? SetarJogada(string identificador, string jogadorID, int linha, int coluna)
    {
        var sessao = ObterSessao(identificador);
        if(sessao.Status == eStatusSessaoJogoVelha.EmAndamento)
            return null;
        
        var jogadorNovo = sessao.ObterTipoJogador(jogadorID);
        if (jogadorNovo == sessao.JogadorAtual && sessao.NumerosDeJogadas > 1) return null;
        if (sessao.ObterVencedor().HasValue) return null;

        sessao.JogadorAtual = jogadorNovo;
        sessao.Status = eStatusSessaoJogoVelha.EmAndamento;
        sessao.NumerosDeJogadas++;
        CommitSessao(sessao);

        if (sessao.Tabuleiro[linha, coluna] == eJogadorSessaoJogoVelha.Nenhum)
            sessao.Tabuleiro[linha, coluna] = sessao.ObterTipoJogador(jogadorID);

        sessao.Status = eStatusSessaoJogoVelha.FinalizadoJogada;
        CommitSessao(sessao);
        return sessao;
    }
}
