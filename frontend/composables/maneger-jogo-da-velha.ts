import { ColJogoDaVelha } from "~/components/canva-jogo-vela/types";

const getWin = (tabuleiro: Array<ColJogoDaVelha[]>) => {
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
    console.log(tabuleiro);
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
  return undefined;
};

export const useManegerJogoDaVelha = () => {
  const matrixDefault = () => [
    [{}, {}, {}],
    [{}, {}, {}],
    [{}, {}, {}],
  ];
  let jogadas = 0;
  const ganhador = ref<ColJogoDaVelha|undefined>();
  const jogadorAtual = ref<"X" | "O">("X");
  const matrix = ref<Array<ColJogoDaVelha[]>>(matrixDefault());
  const setarJogada = (col: ColJogoDaVelha) => {
    if (col.selecionado) return;
    if (++jogadas == 9) return;
    if (ganhador.value) return;
    
    col.selecionado = jogadorAtual.value;
    jogadorAtual.value = jogadorAtual.value === "X" ? "O" : "X";


    if (jogadas >= 5) {
      ganhador.value = getWin(matrix.value);     
    }
  };

  const resetar = () => {
    matrix.value = matrixDefault();
    jogadorAtual.value = "X";
    jogadas = 0;
    ganhador.value = undefined;
  };

  return {
    matrix,
    jogadorAtual,
    ganhador,
    setarJogada,
    resetar,
  };
};
