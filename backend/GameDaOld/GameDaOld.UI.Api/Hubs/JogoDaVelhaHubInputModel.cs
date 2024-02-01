using Microsoft.Extensions.WebEncoders.Testing;

namespace GameDaOld.UI.Api;

public class JogoDaVelhaHubInputModel
{
    public int IndexLinha { get; set; }
    public int IndexColuna { get; set; }
    public string[][] Board { get; set; } = new string[3][]{
        new string[3],
        new string[3],
        new string[3]
    };

    public void ttete(){
        Board[1][2] = "X";
    }
}
