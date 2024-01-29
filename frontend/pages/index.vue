<template>
  <div>
    <label for="iaPlayer">Como Jogar</label>
    <select id="iaPlayer" v-model="iaPlayer">
      <option value="">PvP</option>
      <option value="O">X vs IA</option>
      <option value="X">O vs IA</option>
    </select>
  </div>
  <div>
    <input type="checkbox" v-model="usarVoz"> usar comando de voz
  </div>
  <div v-if="iaPlayer">
    AI: {{ iaPlayer }}
  </div>
  <div>
    <JogoDaVelhaComponent :ganhador="ganhador" :jogadorAtual="jogadorAtual" :board="board" @selecionou-celula="setarJogada" @resetar="resetar" />
  </div>
</template>
<script setup lang="ts">
import JogoDaVelhaComponent from '@/components/jogo-vela/JogoDaVelhaComponent.vue';
const { board, jogadorAtual, ganhador, iaPlayer, usarVoz, setarJogada, resetar, iniciar } = useManegerJogoDaVelha();
onMounted(() => {
  iniciar();
})
</script>