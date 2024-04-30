namespace GameDaOld.Aplication.SessoesJogoVelha.InputModel
{
    public class IniciarJogoVelhaInputModel
    {
        public string JogadorID { get; set; } = string.Empty;

        public static IniciarJogoVelhaInputModel Create(string connectionId) =>
            new()
            {
                JogadorID = connectionId
            };
    }
}
