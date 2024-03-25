<script setup lang="ts">
import type { ColJogoDaVelha, ColSelecionadoEvent } from "~~/components/jogo-vela/types";
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
  <div class="container" tabindex="0" atl="tabuleiro do jogo da velha">
    <header class="cabecalho">
      <div class="status-partida" v-if="ganhador">
        <div>Ganhador:
          <JogoVelaIdentificadorJogador :jogador="ganhador.selecionado" />
        </div>
      </div>
      <div class="status-partida" v-else>Jogador Atual:
        <JogoVelaIdentificadorJogador :jogador="jogadorAtual" />
      </div>
      <BtnComponent @click="emit('resetar')">Resetar</BtnComponent>
    </header>
    <div class="container-board">
      <BoardJogoDaVelha :board="prop.board" @selecionou-celula="emit('selecionou-celula', $event)" />
    </div>
  </div>
</template>
<style scoped>
.container {
  font-size: 1rem;
  padding: .6rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 18rem;
  border: 1px solid #000;
  border-radius: 0.4rem;
  position: relative;
}

.cabecalho {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
