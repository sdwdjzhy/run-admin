import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import WindiCSS from "vite-plugin-windicss";
const { join } = require("path");
function resolve(dir) {
    return join(__dirname, dir);
}

const root = resolve("src");
// https://vitejs.dev/config/
export default defineConfig({
    plugins: [WindiCSS(), vue()],

    resolve: {
        alias: {
            "@": root
        }
    }

    // css: {
    //     /* CSS 预处理器 */
    //     preprocessorOptions: {
    //         scss: {
    //             additionalData: '@import "src/styles/variables.scss";@import "src/styles/mixin.scss";'
    //         }
    //     }
    // }
});
