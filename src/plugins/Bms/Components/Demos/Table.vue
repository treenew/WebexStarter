
<template title="分页表格演示">
    <el-card>
        <div slot="header">
            <span>分页表格演示</span>
        </div>
        <el-form ref="filter" :inline="true">
            <el-form-item label="账号">
                <el-input placeholder="模糊搜索" v-model="params.usernameLike"></el-input>
            </el-form-item>
            <el-form-item label="名称">
                <el-input placeholder="模糊搜索" v-model="params.nicknameLike"></el-input>
            </el-form-item>
            <el-row>
                <el-col :offset="18" :span="6" align="right">
                    <el-button type="primary" icon="el-icon-search" native-type="submit">搜索</el-button>
                </el-col>
            </el-row>
        </el-form>
    </el-card>

    <el-card>
        <x-table :params="params" ref="table" filter-ref="filter" :url="findAll">
            <el-table-column prop="username" label="账号"></el-table-column>
            <el-table-column prop="nickname" label="名称"></el-table-column>
            <el-table-column prop="createTime" label="创建时间" :formatter="$dateFormatter"></el-table-column>
            <el-table-column label="状态">
                <template slot-scope="scope">
                    <span v-if="scope.row.status==1">启用</span>
                    <span v-else>禁用</span>
                </template>
            </el-table-column>
            <el-table-column label="操作">
                <template slot-scope="scope">
                </template>
            </el-table-column>
        </x-table>
    </el-card>
</template>
<script>

    import { user } from '../libs/apis';

    export default {
        data() {
            return {
                params: {},
                findAll: user.findAll
            }
        },
        init() {

        },
        deinit() {

        },
        methods: {
        }
    }
</script>