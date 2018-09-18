<header>
    <link rel="stylesheet" href="/globals/node_modules/element-ui/lib/theme-chalk/index.css">

    <link href="/bms/assets/css/site.css" rel="stylesheet" />
</header>
<footer>
    <script src="/globals/node_modules/vue/dist/vue.min.js"></script>
    <script src="/globals/node_modules/element-ui/lib/index.js"></script>
    <script src="/globals/assets/vue.ext.js"></script>

    <script src="/bms/assets/js/bms.core.js"></script>
</footer>

<template title="登录" layout="false">
    <div class="wrap" v-if="loaded">
        <div class="wpn">
            <div class="form-data">
                <a href="javascript:;"><img src="/bms/assets/images/head-logo.png" class="head-logo"></a>
                <div class="change-login">
                    <p class="account_number on">账号登录</p>
                    <p class="message" onclick="msg.info('短信登录尚未开放哦。')">短信登录</p>
                </div>
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px" @submit.native.prevent>
                    <el-form-item prop="username">
                        <el-input v-model="ruleForm.username" placeholder="平台账号"></el-input>
                    </el-form-item>
                    <el-form-item prop="password">
                        <el-input type="password" placeholder="平台密码" v-model="ruleForm.password" @keyup.enter.native="submitForm('ruleForm')"></el-input>
                    </el-form-item>

                    <div class="r-forget cl">
                        <a href="javascript:msg.info('如您忘记密码，请联系系统管理员。');" class="y">忘记密码</a>
                    </div>
                    <div class="login-btn">
                        <el-button type="primary" @click="submitForm('ruleForm')">登录</el-button>
                    </div>
                </el-form>
                <p class="right">您当前正在登录的平台是 <span style="color:white">{{title}}</span>  \ Powered by © 2018</p>
            </div>
        </div>
    </div>
</template>