<template inline>
    <span @click="defaultClick" class="upload-span">
        <slot>
            <el-button>点击上传</el-button>
        </slot>
    </span>
</template>
<script>

    function getEditor() {
        if (!window.UPDATE_EDITOR) {
            var node = UE.dom.domUtils.createElement(document, 'script', {
                type: 'text/plain'
            });
            document.body.appendChild(node);
            node.style.padding = 0;
            node.style.border = 0;
            node.style.display = 'none';
            window.UPDATE_EDITOR = UE.getEditor(node, {
                isShow: false,
                focus: false,
                enableAutoSave: false,
                autoSyncData: false,
                autoFloatEnabled: false,
                wordCount: false,
                sourceEditor: null,
                scaleEnabled: true,
                toolbars: [["insertimage", "attachment"]]
            });
            window.UPDATE_EDITOR.addListener('beforeInsertImage', function(r, list) {
                var files = [];
                for (var i in list) {
                    files.push(list[i].src);
                }
                if (this.$viewModel) this.$viewModel.selectFiles(files);
                return true;
            });
            window.UPDATE_EDITOR.addListener('beforeInsertFile', function(r, list) {
                var files = [];
                for (var i in list) {
                    files.push(list[i].url);
                }
                if (this.$viewModel) this.$viewModel.selectFiles(files);
                return true;
            });
        }
        return window.UPDATE_EDITOR;
    }


    export default {
        props: {
            value: {
                type: [String, Array]
            },
            custom: Boolean,
            limit: {
                type: Number,
                default: 1
            },
            type: {
                type: String,
                default: 'img'
            }
        },
        data() {
            return {
                currentValue: this.value
            };
        },
        mounted() {
            const vm = this;
            vm.$nextTick(function() {
                var editor = getEditor();
                if (editor.$viewModel == vm) editor.$viewModel = null;
            });
        },
        destroyed() {
            var editor = getEditor();
            editor.$viewModel = null;
        },
        watch: {
            'value'(val, oldValue) {
                const vm = this;
                if (vm.currentValue == val) return;
                vm.currentValue = val;
            }
        },
        methods: {
            selectFiles(files) {
                const vm = this;
                if (vm.limit == 1) vm.currentValue = files[0];
                else {
                    for (var i = 0; i < files.length; i++) {
                        vm.currentValue.push(files[i]);
                    }
                }
                vm.$emit('input', vm.currentValue);
            },
            defaultClick() {
                const vm = this;
                if (vm.custom !== true) vm.open();
            },
            open() {
                const vm = this;
                var length = 0;
                if (vm.limit > 1) {
                    length = vm.value.length;
                    if (length == vm.limit) {
                        this.$message('对不起，最多只能选择 ' + vm.limit + ' 个内容！');
                        return;
                    }
                }
                var editor = getEditor();
                editor.$viewModel = vm;
                editor.options.fileNumLimit = vm.limit - length;

                var dialog;
                if (vm.type !== 'img' && vm.type !== 'image') {
                    dialog = editor.getDialog("attachment");
                    dialog.title = '附件上传';
                }
                else {
                    dialog = editor.getDialog("insertimage");
                    dialog.title = '图片上传';
                }
                dialog.title += '（您还可以上传 ' + editor.options.fileNumLimit + ' 个内容）';
                if (editor.dialogIsRender !== true) {
                    dialog.render();
                    editor.dialogIsRender = true;
                }

                dialog.open();
            }
        }
    }
</script>