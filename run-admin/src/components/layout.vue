<template>
    <el-container class="h-full">
        <el-aside width="200px">
            <div class="logo" :class="{ collapse: isCollapse }">
                <el-image :src="logo"></el-image>
            </div>
            <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">
                <el-sub-menu index="1">
                    <template #title>
                        <el-icon><location /></el-icon>
                        <span>Navigator One</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="1-1">item one</el-menu-item>
                        <el-menu-item index="1-2">item two</el-menu-item>
                    </el-menu-item-group>
                    <el-menu-item-group title="Group Two">
                        <el-menu-item index="1-3">item three</el-menu-item>
                    </el-menu-item-group>
                    <el-sub-menu index="1-4">
                        <template #title><span>item four</span></template>
                        <el-menu-item index="1-4-1">item one</el-menu-item>
                    </el-sub-menu>
                </el-sub-menu>
                <el-menu-item index="2">
                    <el-icon><icon-menu /></el-icon>
                    <template #title>Navigator Two</template>
                </el-menu-item>
                <el-menu-item index="3" disabled>
                    <el-icon><document /></el-icon>
                    <template #title>Navigator Three</template>
                </el-menu-item>
            </el-menu>
        </el-aside>
        <el-container>
            <el-header>
                <div class="flex-row flex-column-center">
                    <div class="pl-20px pr-20px border-bottom-1px flex-center flex flex-col cursor-pointer custom-bg h-60px" @click="isCollapse = !isCollapse">
                        <el-icon :size="20" color="#606266">
                            <component :is="isCollapse ? 'Expand' : 'Fold'"></component>
                        </el-icon>
                    </div>
                    <el-menu :default-active="activeIndex" class="el-menu-demo flex-grow1" mode="horizontal" @select="handleSelect">
                        <el-menu-item index="1">Processing Center</el-menu-item>
                        <el-sub-menu index="2">
                            <template #title>Workspace</template>
                            <el-menu-item index="2-1">item one</el-menu-item>
                            <el-menu-item index="2-2">item two</el-menu-item>
                            <el-menu-item index="2-3">item three</el-menu-item>
                            <el-sub-menu index="2-4">
                                <template #title>item four</template>
                                <el-menu-item index="2-4-1">item one</el-menu-item>
                                <el-menu-item index="2-4-2">item two</el-menu-item>
                                <el-menu-item index="2-4-3">item three</el-menu-item>
                            </el-sub-menu>
                        </el-sub-menu>
                        <el-menu-item index="3" disabled>Info</el-menu-item>
                        <el-menu-item index="4">Orders</el-menu-item>
                    </el-menu>
                    <div class="pr-20px border-bottom-1px flex-column flex-center" style="height: 60px">
                        <el-avatar></el-avatar>
                    </div>
                </div>
            </el-header>
            <el-main>
                <router-view v-slot="{ Component }">
                    <component :is="Component" />
                </router-view>
            </el-main>
        </el-container>
    </el-container>
</template>

<script setup lang="ts">
import { ref } from "vue";

// 菜单
function handleSelect() {
    console.log({ arguments });
}

const isCollapse = ref(false);
const handleOpen = (key: string, keyPath: string) => {
    console.log(key, keyPath);
};
const handleClose = (key: string, keyPath: string) => {
    console.log(key, keyPath);
};

const logo = ref("");
const sync = () => {};
</script>

<style>
.el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;
}
.el-menu-vertical-demo {
    min-height: 100%;
}
.line {
    border-bottom: 1px solid #e6e6e6;
    margin-top: -1px;
}
</style>
