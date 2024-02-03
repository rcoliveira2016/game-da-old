using Microsoft.Extensions.WebEncoders.Testing;

namespace GameDaOld.UI.Api;

public class JogoDaVelhaHubInputModel
{
    public string Identificador { get; set; } = string.Empty;
    public short IndexLinha { get; set; }
    public short IndexColuna { get; set; }
}
