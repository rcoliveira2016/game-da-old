<template>
  <main class="container">
    <header class="header-toolbar">
      <div class="justify-space-between">
        <div>
          <label for="iaPlayer" class="text-2">Como Jogar: </label>
          <SelectComponent v-model="iaPlayer" :options="options" />
        </div>
        <div v-if="iaPlayer">
          AI:
          <JogoVelaIdentificadorJogador :jogador="iaPlayer" />
        </div>
      </div>
      <div class="pym">
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
const options = [{ value:"", label:"PvP" },{value:"O",label:"X vs IA"},{value:"X",label:"O vs IA"}]
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