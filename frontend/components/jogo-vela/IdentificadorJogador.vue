<script setup lang="ts">
import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";
const prop = defineProps({
    jogador: {
        type: String as PropType<ValorColSelecionado>,
        required: false,
    },
});

const classTextoJogador = (valor: ValorColSelecionado | undefined) => valor ? [`bg-jogador-${valor.toLowerCase()}`] : []

</script>
<template>
    <strong :class="classTextoJogador(jogador)">{{ jogador}}</strong>
</template>
<style>
strong {
    padding: .3rem .5rem;
    border-radius: .2rem;
}
</style>