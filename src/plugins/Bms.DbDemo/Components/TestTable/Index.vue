
<template title="数据库测试">
    <el-card>
        <div slot="header">
            <span>{{$options.pageTitle}}</span>
        </div>
        <el-form ref="filter" :inline="true">
            <el-form-item label="名称">
                <el-input placeholder="模糊搜索" v-model="params.usernameLike"></el-input>
            </el-form-item>
            <el-row>
                <el-col :span="18">
                    <el-button type="success" round icon="el-icon-plus" v-router url="manage.html">添加</el-button>
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
            <el-table-column prop="username" label="名称"></el-table-column>
            <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button v-router v-query:id="scope.row.id" url="manage.html" type="text" size="small">编辑</el-button>
                    <el-button @click="remove(scope.row)" type="text" size="small">删除</el-button>
                </template>
            </el-table-column>
        </x-table>
    </el-card>
</template>
<script>
    import { testTable } from '../libs/apis';

    export default {
        keepAlive: true,
        data() {
            return {
                params: {},
                findAll: testTable.findAll
            }
        },
        methods: {
            remove(row) {
                const vm = this;
                msg.confirm(`是否删除“${row.username}”？`, () => {
                    testTable.remove(row.id).then(r => {
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

