<template title="首页">
    <h1>欢迎您 {{identity.nickname}} | [<bms-logout>立即退出</bms-logout>]</h1>
    <hr />
    <h3><a href="demos/demoeditor.html">富文本</a></h3>
    <h3><a href="demos/demoimage.html">图片上传</a></h3>
    <h3><a href="demos/demoupload.html">附件上传</a></h3>
    <h3><a href="demos/demotable.html">分页表格</a></h3>
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
                identity: identity
            }
        }
    }
</script>