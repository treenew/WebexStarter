<template title="用户管理">
    <el-card v-loading="loading">
        <div slot="header">
            <span>{{ state ? '修改用户' : '创建用户' }}</span>
        </div>
        <el-form ref="form" :model="item" :rules="rules" @submit.native.prevent :validate-on-rule-change="false">
            <el-form-item label="账号" prop="username">
                <el-input v-model="item.username" v-if="state" :disabled="true"></el-input>
                <el-input v-model="item.username" v-else></el-input>
            </el-form-item>
            <el-form-item label="名称" prop="nickname">
                <el-input v-model="item.nickname"></el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submit">提交</el-button>
                <el-button v-router url="index.html">返回</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script>

    import { user } from '../libs/apis';

    export default {
        data() {
            const vm = this;
            var state = vm.query.id ? 1 : 0;
            return {
                state: state,
                loading: state ? true : false,
                item: {},
                rules: {
                    nickname: [
                        { required: true, message: '请输入名称' },
                        { min: 1, max: 1000, message: '长度在 1 到 1000 个字符' }
                    ],
                    username: [
                        { required: true, message: '请输入账号' },
                        { min: 1, max: 1000, message: '长度在 1 到 1000 个字符' }
                    ]
                }
            }
        },
        init() {
            const vm = this;
            vm.state && user.find(vm.query.id).then(r => {
                vm.item = r.value;
                vm.loading = false;
            });
        },
        methods: {
            submit() {
                const vm = this;
                vm.$refs.form.validate(function(valid) {
                    if (!valid) return;
                    user.manage(vm.item).then(r => {
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