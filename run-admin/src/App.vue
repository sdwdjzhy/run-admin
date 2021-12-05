<template>
    <component :is="component" />
</template>

<script lang="ts" setup>
import layout1 from "./components/layout.vue";
import { onMounted, ref } from "vue";
onMounted(() => {
    let n = document.getElementById("loading");
    if (n) document.body.removeChild(n);
    document.body.classList.remove("loading");
});
let component = ref(layout1);
</script>
