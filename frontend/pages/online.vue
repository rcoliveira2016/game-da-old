<template>
  <section>
    <p><input type="checkbox" v-model="jogarOnlineStore.host" /> host</p>
    <p v-if="jogarOnlineStore.host">
      <BtnComponent @click="jogarOnlineStore.iniciarPartida">Gerar novo Sessao</BtnComponent>
      <InputText :model-value="jogarOnlineStore.identicicador" />
    </p>
    <p v-else>
      <InputText v-model="jogarOnlineStore.identicicador" />
      <BtnComponent @click="jogarOnlineStore.conectarPartida">concetar partida</BtnComponent>
    </p>
  </section>
  <section v-if="jogarOnlineStore.conectado">
    <JogoVelaJogoDaVelhaComponent
      :ganhador="jogarOnlineStore.ganhador"
      :jogadorAtual="jogarOnlineStore.jogadorAtual"
      :board="jogarOnlineStore.board"
      @selecionou-celula="jogarOnlineStore.setarJogada"
    />
  </section>
</template>
<script setup lang="ts">
import { useJogaOnlineStore } from '~/stores/jogar-online/store';

const jogarOnlineStore = useJogaOnlineStore();
</script>
