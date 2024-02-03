namespace GameDaOld.Aplication;

public static class JogoVelhaHelper
{
    public static eVencedorSessaoJogoVelha ObterVencedor(eJogadorSessaoJogoVelha[,] tabuleiro)
    {
        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i, 0] != eJogadorSessaoJogoVelha.Nenhum &&
                tabuleiro[i, 0] == tabuleiro[i, 1] &&
                tabuleiro[i, 1] == tabuleiro[i, 2])
            {
                return tabuleiro[i, 0] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
            }

            if (tabuleiro[0, i] != eJogadorSessaoJogoVelha.Nenhum &&
                tabuleiro[0, i] == tabuleiro[1, i] &&
                tabuleiro[1, i] == tabuleiro[2, i])
            {
                return tabuleiro[0, i] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
            }
        }

        if (tabuleiro[0, 0] != eJogadorSessaoJogoVelha.Nenhum &&
            tabuleiro[0, 0] == tabuleiro[1, 1] &&
            tabuleiro[1, 1] == tabuleiro[2, 2])
        {
            return tabuleiro[0, 0] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
        }

        if (tabuleiro[0, 2] != eJogadorSessaoJogoVelha.Nenhum &&
            tabuleiro[0, 2] == tabuleiro[1, 1] &&
            tabuleiro[1, 1] == tabuleiro[2, 0])
        {
            return tabuleiro[0, 2] == eJogadorSessaoJogoVelha.X ? eVencedorSessaoJogoVelha.X : eVencedorSessaoJogoVelha.O;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == eJogadorSessaoJogoVelha.Nenhum)
                {
                    return eVencedorSessaoJogoVelha.JogoEmAndamento;
                }
            }
        }

        return eVencedorSessaoJogoVelha.Empate; // empate
    }
    public enum eVencedorSessaoJogoVelha
    {
        JogoEmAndamento,
        Empate,
        X,
        O
    }
}
