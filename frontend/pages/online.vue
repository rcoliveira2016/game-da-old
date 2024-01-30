<template>
  <section>
    <p>recebidas: {{mensagem}}</p>
  </section>
  <section>
    <p>texto: <input type="text" v-model="tbMessage"><button @click="send">enviar</button> </p>
  </section>
</template>
<script setup lang="ts">
import * as signalR from "@microsoft/signalr";


interface JogoDaVelhaHubInputModel{
  indexLinha: number;
  indexColuna: number;
}

const mensagem = ref("");
const tbMessage = ref("");
const port = 5023;
const connection = new signalR.HubConnectionBuilder()
  .withUrl(`http://localhost:${port}/JogoDaVelhaHub`, {
    transport: signalR.HttpTransportType.WebSockets
  }
)
    .build();

connection.on("ReceiveMessage", (jogada: JogoDaVelhaHubInputModel) => {
  console.log(jogada);
  mensagem.value += JSON.stringify(jogada);
});

connection.start().catch((err) => console.log(err));


function send() {
  connection.invoke("SendMessage", { indexColuna:0, indexLinha: 1} satisfies JogoDaVelhaHubInputModel)
    .then(() => (tbMessage.value = ""));
}
</script>