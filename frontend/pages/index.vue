<template>
  <div>
    <CanvasJogoDaVelha :ganhador="ganhador" :jogadorAtual="jogadorAtual" :board="matrix" @selecionou-celula="setarJogada" @resetar="resetar" />
  </div>
</template>
<script setup lang="ts">
import CanvasJogoDaVelha from '@/components/canva-jogo-vela/CanvasJogoDaVelha.vue';
const { matrix, jogadorAtual, ganhador, setarJogada, resetar } = useManegerJogoDaVelha();

</script>