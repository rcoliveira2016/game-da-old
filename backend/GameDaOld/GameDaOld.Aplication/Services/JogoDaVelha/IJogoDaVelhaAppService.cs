namespace GameDaOld.Aplication.JogoDaVelha;

public interface IJogoDaVelhaAppService
{
    void ConectarPartida(string identificador, string jogadorID);
    void IniciarNovoJogo(string identificador, string jogadorID);
    SessaoJogoVelha? SetarJogada(string identificador, string jogadorID, int linha, int coluna);
}
