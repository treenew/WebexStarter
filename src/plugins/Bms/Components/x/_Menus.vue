<template title="菜单管理">
    <template v-if="loaded">
        <el-menu :default-active="defaultActive"
                 :collapse="collapse"
                 background-color="#262E41"
                 text-color="#fff"
                 active-text-color="#fff"
                 ref="menus"
                 :collapse-transition="false"
                 :router="true">
            <x-menu-item v-for="(item,key) in menus" :key="key" :item="item" :index="'/' + key"></x-menu-item>
        </el-menu>
    </template>
</template>
<script>
    import { menu } from '/bms/libs/apis';

    function findActive(index, items = []) {
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.items) {
                if (findActive(index, item.items)) return true;
            }
            else if (item.path.toLowerCase() == index) {
                return true;
            }
        }
        return false;
    }

    export default {
        data() {
            return {
                menus: [],
                loaded: false,
                defaultActive: ""
            };
        },
        props: {
            collapse: {
                type: Boolean,
                default: false
            }
        },
        init() {
            const vm = this;

            menu.getMenus().then(r => {
                if (r.status) {
                    msg.error(r.message)
                }
                else {
                    vm.menus = r.value;
                    vm.fixIndex();
                }
            });

            vm.loaded = true;
        },
        methods: {
            fixIndex() {
                const vm = this;
                var index = location.pathname.toLowerCase();
                if (!findActive(index, vm.menus)) {
                    index = index.substr(0, index.lastIndexOf('/')) + '/index.html';
                    if (!findActive(index, vm.menus)) index = "";
                }

                if (vm.defaultActive == index) return;
                vm.defaultActive = index;
            }
        }
    }
</script>