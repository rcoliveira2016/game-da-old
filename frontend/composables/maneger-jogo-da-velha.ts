import { ColJogoDaVelha, ColSelecionadoEvent } from "~/components/jogo-vela/types";
import { useGetWin } from "./get-win";
import { useBestMove } from "./maxmin-jogo-da-velha";



export const useManegerJogoDaVelha = () => {
  const matrixDefault = () => [
    [{}, {}, {}],
    [{}, {}, {}],
    [{}, {}, {}],
  ];
  let jogadas = 0;
  const ganhador = ref<ColJogoDaVelha|undefined>();
  const jogadorAtual = ref<"X" | "O">("X");
  const board = ref<Array<ColJogoDaVelha[]>>(matrixDefault());

  const setarJogada = (event: ColSelecionadoEvent) => {
    if (event.col.selecionado) return;
    if (++jogadas == 9) return;
    if (ganhador.value) return;

    board.value[event.indexRow][event.indexCol].selecionado =
      jogadorAtual.value;
    jogadorAtual.value = jogadorAtual.value === "X" ? "O" : "X";
    
    if (jogadas >= 5) {
      const win = useGetWin(board.value);
      if(!!win && win !== "empate") {
        ganhador.value = win;
        return;
      }
    }
    setarJogadaIA();
  };

  
  const setarJogadaIA = () => {    
    const humano = jogadorAtual.value === "X" ? "O" : "X";
    const boardRaw = toRaw(board.value.map((x) => x.map((x) => ({ ...x }))));
    const move = useBestMove(boardRaw, jogadorAtual.value, humano);    
    board.value[move.i][move.j].selecionado = jogadorAtual.value;
    jogadorAtual.value = humano;

    if (++jogadas >= 5) {
      const win = useGetWin(board.value);
      if (!!win && win !== "empate") {
        ganhador.value = win;
        return;
      }
    }
  };

  const resetar = () => {
    board.value = matrixDefault();
    //Funcionando apenas para o teste
    //jogadorAtual.value = "O";
    jogadorAtual.value = "X";
    jogadas = 0;
    ganhador.value = undefined;
  };


  const iniciar = () => {
      setarJogadaIA();
  }

  return {
    matrix: board,
    jogadorAtual,
    ganhador,
    setarJogada,
    resetar,
    iniciar,
  };
};
