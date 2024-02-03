using MessagePack;

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

    public eVencedorSessaoJogoVelha? ObterVencedor()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Tabuleiro[i,0] != eJogadorSessaoJogoVelha.Nenhum &&
                Tabuleiro[i,0] == Tabuleiro[i,1] &&
                Tabuleiro[i,1] == Tabuleiro[i,2])
            {
                return Tabuleiro[i,0] == eJogadorSessaoJogoVelha.X? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
            }

            if (Tabuleiro[0,i] != eJogadorSessaoJogoVelha.Nenhum &&
                Tabuleiro[0,i] == Tabuleiro[1,i] &&
                Tabuleiro[1,i] == Tabuleiro[2,i])
            {
                return Tabuleiro[0,i] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
            }
        }

        if (Tabuleiro[0,0] != eJogadorSessaoJogoVelha.Nenhum &&
            Tabuleiro[0,0] == Tabuleiro[1,1] &&
            Tabuleiro[1,1] == Tabuleiro[2,2])
        {
            return Tabuleiro[0,0] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
        }

        if (Tabuleiro[0,2] != eJogadorSessaoJogoVelha.Nenhum &&
            Tabuleiro[0,2] == Tabuleiro[1,1] &&
            Tabuleiro[1,1] == Tabuleiro[2,0])
        {
            return Tabuleiro[0,2] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Tabuleiro[i,j] == eJogadorSessaoJogoVelha.Nenhum)
                {
                    return null;
                }
            }
        }

        return eVencedorSessaoJogoVelha.Empate; // empate
    }
}
public enum eVencedorSessaoJogoVelha
{
    Empate,
    X,
    O
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
