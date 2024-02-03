import type { ColJogoDaVelha } from "~/components/jogo-vela/types";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

export const useJogaOnlineStore = defineStore("useJogaOnline", () => {
  return {
    board: ref<Array<ColJogoDaVelha[]>>([
      [{}, {}, {}],
      [{}, {}, {}],
      [{}, {}, {}],
    ]),
    ganhador: ref<ColJogoDaVelha | undefined>(),
    jogadorAtual: ref<ValorColSelecionado>("X"),
    identicicador: ref(""),
    host: ref(true),
    conectado: ref(false),
  };
});
