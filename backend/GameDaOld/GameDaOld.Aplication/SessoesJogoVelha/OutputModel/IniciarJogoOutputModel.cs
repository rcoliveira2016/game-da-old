using GameDaOld.Domain.SessoesJogoVelha.Model;

namespace GameDaOld.Aplication.SessoesJogoVelha.OutputModel;

public class IniciarJogoOutputModel
{
    public string Identificador { get; set; } = string.Empty;

    internal static IniciarJogoOutputModel Create(SessaoJogoVelha sessao)
    {
        return new()
        {
            Identificador = sessao.Identificador,
        };
    }
}
