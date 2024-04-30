using GameDaOld.Aplication.SessoesJogoVelha.InputModel;
using GameDaOld.Aplication.SessoesJogoVelha.OutputModel;

namespace GameDaOld.Aplication.SessoesJogoVelha.Services;

public interface IJogoDaVelhaAppService
{
    void ConectarPartida(IndentificadorJogoVelhaInputModel inputModel);
    IniciarJogoOutputModel IniciarNovoJogo(IniciarJogoVelhaInputModel inputModel);
    JogadaSetadaOutputModel? SetarJogada(SetarJogadaInputModel inputModel);
    EncerrarJogoOutputModel? EncerrarJogo(string identificador);
}
