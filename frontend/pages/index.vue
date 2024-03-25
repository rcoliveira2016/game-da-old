<template>
  <main class="container">
    <header class="header-toolbar">
      <div class="justify-space-between">
        <div>
          <label for="iaPlayer">Como Jogar: </label>
          <select id="iaPlayer" v-model="iaPlayer">
            <option value="">PvP</option>
            <option value="O">X vs IA</option>
            <option value="X">O vs IA</option>
          </select>
        </div>
        <div v-if="iaPlayer">
          AI:
          <JogoVelaIdentificadorJogador :jogador="iaPlayer" />
        </div>
      </div>
      <div>
        <input type="checkbox" v-model="usarVoz"> usar comando de voz
        <div v-if="usarVoz"> usar comando de voz: "Jogar linha [1-3] coluna [1-3]"</div>
      </div>
    </header>
    <div>
      <JogoDaVelhaComponent :ganhador="ganhador" :jogadorAtual="jogadorAtual" :board="board"
        @selecionou-celula="setarJogada" @resetar="resetar" />
    </div>
  </main>
</template>
<script setup lang="ts">
import JogoDaVelhaComponent from '@/components/jogo-vela/JogoDaVelhaComponent.vue';
const { board, jogadorAtual, ganhador, iaPlayer, usarVoz, setarJogada, resetar, iniciar } = useManegerJogoDaVelha();
onMounted(() => {
  iniciar();
})
</script>
<style scoped>
main.container {
  display: flex;
  flex-direction: column;
  align-content: center;
  flex-wrap: wrap;
}

.header-toolbar {
  display: flex;
  flex-direction: column;
}
</style>