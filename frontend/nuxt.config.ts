// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  css: [
    'assets/style/app.css'
  ],
  imports: {
    dirs: [
      'composables/**'
    ]
  }
})
