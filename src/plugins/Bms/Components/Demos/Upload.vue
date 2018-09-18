<template title="附件上传演示">
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
    export default {
        data() {
            return {
                value1: "",
                value2: ""
            };
        },
        init() {

        },
        deinit() {

        }
    }
</script>