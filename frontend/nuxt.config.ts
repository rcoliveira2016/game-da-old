// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  app: {
    head: {
      charset: "utf-8",
      viewport: "width=device-width, initial-scale=1",
      title: "Jogo da velha",
    },
  },
  devtools: { enabled: true },
  css: ["assets/style/app.css"],
  imports: {
    dirs: ["composables/**"],
  },
  modules: ["@pinia/nuxt"],
  runtimeConfig: {
    public: {
      APP_URL_SIGNALR: process.env.APP_URL_SIGNALR,
    },
  },
});
