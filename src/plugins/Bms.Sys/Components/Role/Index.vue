
<template title="角色列表">
    <el-card>
        <div slot="header">
            <span>{{$options.pageTitle}}</span>
        </div>
        <el-form ref="filter" :inline="true">
            <el-form-item label="角色名称">
                <el-input placeholder="模糊搜索" v-model="params.nameLike"></el-input>
            </el-form-item>
            <el-row>
                <el-col :span="18">
                    <el-button type="success" round icon="el-icon-plus" v-router url="manage.html">添加角色</el-button>
                </el-col>
                <el-col :span="6" align="right">
                    <el-button type="primary" icon="el-icon-search" native-type="submit">搜索</el-button>
                </el-col>
            </el-row>
        </el-form>
    </el-card>

    <el-card>
        <x-table :params="params" ref="table" filter-ref="filter" :url="findAll">
            <el-table-column prop="id" label="编号"></el-table-column>
            <el-table-column prop="name" label="名称"></el-table-column>
            <el-table-column label="状态">
                <template slot-scope="scope">
                    <span v-if="scope.row.status==1">启用</span>
                    <span v-else-if="scope.row.status==-1">删除</span>
                    <span v-else>禁用</span>
                </template>
            </el-table-column>
            <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button v-router v-query:id="scope.row.id" url="manage.html" type="text" size="small">编辑</el-button>
                    <el-button @click="modifyStatus(scope.row)" type="text" size="small">{{scope.row.status==1?'禁用':'启用'}}</el-button>
                    <el-button @click="remove(scope.row)" type="text" size="small">删除</el-button>
                </template>
            </el-table-column>
        </x-table>
    </el-card>
</template>
<script>
    import { role } from '../libs/apis';

    export default {
        keepAlive: true,
        data() {
            return {
                params: {},
                findAll: role.findAll
            }
        },
        methods: {
            remove(row) {
                const vm = this;
                msg.confirm(`是否删除角色“${row.name}”？`, () => {
                    role.modifyStatus(row.id, -1).then(r => {
                        if (r.status) {
                            msg.error(r.message);
                        }
                        else {
                            msg.info('删除成功！');
                            vm.$refs.table.refresh();
                        }
                    });
                });
            },
            modifyStatus(row) {
                const vm = this;
                var text = row.status == 1 ? '禁用' : '启用';
                msg.confirm(`是否${text}角色“${row.name}”？`, () => {
                    role.modifyStatus(row.id, row.status == 1 ? 0 : 1).then(r => {
                        if (r.status) {
                            msg.error(r.message);
                        }
                        else {
                            msg.info('删除成功！');
                            vm.$refs.table.refresh();
                        }
                    });
                });
            }
        }
    }
</script>

