<template title="角色管理">
    <el-card v-loading="loading">
        <div slot="header">
            <span>{{ state ? '修改角色' : '创建角色' }}</span>
        </div>
        <el-form ref="form" :model="item" :rules="rules" @submit.native.prevent :validate-on-rule-change="false">
            <el-form-item label="名称" prop="name">
                <el-input v-model="item.name" v-if="state" :disabled="true"></el-input>
                <el-input v-model="item.name" v-else></el-input>
            </el-form-item>
            <el-form-item label="状态" prop="status">
                <el-select v-model="item.status">
                    <el-option label="请选择" :value="undefined"></el-option>
                    <el-option label="删除" :value="-1"></el-option>
                    <el-option label="禁用" :value="0"></el-option>
                    <el-option label="启用" :value="1"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="扩展数据" prop="exdata">
                <el-input type="textarea"
                          :rows="2"
                          placeholder="请输入内容"
                          v-model="item.exdata">
                </el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submit">提交</el-button>
                <el-button v-router url="index.html">返回</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>

    import { role } from '../libs/apis';

    export default {
        data() {
            const vm = this;
            var state = vm.query.id ? 1 : 0;
            return {
                state: state,
                loading: state ? true : false,
                item: {},
                rules: {
                    name: [
                        { required: true, message: '请输入名称' },
                        { min: 1, max: 1000, message: '长度在 1 到 1000 个字符' }
                    ],
                    status: [
                        { required: true, message: '请选择状态' },
                    ]
                }
            }
        },
        init() {
            const vm = this;
            vm.state && role.find(vm.query.id).then(r => {
                vm.item = r.value;
                vm.loading = false;
            });
        },
        methods: {
            submit() {
                const vm = this;
                vm.$refs.form.validate(function(valid) {
                    if (!valid) return;
                    role.manage(vm.item).then(r => {
                        if (r.status) {
                            msg.error(r.message);
                        }
                        else {
                            msg.info('保存成功！');
                            vm.$routeBack();
                        }
                    });
                });
            }
        }
    }
</script>