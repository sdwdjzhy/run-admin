import { createApp } from "vue";
import App from "./App.vue";

import ElementPlus from "element-plus";
import * as ElementIcon from "@element-plus/icons";
import "virtual:windi.css";
import "./styles/index.scss";
import "./styles/main.scss";
import router from "./router";
import { errorHandler } from "./error";
import store from "./store";
import _ from "lodash";

const app = createApp(App);
app.use(ElementPlus, { size: "small" });
app.use(router);
app.use(store);
errorHandler(app);
for (const key in ElementIcon) {
    if (key == "Menu") {
        app.component("IconMenu", ElementIcon[key]);
    } else {
        app.component(key, _.get(ElementIcon, key));
    }
}

app.mount("#app");
