import type { ColJogoDaVelha } from "~/components/jogo-vela/types";
import { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

const scores = {
  X: 10,
  O: -10,
  empate: 0,
};

export const useBestMove = (
  board: Array<ColJogoDaVelha[]>,
  ai: ValorColSelecionado,
  human: ValorColSelecionado
) => {

  scores[ai] = 10;
  scores[human] = -10;
  
  let bestScore = -Infinity;
  let move = { i: -1, j: -1 };
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (!board[i][j].selecionado) {
        board[i][j].selecionado = ai;
        let score = minimax(board, 0, false, ai, human);
        board[i][j].selecionado = undefined;
        if (score > bestScore) {
          bestScore = score;
          move = { i, j };
        }
      }
    }
  }
  return move;
};

const minimax = (
  board: Array<ColJogoDaVelha[]>,
  depth: number,
  isMaximizing: boolean,
  ai: ValorColSelecionado,
  human: ValorColSelecionado
) => {
  let result = useGetWin(board);
  if (result === "empate") {
    return scores["empate"];
  }
  if (result?.selecionado) {
    return scores[result.selecionado];
  }
  if (isMaximizing) {
    let bestScore = -Infinity;
    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        if (!board[i][j].selecionado) {
          board[i][j].selecionado = ai;
          bestScore = Math.max(
            bestScore,
            minimax(board, depth + 1, false, ai, human)
          );
          board[i][j].selecionado = undefined;
        }
      }
    }
    return bestScore;
  }

  let bestScore = Infinity;
  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      if (!board[i][j].selecionado) {
        board[i][j].selecionado = human;
        bestScore = Math.min(
          bestScore,
          minimax(board, depth + 1, true, ai, human)
        );
        board[i][j].selecionado = undefined;
      }
    }
  }
  return bestScore;
};
