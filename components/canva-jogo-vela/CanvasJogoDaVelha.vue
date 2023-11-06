<script setup lang="ts">
import type { ColJogoDaVelha } from "@/components/canva-jogo-vela/types"

const matrix = ref<Array<ColJogoDaVelha[]>>([
    [{},{},{}],
    [{},{},{}],
    [{},{},{}]
]);
const selecionar = (col:ColJogoDaVelha) => {
    col.selecionado = "X"
}
</script>
<template>
    <div class="container">
        <div class="grid">
            <div class="row" v-for="(row, indexRow) in matrix" :key="indexRow">
                <div class="col" v-for="(col, indexCol) in row" :key="indexCol" @click="selecionar(col)">
                    <div v-if="col.selecionado==='X'">
                        X
                    </div>
                    <div v-if="col.selecionado==='O'">
                        O
                    </div>
                </div>
            </div>
        </div>        
    </div>
</template>
<style scoped>
.container{
    height: 400px;
    width: 570px;
    border: 1px solid black;
}
.grid{
    display: flex;
    flex-direction: column;
    height: 400px;
}
.row{
    width: 100%;
    display: flex;
    flex-direction: row;
    flex: 1;
}
.col{
    flex: 1;
    border: 1px solid #000;
    text-align: center;
    font-size: 5rem;
}
</style>