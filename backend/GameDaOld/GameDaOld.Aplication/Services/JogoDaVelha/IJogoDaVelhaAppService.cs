namespace GameDaOld.Aplication.JogoDaVelha;

public interface IJogoDaVelhaAppService
{
    void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel);
    void IniciarNovoJogo(IndentificadorJogoVelhaInputModel inputModel);
    SessaoJogoVelha? SetarJogada(SetarJogadaInputModel inputModel);
}
