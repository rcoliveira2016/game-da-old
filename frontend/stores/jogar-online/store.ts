import { HttpTransportType, HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import type { ColJogoDaVelha, ColSelecionadoEvent } from "~/components/jogo-vela/types";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";
import type { JogoDaVelhaHubInputModel } from "./types";

export const useJogaOnlineStore = defineStore("useJogaOnline", () => {
  const board = ref<Array<ColJogoDaVelha[]>>([
    [{}, {}, {}],
    [{}, {}, {}],
    [{}, {}, {}],
  ]);

  const ganhador = ref<ColJogoDaVelha | undefined>();
  const jogadorAtual = ref<ValorColSelecionado>("X");
  const identicicador = ref("");
  const host = ref(true);
  const conectado = ref(false);

  const iniciarPartida = async () => {
    await signalREventsHub().start();
    await signalREventsHub().invoke("IniciarNovoJogo");
  };

  const conectarPartida = async () => {
    await signalREventsHub().start();
    await signalREventsHub().invoke("ConectarPartida", identicicador.value);
  };

  const setarJogada = async (event: ColSelecionadoEvent) => {
    await signalREventsHub().invoke("SetarJogada", {
      Identificador: identicicador.value,
      IndexColuna: event.indexCol,
      IndexLinha: event.indexRow,
    } satisfies JogoDaVelhaHubInputModel);
  };
  

  let connection: HubConnection | undefined = undefined;
  const signalREventsHub = () => {
    if (connection) return connection;

    const runtimeConfig = useRuntimeConfig();
    connection = new HubConnectionBuilder()
      .withUrl(runtimeConfig.public.urlSignalr, {
        transport: HttpTransportType.WebSockets,
      })
      .build();

    connection.on(
      "SetarJogada",
      (
        novoBoard: Array<ValorColSelecionado[]>,
        jogadorProximoJogador: ValorColSelecionado,
        novoGanhador: ValorColSelecionado
      ) => {
        jogadorAtual.value = jogadorProximoJogador;
        if (novoGanhador == "X" || novoGanhador == "O")
          ganhador.value = { selecionado: novoGanhador };

        for (let index = 0; index < novoBoard.length; index++) {
          const element = novoBoard[index];
          for (let j = 0; j < element.length; j++) {
            const valor = element[j];
            board.value[index][j] = { selecionado: valor };
          }
        }
      }
    );

    connection.on("JogoAberto", (identificador: string) => {
      identicicador.value = identificador;
    });

    connection.on("JogoIniciado", (identicicador: string) => {
      identicicador = identicicador;
      conectado.value = true;
    });

    connection.onclose(async () => {
      await connection?.start();
    });

    connection.on("ErroNotificacao", (msg: string[]) => {
      console.error(msg);
    });

    return connection;
  };


  
  onMounted(() => {
    document.addEventListener("abort", (event) => {
      signalREventsHub().stop();
    });
  });

  onBeforeMount(() => {
    signalREventsHub();
  })

  return {
    board,
    ganhador,
    jogadorAtual,
    identicicador,
    host,
    conectado,
    setarJogada,
    iniciarPartida,
    conectarPartida
  };
});
