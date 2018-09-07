<template inline="true">
    <a href="javascript:;" @click="logout">
        <slot>
            退出
        </slot>
    </a>
</template>
<script>
    export default {
        methods: {
            logout() {
                api('/bms-home/user/logout').sync();
                window.unauthorized();
            }
        }
    }
</script>