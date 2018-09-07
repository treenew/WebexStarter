
<template title="分页表格演示">
    <h1>欢迎您 {{identity.nickname}} | [<bms-logout>立即退出</bms-logout>] | [<a href="../index.html">返回首页</a>]</h1>
    <el-card>
        <div slot="header">
            <span>用户列表</span>
        </div>
        <el-form ref="filter" :inline="true">
            <el-form-item label="账号">
                <el-input placeholder="模糊搜索" v-model="params.usernameLike"></el-input>
            </el-form-item>
            <el-form-item label="名称">
                <el-input placeholder="模糊搜索" v-model="params.nicknameLike"></el-input>
            </el-form-item>
            <el-row>
                <el-col :span="18">
                    <!--<el-button type="success" round icon="el-icon-plus" v-router url="manage">添加用户</el-button>-->
                </el-col>
                <el-col :span="6" align="right">
                    <el-button type="primary" icon="el-icon-search" native-type="submit">搜索</el-button>
                </el-col>
            </el-row>
        </el-form>
    </el-card>

    <el-card>
        <x-table :params="params" ref="table" filter-ref="filter" url="/bms-home/user/findall">
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
                    <!--<el-button v-router="{id:scope.row.id}" url="manage" type="text" size="small">编辑</el-button>
                    <el-button @click="modifyStatus(scope.row)" type="text" size="small">{{scope.row.status==1?'禁用':'启用'}}</el-button>
                    <el-button @click="initPassword(scope.row.id)" type="text" size="small">重置密码</el-button>
                    <el-button @click="remove(scope.row.id)" type="text" size="small">删除</el-button>-->
                </template>
            </el-table-column>
        </x-table>
    </el-card>
</template>
<script>

    var identity = api('/bms-home/user/getIdentity').sync().value;

    import "/globals/x/all.vue";
    import logout from "/globals/bms/logout.vue";

    export default {
        components: {
            "bms-logout": logout
        },
        data() {
            return {
                params: {}, identity
            }
        },
        methods: {
        }
    }
</script>