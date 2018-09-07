<template title="附件上传演示">
    <h1>欢迎您 {{identity.nickname}} | [<bms-logout>立即退出</bms-logout>] | [<a href="../index.html">返回首页</a>]</h1>
    <hr />

    <div>
        <x-upload v-model="value1"></x-upload>
    </div>
    <div>
        <h2>value1:</h2>
        <pre>{{value1}}</pre>
    </div>
    <hr />
    <div>
        <x-upload v-model="value2" type="file"></x-upload>
    </div>
    <div>
        <h2>value2:</h2>
        <pre>{{value2}}</pre>
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
                value1: "",
                value2: ""
            };
        },
        mounted() {
        }
    }
</script>