namespace GameDaOld.Aplication.JogoDaVelha;

public interface IJogoDaVelhaAppService
{
    void AdicionarJogador(string identificador, string jogadorID);
    void IniciarJogo(string identificador, string jogadorID);
    void Jogar(string identificador, string jogadorID, int linha, int coluna);
}
