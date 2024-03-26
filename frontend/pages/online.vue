<template>
  <section>
    <div class="conctar-partida">
      <BtnComponent
        v-if="!jogarOnlineStore.host"
        :disabled="jogarOnlineStore.host === false"
        @click="jogarOnlineStore.conectarPartida"
        >concetar partida</BtnComponent
      >
      <BtnComponent
        v-if="jogarOnlineStore.host !== false"
        :disabled="!!jogarOnlineStore.identicicador"
        @click="jogarOnlineStore.iniciarPartida"
        >inciar partida</BtnComponent
      >
      <InputText
        v-if="!jogarOnlineStore.conectado"
        v-model="jogarOnlineStore.identicicador"
        :disabled="jogarOnlineStore.host"
      />
    </div>
  </section>
  <section class="canvas-jogo-velha" v-if="jogarOnlineStore.conectado">
    <JogoVelaJogoDaVelhaComponent
      :ganhador="jogarOnlineStore.ganhador"
      :jogadorAtual="jogarOnlineStore.jogadorAtual"
      :board="jogarOnlineStore.board"
      @selecionou-celula="jogarOnlineStore.setarJogada"
    />
  </section>
</template>
<script setup lang="ts">
import { useJogaOnlineStore } from "~/stores/jogar-online/store";

const jogarOnlineStore = useJogaOnlineStore();
jogarOnlineStore.resetarTela();
</script>
<style scoped>
.conctar-partida {
  display: flex;
  flex-direction: row;
  gap: 1rem;
}
.canvas-jogo-velha {
  margin-top: var(--spacing-xl);
}
</style>
