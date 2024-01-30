import type {
  ColJogoDaVelha,
  ColSelecionadoEvent,
} from "~~/components/jogo-vela/types";
import { useGetWin } from "./use-get-win";
import { useBestMove } from "./use-maxmin-jogo-da-velha";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

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
  const usarVoz = ref(false);

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
    return iaPlayer.value === ""
      ? "X"
      : toogle(iaPlayer.value as ValorColSelecionado);
  };

  const iniciar = () => {
    setarPlayerIA();
  };

  const toogle = (value: Ref<ValorColSelecionado> | ValorColSelecionado) => {
    const realValue = typeof value === "string" ? value : value.value;

    return realValue === "X" ? "O" : "X";
  };

  let canalEventoVoz: EventSpeechRecognition | undefined = undefined;
  const ouvirCamandosVoz = () => {
    canalEventoVoz = canalEventoVoz ?? useEventSpeechRecognition();
    if (!canalEventoVoz.possuiSuporte) {
      console.error("Nao possui suporte");
      return;
    }
    canalEventoVoz.adicionarEventos((texto) => {
      const parserJogadaVoz = useParserVozJogada(texto);
      if (!parserJogadaVoz.sucesso) {
        console.error(parserJogadaVoz.erro);
        return;
      }
      setarJogada({
        col: board.value[parserJogadaVoz.objeto.linha - 1][
          parserJogadaVoz.objeto.coluna - 1
        ],
        indexCol: parserJogadaVoz.objeto.coluna - 1,
        indexRow: parserJogadaVoz.objeto.linha - 1,
      });
    });
    canalEventoVoz.iniciar();
  };
  const destruitCamandosVoz = () => {
    canalEventoVoz?.distruir();
  };

  watch(iaPlayer, () => {
    resetar();
  });

  watch(usarVoz, () => {
    if (!usarVoz.value) {
      destruitCamandosVoz();
      return;
    }
    ouvirCamandosVoz();
  });

  return {
    board,
    jogadorAtual,
    ganhador,
    iaPlayer,
    usarVoz,
    setarJogada,
    resetar,
    iniciar,
  };
};
