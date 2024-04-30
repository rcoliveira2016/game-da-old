using GameDaOld.Domain.SessoesJogoVelha.Model;

namespace GameDaOld.Aplication.SessoesJogoVelha.OutputModel
{
    public class JogadaSetadaOutputModel
    {
        public string[][] Tabuleiro { get; set; } = new string[3][];
        public string ProximoJogador { get; set; } = string.Empty;
        public string? Vencedor { get; set; }
        internal static JogadaSetadaOutputModel? Create(SessaoJogoVelha? sessao) =>
            sessao == null ? null : new()
            {
                Vencedor = sessao.ObterVencedor().ToString()?.ToUpper(),
                ProximoJogador = sessao.ProximoJogador().ToString().ToUpper(),
                Tabuleiro = ObterBoard(sessao)
            };
        private static string[][] ObterBoard(SessaoJogoVelha sessao)
        {
            var retorno = new string[3][]{
                new string[3],
                new string[3],
                new string[3],
            };

            for (int i = 0; i < sessao.Tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < sessao.Tabuleiro.GetLength(1); j++)
                {
                    var valor = sessao.Tabuleiro[i, j];
                    if (valor == eJogadorSessaoJogoVelha.Nenhum) continue;
                    retorno[i][j] = sessao.Tabuleiro[i, j].ToString().ToUpper();
                }
            }

            return retorno;
        }
    }
}
