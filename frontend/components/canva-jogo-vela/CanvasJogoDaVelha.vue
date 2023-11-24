<script setup lang="ts">
import { PropType } from "vue";
import { ColJogoDaVelha } from "./types";
const emit = defineEmits<{
  (e: "selecionou-celula", cell: ColJogoDaVelha): void;
  (e: "resetar"): void;
}>();
const prop = defineProps({
  matrix: {
    type: Array as PropType<Array<ColJogoDaVelha[]>>,
    required: true,
  },
  jogadorAtual: {
    type: String as PropType<"X" | "O">,
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
    <div class="grid">
      <div class="row" v-for="(row, indexRow) in prop.matrix" :key="indexRow">
        <div
          v-for="(col, indexCol) in row"
          :class="{
            col: true,
            'col--selecionado-x': col.selecionado === 'X',
            'col--selecionado-o': col.selecionado === 'O',
          }"
          :key="indexCol"
          @click="emit('selecionou-celula', col)"
        >
          <div>{{ col.selecionado }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.container {  
  border: 1px solid black;
  padding: 10px;
  width: 410px;
}
.grid {
  display: flex;
  flex-direction: column;
  width: 400px;
  height: 400px;
}
.row {
  width: 100%;
  display: flex;
  flex-direction: row;
  flex: 1;
}
.col {
  flex: 1;
  border: 1px solid #000;
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;
}
.col--selecionado-x {
  background: #f00;
}
.col--selecionado-o {
  background: rgb(45, 19, 161);
}
</style>
