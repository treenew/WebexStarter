<template title="首页">
    <h1>欢迎您 {{identity.nickname}}</h1>
    <h2>创建于：{{created|date}}</h2>
    <h2>现在是：{{now|date}}</h2>
</template>
<script>
    var intervalFlag;
    export default {
        props: {
            identity: Object
        },
        data() {
            return {
                created: new Date(),
                now: new Date(),
            }
        },
        init() {
            const vm = this;
            intervalFlag = setInterval(() => {
                vm.now = new Date();
                console.log('run now interval...');
            }, 1000);
        },
        deinit() {
            if (intervalFlag) clearInterval(intervalFlag);
        }
    }
</script>