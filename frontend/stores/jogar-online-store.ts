import type { ColJogoDaVelha } from "~/components/jogo-vela/types";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

export const useJogaOnline = defineStore("useJogaOnline", {
  state: () => ({
    board: [
      [{}, {}, {}],
      [{}, {}, {}],
      [{}, {}, {}],
    ] as Array<ColJogoDaVelha[]>,
    ganhador: undefined as ColJogoDaVelha | undefined,
    jogadorAtual: "X" as ValorColSelecionado,
    identicicador: "",
    host: true,
    conectado: false,
  }),
  getters: {
    estaJogando(): boolean {
      return this.conectado;
    },
  },
  actions: {
    async iniciarPartida() {},
    setarJogada(event: ColJogoDaVelha) {},
    resetar() {},
  },
});
