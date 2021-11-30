<template>
    <layout>
        <router-view v-slot="{ Component }">
            <component :is="Component" />
        </router-view>
    </layout>
</template>

<script lang="ts" setup>
import layout from "./components/layout.vue";
import { onMounted } from "vue";
onMounted(() => {
    let n = document.getElementById("loading");
    if (n) document.body.removeChild(n);
    document.body.classList.remove("loading");
});
</script>
