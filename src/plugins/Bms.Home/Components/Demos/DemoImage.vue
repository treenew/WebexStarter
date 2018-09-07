<template title="图片上传演示">
    <h1>欢迎您 {{identity.nickname}} | [<bms-logout>立即退出</bms-logout>] | [<a href="../index.html">返回首页</a>]</h1>
    <hr />
    <div>
        <x-image v-model="value"></x-image>
    </div>
    <div>
        <h2>value:</h2>
        <pre>{{value}}</pre>
    </div>
</template>

<script>
    var identity = api('/bms-home/user/getIdentity').sync().value;

    import "/globals/x/all.vue";
    import logout from "/globals/bms/logout.vue";

    export default {
        components: {
            "bms-logout": logout
        },
        data() {
            return {
                identity: identity,
                value: ""
            };
        },
        mounted() {
        }
    }
</script>