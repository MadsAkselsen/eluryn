import { fileURLToPath, URL } from 'node:url'
import fs from "node:fs";

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

import tailwindcss from '@tailwindcss/vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  server: {
    https: {
      key: fs.readFileSync("../certs/local-key.pem"),
      cert: fs.readFileSync("../certs/local-cert.pem"),
    },
    port: 5173,
  },

  // This was for running inside a container with Traefik
  // server: {
  //   host: true, // listen on 0.0.0.0 in container
  //   port: 5173,
  //   strictPort: true,

  //   // 👇 this is the important part for Traefik
  //   hmr: {
  //     host: "app.localhost",
  //     protocol: "ws",
  //     clientPort: 80,
  //   },
  // },
})
