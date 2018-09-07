<template inline>
    <div ref="editor">
    </div>
</template>
<script>
    export default {
        props: {
            value: String,
            config: Object
        },
        data() {
            return {
                currentValue: this.value
            };
        },
        mounted() {
            const vm = this;
            vm.$nextTick(() => {
                setTimeout(() => {
                    var divEditor = vm.$refs.editor;
                    divEditor.innerHTML = '<div style="width:100%;height:400px;line-height:normal;"></div>';
                    divEditor = divEditor.firstElementChild;
                    var editorId = divEditor.id;
                    if (!editorId) {
                        editorId = 'editor' + Math.random().toString().split('.')[1];
                        divEditor.id = editorId;
                    }
                    var editor = UE.getEditor(editorId, vm.config || {});
                    editor.ready(() => {
                        editor.addListener("contentChange", () => {
                            var content = editor.getContent();
                            vm.currentValue = content;
                            vm.$emit('input', content);
                        })
                        editor.setContent(vm.currentValue || "");
                    });
                    vm.$_editor = editor;
                }, 888);
            });
        },
        destroyed() {
            const vm = this;
            var divEditor = vm.$refs.editor;

            if (vm.$_editor) {
                UE.delEditor(divEditor.firstElementChild.id);
                vm.$_editor = null;
                divEditor.innerHTML = '';
            }
        },
        watch: {
            'value'(val, oldValue) {
                const vm = this;
                if (vm.currentValue == val) return;
                vm.currentValue = val;
                if (!vm.$_editor) return;
                vm.$_editor.ready(() => {
                    vm.$_editor.setContent(val);
                });

            }
        },
    }
</script>