import { ColJogoDaVelha } from "~/components/jogo-vela/types";

export const useGetWin = (
  tabuleiro: Array<ColJogoDaVelha[]>
): ColJogoDaVelha | "empate" | undefined => {
  for (let i = 0; i < 3; i++) {
    if (
      !!tabuleiro[i][0].selecionado &&
      tabuleiro[i][0].selecionado === tabuleiro[i][1].selecionado &&
      tabuleiro[i][1].selecionado === tabuleiro[i][2].selecionado
    ) {
      return tabuleiro[i][0]; // Row winner
    }

    if (
      !!tabuleiro[0][i].selecionado &&
      tabuleiro[0][i].selecionado === tabuleiro[1][i].selecionado &&
      tabuleiro[1][i].selecionado === tabuleiro[2][i].selecionado
    ) {
      return tabuleiro[0][i]; // Column winner
    }
  }

  if (
    !!tabuleiro[0][0].selecionado &&
    tabuleiro[0][0].selecionado === tabuleiro[1][1].selecionado &&
    tabuleiro[1][1].selecionado === tabuleiro[2][2].selecionado
  ) {
    return tabuleiro[0][0]; // Diagonal from top-left to bottom-right
  }

  if (
    !!tabuleiro[0][2].selecionado &&
    tabuleiro[0][2].selecionado === tabuleiro[1][1].selecionado &&
    tabuleiro[1][1].selecionado === tabuleiro[2][0].selecionado
  ) {
    return tabuleiro[0][2]; // Diagonal from top-right to bottom-left
  }

  let openSpots = 0;
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (!tabuleiro[i][j].selecionado) {
        return undefined;
      }
    }
  }

  return "empate";
};
