<template title="首页">
    <h1>欢迎您 {{identity.nickname}}</h1>
</template>
<script>
    export default {
        props: {
            identity: Object
        }
    }
</script>