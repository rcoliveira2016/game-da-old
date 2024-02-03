using Microsoft.Extensions.WebEncoders.Testing;

namespace GameDaOld.UI.Api;

public class JogoDaVelhaHubInputModel
{
    public string Identificador { get; set; } = string.Empty;
    public int IndexLinha { get; set; }
    public int IndexColuna { get; set; }
}
