<header title="{0} - 超级管理平台">
    <!--全局样式-->
    <link rel="stylesheet" href="/globals/node_modules/element-ui/lib/theme-chalk/index.css">
    <link href="/globals/assets/libs/iconfont/iconfont.css" rel="stylesheet" />

    <!--插件样式-->
    <link href="/bms/assets/css/site.css" rel="stylesheet" />
</header>
<footer>

    <!--全局脚本-->
    <script src="/globals/node_modules/fastclick/lib/fastclick.js"></script>
    <script src="/globals/node_modules/vue/dist/vue.min.js"></script>
    <script src="/globals/node_modules/element-ui/lib/index.js"></script>
    <script src="/globals/node_modules/moment/min/moment.min.js"></script>
    <script src="/globals/node_modules/moment/locale/zh-cn.js"></script>
    <script src="/globals/assets/js/vue.ext.js"></script>
    <script src="/globals/assets/libs/history.js/native.history.js"></script>
    <script src="/globals/assets/libs/iconfont/iconfont.js"></script>

    <!--第三方 JS 插件脚本-->
    <script src="/bms/assets/libs/ueditor/ueditor.config.js"></script>
    <script src="/bms/assets/libs/ueditor/ueditor.all.js"></script>

    <!--插件脚本-->
    <script src="/bms/assets/js/bms.core.js"></script>
    <script>document.addEventListener('DOMContentLoaded', function() { FastClick.attach(document.body); });</script>
</footer>

<template inline="true">
    <el-container v-if="loaded">
        <el-header class="el-topbar" height="auto">
            <el-row type="flex">
                <el-col :xs="12" :sm="12" :md="12" :lg="12">
                    <a class="topbar-home" target="_blank" href="javascript:void(0);" @click="menuShow=!menuShow" title="切换菜单显示"></a>
                    <a href="javascript:void(0);" target="_self" class="topbar-home-link">
                        <span>管理控制台</span>
                    </a>
                </el-col>
                <el-col :xs="12" :sm="12" :md="12" :lg="12">
                    <el-dropdown trigger="click" class="topbar-info" @command="dropdownCommand">
                        <a href="javascript:void(0)" class="user-name">
                            <span class="el-dropdown-link">
                                <img src="/bms/assets/images/user.jpg" />{{identity.nickname}}<i class="el-icon-caret-bottom el-icon--right"></i>
                            </span>
                        </a>
                        <el-dropdown-menu slot="dropdown">
                            <el-dropdown-item command="/bms/user/modifypassword.html">修改密码</el-dropdown-item>
                            <el-dropdown-item command="logout">退出</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>
                </el-col>
            </el-row>
        </el-header>
        <el-container>
            <el-aside width="auto" v-show="menuShow">
                <div class="version">VER 1.0.0.0</div>
                <x-menus ref="menus"></x-menus>
            </el-aside>
            <el-main>
                <div class="el-main-body" v-if="currentName">
                    <keep-alive :include="includes">
                        <component ref="page" :is="currentName" :identity="identity"></component>
                    </keep-alive>
                </div>
            </el-main>
        </el-container>
    </el-container>
</template>

<script>
    //Vue.Router.enabled = false; //- 设为 false 表示禁用 SPA 模式

    import { user } from '/bms/libs/apis';
    import "/bms/x/all.vue";

    export default {
        data() {
            var identity = user.getIdentity().sync().value;
            if (!identity) window.goLoginPage();
            return {
                loaded: identity != null,
                identity,
                keepAlive: false,
                menuShow: true,
                includes: [],
                currentName: "",
                homePath: "/bms/index.html"
            }
        },
        mounted: function() {
            const vm = this;
            const scrollTops = [];

            vueBus.$on('init', function(child) {
                if (child.$options.isPage) {
                    vm.keepAlive = child.$options.keepAlive || false;

                    var title = child.$options.pageTitle;
                    if (child.$options.layoutTitle) title = child.$options.layoutTitle.replace(/\{0\}/g, title);
                    document.title = title;

                    vm.keepAlive && vm.includes.indexOf(child.$options.pageId) == -1 && vm.includes.push(child.$options.pageId);

                    if (vm.keepAlive && scrollTops.length) {
                        vm.setScrollTop(scrollTops.pop());
                    }
                }
            });

            function statechange() {
                var state = History.getState();
                var hashes = state.hash.split("?");
                var path = hashes[0] + "";
                var name = parseComponentName(!path || path == '/' ? vm.homePath : path);

                window.$initQuery();

                Vue.addComponent(name, path.replace(/\.html$/gi, ".vue"));

                if (vm.keepAlive) {
                    scrollTops.push(vm.getScrollTop());
                }

                vm.currentName = name;
                vm.$refs.menus.fixIndex();
            };

            History.Adapter.bind(window, 'statechange', statechange);

            Vue.Router.push((location.pathname == '/' ? vm.homePath : location.pathname) + location.search);
            statechange();
        },
        methods: {
            getScrollTop() {
                var view = document.querySelector('.el-main');
                return view ? view.scrollTop : 0;
            },
            setScrollTop(value) {
                var view = document.querySelector('.el-main');
                if (view) view.scrollTop = value;
            },
            dropdownCommand(command) {
                if (command == 'logout') {
                    user.logout().sync();
                    window.goLoginPage();
                }
                else {
                    Vue.Router.push(command);
                }
            }
        }
    }
</script>