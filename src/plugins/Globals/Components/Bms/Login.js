
import md5 from "/globals/node_modules/blueimp-md5/js/md5.min.js";

export default {
    props: {
        title: {
            type: String,
            default: '管理平台'
        },
        url: {
            type: String,
            default: 'index.html'
        }
    },
    data() {
        return {
            ruleForm: {
                username: '',
                password: ''
            },
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
    methods: {
        submitForm(formName) {
            let vm = this;
            vm.$refs[formName].validate((valid) => {
                api('user/login', vm.ruleForm.username, md5(vm.ruleForm.password))
                    .then(r => {
                        if (r.status) {
                            msg.error(r.message);
                        }
                        else {
                            var fromUrl = vm.query.from || vm.url;
                            location.href = fromUrl;
                        }
                    });
            });
        }
    }
}