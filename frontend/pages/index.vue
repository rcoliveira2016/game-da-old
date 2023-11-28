<template>
  <div>
    <JogoDaVelhaComponent :ganhador="ganhador" :jogadorAtual="jogadorAtual" :board="matrix" @selecionou-celula="setarJogada" @resetar="resetar" />
  </div>
</template>
<script setup lang="ts">
import JogoDaVelhaComponent from '@/components/jogo-vela/JogoDaVelhaComponent.vue';
const { matrix, jogadorAtual, ganhador, setarJogada, resetar, iniciar } = useManegerJogoDaVelha();
onMounted(() => {
  iniciar();
})
</script>