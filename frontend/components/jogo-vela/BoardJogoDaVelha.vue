<script setup lang="ts">
import { PropType } from "vue";
import { ColJogoDaVelha, ColSelecionadoEvent } from "./types";
const emit = defineEmits<{
  (e: "selecionou-celula", event: ColSelecionadoEvent): void;
}>();
const prop = defineProps({
  board: {
    type: Array as PropType<Array<ColJogoDaVelha[]>>,
    required: true,
  },
});
</script>
<template>
  <div class="container">
    <div class="grid">
      <div class="row" v-for="(row, indexRow) in prop.board" :key="indexRow">
        <div
          v-for="(col, indexCol) in row"
          :class="{
            col: true,
            'col--selecionado-x': col.selecionado === 'X',
            'col--selecionado-o': col.selecionado === 'O',
          }"
          :key="indexCol"
          @click="emit('selecionou-celula', {col: col, indexRow: indexRow, indexCol: indexCol})"
        >
          <div>{{ col.selecionado }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.container{
  width: 18em;
}
.grid {
  display: flex;
  flex-direction: column;
}
.row {
  width: 100%;
  display: flex;
  flex-direction: row;
  flex: 1;
}
.col {
  flex: 1;
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;

  height: 5em;
  max-width: 5em;
  min-width: 5em;

  margin: 0.5em;

  border: 1px solid #000;
  border-radius: 0.5em;

}
.col--selecionado-x {
  background: rgb(170, 107, 107);
}
.col--selecionado-o {
  background: rgb(90, 81, 126);
}
</style>
