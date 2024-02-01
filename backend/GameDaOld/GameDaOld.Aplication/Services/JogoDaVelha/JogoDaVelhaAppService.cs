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

    public void IniciarJogo(string identificador, string jogadorID)
    {
        var sessao = new SessaoJogoVelha
        {
            Identificador = identificador,
            Status = eStatusSessaoJogoVelha.Iniciando,
            JogadorAtual = eJogadorSessaoJogoVelha.X
        }.AdicionarJogador(eJogadorSessaoJogoVelha.X, jogadorID);

        CommitSessao(sessao);
    }

    public void AdicionarJogador(string identificador, string jogadorID)
    {
        var sessao = ObterSessao(identificador);
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

    public void Jogar(string identificador, string jogadorID, int linha, int coluna)
    {
        var sessao = ObterSessao(identificador);
        sessao.Tabuleiro[linha, coluna] = eJogadorSessaoJogoVelha.X;
        CommitSessao(sessao);
    }
}
