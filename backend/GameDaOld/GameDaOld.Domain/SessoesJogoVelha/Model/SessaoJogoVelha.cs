using GameDaOld.Domain.SessoesJogoVelha.Helpers;
using MessagePack;
using static GameDaOld.Domain.SessoesJogoVelha.Helpers.JogoVelhaHelper;

namespace GameDaOld.Domain.SessoesJogoVelha.Model;

[MessagePackObject]
public class SessaoJogoVelha
{
    [Key(0)]
    public string Identificador { get; set; } = Guid.NewGuid().ToString();
    [Key(1)]
    public eStatusSessaoJogoVelha Status { get; set; }
    [Key(2)]
    public eJogadorSessaoJogoVelha JogadorAtual { get; set; }
    [Key(3)]
    public eJogadorSessaoJogoVelha[,] Tabuleiro { get; set; } = new eJogadorSessaoJogoVelha[3, 3];
    [Key(4)]
    public IDictionary<eJogadorSessaoJogoVelha, string> Jogadores { get; set; } = new Dictionary<eJogadorSessaoJogoVelha, string>();
    [Key(5)]
    public short NumerosDeJogadas { get; set; } = 0;

    public SessaoJogoVelha AdicionarJogador(eJogadorSessaoJogoVelha jogador, string id)
    {
        this.Jogadores.TryAdd(jogador, id);
        return this;
    }

    public eJogadorSessaoJogoVelha ObterTipoJogador(string jogador) =>
        this.Jogadores.FirstOrDefault(x => x.Value == jogador).Key;

    public eJogadorSessaoJogoVelha ProximoJogador() =>
        this.JogadorAtual == eJogadorSessaoJogoVelha.X ? eJogadorSessaoJogoVelha.O : eJogadorSessaoJogoVelha.X;

    public void SetarJogadaTabuleiro(short linha, short coluna, eJogadorSessaoJogoVelha jogador)
    {
        if (CelulaEstaVazio(linha, coluna))
            this.Tabuleiro[linha, coluna] = jogador;
    }

    public eVencedorSessaoJogoVelha ObterVencedor()
    {
        return JogoVelhaHelper.ObterVencedor(this.Tabuleiro);
    }

    public bool CelulaEstaVazio(short linha, short coluna)
    {
        return this.Tabuleiro[linha, coluna] == eJogadorSessaoJogoVelha.Nenhum;
    }

    public bool ValidarJogada(short linha, short coluna, eJogadorSessaoJogoVelha jogadorNovo)
    {
        if (NumerosDeJogadas == 0 && jogadorNovo != JogadorAtual) return false;
        if (NumerosDeJogadas > 0 && jogadorNovo == JogadorAtual) return false;
        if (!CelulaEstaVazio(linha, coluna)) return false;
        if (ObterVencedor() != eVencedorSessaoJogoVelha.JogoEmAndamento) return false;

        return true;
    }

    public static SessaoJogoVelha IniciarJogo()
    {
        var identificador = Guid.NewGuid().ToString();
        return new ()
        {
            Identificador = identificador,
            Status = eStatusSessaoJogoVelha.Iniciando,
            JogadorAtual = eJogadorSessaoJogoVelha.X
        };
    }
}
public enum eJogadorSessaoJogoVelha
{
    Nenhum,
    X,
    O
}
public enum eStatusSessaoJogoVelha
{
    laoud,
    Iniciando,
    EmAndamento,
    FinalizadoJogada
}
