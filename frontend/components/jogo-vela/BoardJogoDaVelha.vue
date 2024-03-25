<script setup lang="ts">
import type { ColJogoDaVelha, ColSelecionadoEvent } from "~~/components/jogo-vela/types";
const emit = defineEmits<{
  (e: "selecionou-celula", event: ColSelecionadoEvent): void;
}>();
const prop = defineProps({
  board: {
    type: Array as PropType<Array<ColJogoDaVelha[]>>,
    required: true,
  },
});

const classCol = (col: ColJogoDaVelha) => {
  return {
    col: true,
    'col--selecionado-x': col.selecionado === 'X',
    'col--selecionado-o': col.selecionado === 'O',
  };
}
const textoAcessibilidadeCol = (indexRow: number, indexCol: number) => {
  return `linha ${indexRow + 1}, coluna ${indexCol + 1}`
}
onMounted(() => {
  document.addEventListener('keydown', (event) => {
    if (event.key === 'Enter') {
      if (!document?.activeElement) return;
      if ('click' in document?.activeElement)
        (document.activeElement as HTMLDivElement).click()
    }
  })
})
</script>
<template>
  <div class="bord-container">
    <div class="grid">
      <div class="row" v-for="(row, indexRow) in prop.board" :key="indexRow">
        <div
          v-for="(col, indexCol) in row"
          tabindex="0"
          :class="classCol(col)"
          :key="indexCol"
          :alt="textoAcessibilidadeCol(indexRow, indexCol)"
          @click="emit('selecionou-celula', {col: col, indexRow: indexRow, indexCol: indexCol})"
        >
          <div>{{ col.selecionado }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.bord-container{
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
  background: var(--cor-jogador-x);
}
.col--selecionado-o {
  background: var(--cor-jogador-o);
}
</style>
