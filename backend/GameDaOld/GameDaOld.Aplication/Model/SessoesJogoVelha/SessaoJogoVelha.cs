using MessagePack;
using static GameDaOld.Aplication.JogoVelhaHelper;

namespace GameDaOld.Aplication;
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
    public short NumerosDeJogadas { get; set; } = 1;

    public SessaoJogoVelha AdicionarJogador(eJogadorSessaoJogoVelha jogador, string id)
    {
        this.Jogadores.TryAdd(jogador, id);
        return this;
    }

    public eJogadorSessaoJogoVelha ObterTipoJogador(string jogador) => 
        this.Jogadores.FirstOrDefault(x => x.Value == jogador).Key;

    public eJogadorSessaoJogoVelha ProximoJogador() =>
        this.JogadorAtual == eJogadorSessaoJogoVelha.X ? eJogadorSessaoJogoVelha.O : eJogadorSessaoJogoVelha.X;

    public void SetarJogadaTabuleiro(int linha, int coluna, eJogadorSessaoJogoVelha jogador)
    {
        if(this.Tabuleiro[linha, coluna] == eJogadorSessaoJogoVelha.Nenhum)
            this.Tabuleiro[linha, coluna] = jogador;
    }

    public eVencedorSessaoJogoVelha ObterVencedor()
    {
        return JogoVelhaHelper.ObterVencedor(this.Tabuleiro);
    }
}
public enum eJogadorSessaoJogoVelha
{
    Nenhum,
    X,
    O
}
public enum eStatusSessaoJogoVelha{
    laoud,
    Iniciando,
    EmAndamento,
    FinalizadoJogada
}
