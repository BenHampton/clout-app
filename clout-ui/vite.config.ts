// import { reactRouter } from "@react-router/dev/vite";
// import tailwindcss from "@tailwindcss/vite";
// import { defineConfig } from "vite";
// import tsconfigPaths from "vite-tsconfig-paths";
//
// export default defineConfig({
//   plugins: [tailwindcss(), reactRouter(), tsconfigPaths()],
// });

import path from 'path'
import { reactRouter } from '@react-router/dev/vite'
import { defineConfig } from 'vite'
import { fileURLToPath } from 'node:url'
import tsconfigPaths from 'vite-tsconfig-paths'

export default defineConfig(()=> {
  return {
    test: {
      projects: [
        {
          test: {
            name: 'default',
            environment: 'jsdom',
            globals: true,
          },
          plugins: [tsconfigPaths()],
        },
      ],
    },
    plugins: [
      [reactRouter()],
      tsconfigPaths(),
    ],
  }
})