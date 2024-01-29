import {
  ColJogoDaVelha,
  ColSelecionadoEvent,
} from "~/components/jogo-vela/types";
import { useGetWin } from "./use-get-win";
import { useBestMove } from "./use-maxmin-jogo-da-velha";
import { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

export const useManegerJogoDaVelha = () => {
  const matrixDefault = () => [
    [{}, {}, {}],
    [{}, {}, {}],
    [{}, {}, {}],
  ];
  let jogadas = 0;
  const board = ref<Array<ColJogoDaVelha[]>>(matrixDefault());
  const ganhador = ref<ColJogoDaVelha | undefined>();
  const jogadorAtual = ref<ValorColSelecionado>("X");
  const iaPlayer = ref<ValorColSelecionado | "">("");

  const setarJogada = (event: ColSelecionadoEvent) => {
    setarJogadaBoard(event.indexRow, event.indexCol);
    if (possuiJogadorIA()) setarJogadaIA();
  };

  const setarJogadaIA = () => {
    if (!iaPlayer.value) return;
    const humano = jogadorHumano();
    const boardRaw = toRaw(board.value.map((x) => x.map((x) => ({ ...x }))));
    const move = useBestMove(boardRaw, iaPlayer.value, humano);
    setarJogadaBoard(move.i, move.j);
  };

  const setarJogadaBoard = (indexRow: number, indexCol: number) => {
    const col = board.value[indexRow][indexCol];
    if (col.selecionado) return;
    if (jogadas++ == 9) return;
    if (ganhador.value) return;

    board.value[indexRow][indexCol].selecionado = jogadorAtual.value;
    jogadorAtual.value = toogle(jogadorAtual.value);

    if (jogadas >= 5) {
      const win = useGetWin(board.value);
      if (!!win && win !== "empate") {
        ganhador.value = win;
        return;
      }
    }
  };

  const resetar = () => {
    board.value = matrixDefault();
    jogadorAtual.value = "X";
    jogadas = 0;
    ganhador.value = undefined;
    setarPlayerIA();
  };

  const possuiJogadorIA = () => {
    return !!iaPlayer.value;
  };

  const setarPlayerIA = () => {
    if (!possuiJogadorIA()) return;

    if (iaPlayer.value === "X") {
      setarJogadaIA();
    }
  };

  const jogadorHumano = () => {
    return iaPlayer.value === "" ? "X" : toogle(iaPlayer.value as ValorColSelecionado);
  };

  const iniciar = () => {
    setarPlayerIA();
    ouvirCamandosVoz();
  };

  watch(iaPlayer, () => {
    resetar();
  })

  const toogle = (value: Ref<ValorColSelecionado> | ValorColSelecionado) => {
    const realValue = typeof value === "string" ? value : value.value;

    return realValue === "X" ? "O" : "X";
  }

  const ouvirCamandosVoz = () => {
    const canalEventoVoz = useEventSpeechRecognition();
    canalEventoVoz.adicionarEventos((texto) => {
      console.log(texto);
      if (!validarComandoVoz(texto)) return;

      const objetoJogadaVoz = parserVozJogada(texto);
      setarJogada({
        col: board.value[objetoJogadaVoz.linha - 1][objetoJogadaVoz.coluna - 1],
        indexCol: objetoJogadaVoz.coluna - 1,
        indexRow: objetoJogadaVoz.linha - 1,
      })
    })
  }

  const parserVozJogada = (texto: string) => {
    const incioTextoComandoJogada = texto.substring(texto.indexOf("jogar")).toLowerCase();
    const textosSepadados = incioTextoComandoJogada.split(' ');
    const indexLinha = textosSepadados.indexOf("linha");
    const valorLinha = textosSepadados[indexLinha + 1];

    const indexColuna = textosSepadados.indexOf("coluna");
    const valorColuna = textosSepadados[indexColuna + 1];

    return {
      linha: parseInt(valorLinha),
      coluna: parseInt(valorColuna),
    }
  }

  const validarComandoVoz = (texto: string) => {
    return texto.includes('jogar') && texto.includes('linha') && texto.includes('coluna');
  }

  return {
    board,
    jogadorAtual,
    ganhador,
    iaPlayer,
    setarJogada,
    resetar,
    iniciar,
  };
};
