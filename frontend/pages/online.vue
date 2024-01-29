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
const username = (new Date()).getTime().toString();
const mensagem = ref("");
const tbMessage = ref("");
const port = 5023;
const connection = new signalR.HubConnectionBuilder()
  .withUrl(`http://localhost:${port}/JogoDaVelhaHub`, {
    transport: signalR.HttpTransportType.WebSockets
  }
)
    .build();

connection.on("ReceiveMessage", (username: string, message: string) => {
  console.log(message);
  mensagem.value += message;
});

connection.on("ReceiveMessageTeste", (message: string) => {
  console.log(message);
});

connection.start().catch((err) => console.log(err));


function send() {
  connection.invoke("SendMessage", username, tbMessage.value)
    .then(() => (tbMessage.value = ""));
}
</script>