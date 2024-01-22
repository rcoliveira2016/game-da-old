<script setup lang="ts">
import { PropType } from "vue";
import { ColJogoDaVelha, ColSelecionadoEvent } from "./types";
import BoardJogoDaVelha from "./BoardJogoDaVelha.vue";
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";
const emit = defineEmits<{
  (e: "selecionou-celula", event: ColSelecionadoEvent): void;
  (e: "resetar"): void;
}>();
const prop = defineProps({
  board: {
    type: Array as PropType<Array<ColJogoDaVelha[]>>,
    required: true,
  },
  jogadorAtual: {
    type: String as PropType<ValorColSelecionado>,
    required: true,
  },
  ganhador: {
    type: Object as PropType<ColJogoDaVelha>,
  },
});
</script>
<template>
  <div class="container">
    <div>Jogador Atual: {{ jogadorAtual }}</div>
    <div v-if="ganhador">
      <div>Ganhador: {{ ganhador.selecionado }}</div>
    </div>
    <div><button @click="emit('resetar')">Resetar</button></div>
    <div class="container-board">
      <BoardJogoDaVelha :board="prop.board" @selecionou-celula="emit('selecionou-celula', $event)" />
    </div>  
  </div>
</template>
<style scoped>
.container {
  font-size: 16px;
  border: 1px solid black;
  padding: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 400px;
}
</style>
