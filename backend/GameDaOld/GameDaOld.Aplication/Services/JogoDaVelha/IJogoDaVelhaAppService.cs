using GameDaOld.Aplication.Services.JogoDaVelha.InputModel;
using GameDaOld.Aplication.Services.JogoDaVelha.OutputModel;

namespace GameDaOld.Aplication.JogoDaVelha;

public interface IJogoDaVelhaAppService
{
    void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel);
    SessaoJogoVelha? IniciarNovoJogo(IniciarJogoVelhaInputModel inputModel);
    JogadaSetadaOutputModel? SetarJogada(SetarJogadaInputModel inputModel);
    EncerrarJogoOutputModel? EncerrarJogo(string identificador);
}
