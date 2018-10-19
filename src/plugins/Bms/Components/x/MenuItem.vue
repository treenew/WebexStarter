<template>
    <template v-if="item.path">
        <el-menu-item :index="item.path">
            <span v-html="item.title">
            </span>
        </el-menu-item>
    </template>
    <template v-else>
        <el-submenu :index="index">
            <template slot="title">
                <span v-html="item.title">
                </span>
            </template>
            <x-menu-item v-for="(sub,subKey) in item.items" :key="subKey" :item="sub" :index="index + '/' + subKey"></x-menu-item>
        </el-submenu>
    </template>
</template>
<script>
    export default {
        props: {
            item: {
                type: Object,
                required: true
            },
            index: {
                required: true
            }
        }
    }
</script>