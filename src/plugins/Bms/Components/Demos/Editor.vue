<template title="富文本框编辑器演示">
    <div>
        <x-editor v-model="value"></x-editor>
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
                value: "<b>HELLO WORLD</b>"
            };
        },
        init() {

        },
        deinit() {

        }
    }
</script>