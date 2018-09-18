
import { user } from "../libs/apis";
import md5 from "/globals/node_modules/blueimp-md5/js/md5.min.js";

export default {
    isPage: true,
    data() {
        return {
            title: "超级管理平台",
            index: '../index.html',
            ruleForm: {
                username: '',
                password: ''
            },
            loaded: false,
            rules: {
                username: [
                    { required: true, message: '请输入用户名', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '请输入密码', trigger: 'blur' }
                ]
            }
        }
    },
    mounted() {
        const vm = this;
        var identity = user.getIdentity().sync().value;
        if (identity != null) vm.goIndex();
        vm.loaded = true;
    },
    methods: {
        goIndex() {
            const vm = this;
            var fromUrl = vm.query.from || vm.index;
            location.href = fromUrl;
        },
        submitForm(formName) {
            let vm = this;
            vm.$refs[formName].validate((valid) => {
                user.login(vm.ruleForm.username, md5(vm.ruleForm.password))
                    .then(r => {
                        if (r.status) {
                            msg.error(r.message);
                        }
                        else {
                            vm.goIndex();
                        }
                    });
            });
        }
    }
}