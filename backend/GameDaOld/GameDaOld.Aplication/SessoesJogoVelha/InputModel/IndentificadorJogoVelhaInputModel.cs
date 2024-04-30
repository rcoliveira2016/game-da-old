namespace GameDaOld.Aplication.SessoesJogoVelha.InputModel;

public class IndentificadorJogoVelhaInputModel : IndentificadorJogoVelhaBase
{
    public static IndentificadorJogoVelhaInputModel Create(string identificador, string jogadorID) =>
        new() { Identificador = identificador, JogadorID = jogadorID };
}
