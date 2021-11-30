import { RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
    { path: "/:pathMatch(.*)*", component: () => import("@/views/404.vue") },
    { path: "/", name: "reach首页", component: () => import("@/views/Home.vue") }
];

export default routes;
