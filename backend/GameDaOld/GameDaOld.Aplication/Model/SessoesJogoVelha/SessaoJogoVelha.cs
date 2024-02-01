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


    public SessaoJogoVelha AdicionarJogador(eJogadorSessaoJogoVelha jogador, string id)
    {
        this.Jogadores.TryAdd(jogador, id);
        return this;
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
