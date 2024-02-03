<template>
  <section>
    <p><input type="checkbox" v-model="jogarOnlineStore.host" /> host</p>
    <p v-if="jogarOnlineStore.host">
      <BtnComponent @click="iniciarPartida">Gerar novo Sessao</BtnComponent>
      <InputText :model-value="jogarOnlineStore.identicicador" />
    </p>
    <p v-else>
      <InputText v-model="jogarOnlineStore.identicicador" />
      <BtnComponent @click="conectarPartida">concetar partida</BtnComponent>
    </p>
  </section>
  <section v-if="jogarOnlineStore.conectado">
    <JogoVelaJogoDaVelhaComponent
      :ganhador="jogarOnlineStore.ganhador"
      :jogadorAtual="jogarOnlineStore.jogadorAtual"
      :board="jogarOnlineStore.board"
      @selecionou-celula="setarJogada"
      @resetar="jogarOnlineStore.resetar"
    />
  </section>
</template>
<script setup lang="ts">
import { HttpTransportType, HubConnectionBuilder } from "@microsoft/signalr";
import type { ColSelecionadoEvent } from "~/components/jogo-vela/types";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

interface JogoDaVelhaHubInputModel {
  Identificador: string;
  IndexLinha: number;
  IndexColuna: number;
}

const jogarOnlineStore = useJogaOnline();
const port = 5023;
const connection = new HubConnectionBuilder()
  .withUrl(`http://localhost:${port}/JogoDaVelhaHub`, {
    transport: HttpTransportType.WebSockets,
  })
  .build();

const iniciarPartida = async () => {
  await connection.start();
  await connection.invoke("IniciarNovoJogo");
};

const conectarPartida = async () => {
  await connection.start();
  await connection.invoke("ConectarPartida", jogarOnlineStore.identicicador);
};

const setarJogada = async (event: ColSelecionadoEvent) => {
  await connection.invoke("SetarJogada", {
    Identificador: jogarOnlineStore.identicicador,
    IndexColuna: event.indexCol,
    IndexLinha: event.indexRow,
  } satisfies JogoDaVelhaHubInputModel);
};

connection.on(
  "SetarJogada",
  (
    board: Array<ValorColSelecionado[]>,
    jogadorProximoJogador: ValorColSelecionado,
    ganhador: ValorColSelecionado
  ) => {
    jogarOnlineStore.jogadorAtual = jogadorProximoJogador;
    console.log(ganhador)
    if (ganhador == "X" || ganhador == "O")
      jogarOnlineStore.ganhador = { selecionado: ganhador };

    for (let index = 0; index < board.length; index++) {
      const element = board[index];
      for (let j = 0; j < element.length; j++) {
        const valor = element[j];
        jogarOnlineStore.board[index][j] = { selecionado: valor };
      }
    }
  }
);

connection.on("JogoAberto", (identificador: string) => {
  jogarOnlineStore.identicicador = identificador;
});

connection.on("JogoIniciado", (identicicador: string) => {
  jogarOnlineStore.identicicador = identicicador;
  jogarOnlineStore.conectado = true;
});

connection.onclose(async () => {
  await connection.start();
});
onMounted(() => {
  document.addEventListener("abort", (event) => {
    connection.stop();
  });
});
</script>
