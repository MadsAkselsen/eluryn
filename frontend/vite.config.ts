import { fileURLToPath, URL } from 'node:url'
import fs from 'node:fs'
import path from 'node:path'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig(({ command }) => {
  const isDevServer = command === 'serve'

  const keyPath = path.resolve(__dirname, '../certs/local-key.pem')
  const certPath = path.resolve(__dirname, '../certs/local-cert.pem')

  const hasLocalCerts =
    fs.existsSync(keyPath) && fs.existsSync(certPath)

  const server = !isDevServer
    ? undefined
    : hasLocalCerts
      ? {
          https: {
            key: fs.readFileSync(keyPath),
            cert: fs.readFileSync(certPath),
          },
          port: 5173,
        }
      : {
          port: 5173,
        }

  return {
    plugins: [
      vue(),
      vueDevTools(),
      tailwindcss(),
    ],
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url)),
      },
    },
    server,
  }
})