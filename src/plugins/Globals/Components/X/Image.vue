<template inline>
    <x-upload v-model="currentValue" :limit="limit" custom ref="image">
        <ul class="el-upload-list el-upload-list--picture-card">
            <template v-if="limit==1">
                <li tabindex="0" class="el-upload-list__item is-success" :src="currentValue" v-show="currentValue" :style="{width:width,height:height}">
                    <img :src="currentValue" class="el-upload-list__item-thumbnail" />
                    <label class="el-upload-list__item-status-label">
                        <i class="el-icon-upload-success el-icon-check"></i>
                    </label>
                    <span class="el-upload-list__item-actions">
                        <span class="el-upload-list__item-preview" @click="preview(currentValue)"><i class="el-icon-zoom-in"></i></span>
                        <span class="el-upload-list__item-delete" @click="currentValue=''"><i class="el-icon-delete"></i></span>
                    </span>
                </li>
            </template>
            <template v-else>
                <li tabindex="$index" class="el-upload-list__item is-success" :src="currentValue" v-for="(file,$index) in currentValue">
                    <img :src="file" class="el-upload-list__item-thumbnail" />
                    <label class="el-upload-list__item-status-label">
                        <i class="el-icon-upload-success el-icon-check"></i>
                    </label>
                    <span class="el-upload-list__item-actions">
                        <span class="el-upload-list__item-preview" @click="preview(file)"><i class="el-icon-zoom-in"></i></span>
                        <span class="el-upload-list__item-delete" @click="$delete(currentValue,$index)"><i class="el-icon-delete"></i></span>
                    </span>
                </li>
            </template>
        </ul>
        <div class="el-upload el-upload--picture-card" style="margin: 0 8px 8px 0;padding-top:2px;" v-bind:style="{width:width,height:height,lineHeight:height}" @click="$refs.image.open()" v-show="(limit==1&&!currentValue) || (limit>1&&currentValue.length < limit)">
            <i class="el-icon-plus"></i>
        </div>
        <el-dialog title="预览" :visible.sync="dialogVisible">
            <img style="width:100%" :src="active" />
        </el-dialog>
    </x-upload>
</template>
<script>
    export default {
        props: {
            value: {
                type: [String, Array]
            },
            limit: {
                type: Number,
                default: 1
            },
            width: {
                type: String,
                default: '148px'
            },
            height: {
                type: String,
                default: '148px'
            }
        },
        data() {
            return {
                currentValue: this.value,
                active: '',
                dialogVisible: false
            };
        },
        methods: {
            preview(file) {
                const vm = this;
                vm.active = file;
                vm.dialogVisible = true;
            }
        },
        watch: {
            'value'(val, oldValue) {
                const vm = this;
                if (vm.currentValue == val) return;
                vm.currentValue = val;
            },
            'currentValue'(val, oldValue) {
                const vm = this;
                if (vm.value == val) return;
                vm.$emit('input', val);
            }
        },
    }
</script>