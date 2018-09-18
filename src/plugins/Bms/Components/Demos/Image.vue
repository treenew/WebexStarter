<template title="图片上传演示">
    <div>
        <x-image v-model="value"></x-image>
    </div>
    <div>
        <h2>value:</h2>
        <pre>{{value}}</pre>
    </div>
</template>

<script>

    export default {
        data() {
            return {
                value: ""
            };
        },
        init() {

        },
        deinit() {

        }
    }
</script>