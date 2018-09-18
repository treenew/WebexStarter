+(function() {

    window.goLoginPage = function() {
        location.href = "/bms/user/login.html?from=" + encodeURIComponent(location.pathname + location.search);
    }

    window.unauthorized = function(url, data, err) {
        msg.confirm("您的权限不足，是否跳转到登录界面？", window.goLoginPage);
    }

    window.msg = {
        confirm: function(message, callback) {
            mainApp.$confirm(message, {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(callback).catch(function() { });
        },
        alert: function(message) {
            mainApp.$alert(message + '');
        },
        success: function(message) {
            mainApp.$message({
                type: 'success',
                message: message
            });
        },
        info: function(message) {
            mainApp.$message({
                type: 'info',
                message: message
            });
        },
        warning: function(message) {
            mainApp.$message({
                type: 'warning',
                message: message
            });
        },
        error: function(message) {
            mainApp.$message.error(message + '');
        },
        loading: function(message) {
            msg.$loading = mainApp.$loading({
                lock: true,
                text: message || '正在加载...',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
        },
        loadingClose: function() {
            msg.$loading && msg.$loading.close();
        }
    };

    //   districtProps: { value: 'v', label: 'n', children: 'c' },
    Vue.getCascaderSelectedOptions = function(options, props, selectedValues) {
        var selectedOptions = selectedValues.map(function(value) {
            for (var option of options) {
                if (option[props.value] == value) {
                    options = option[props.children];
                    return option;
                }
            }
            return null;
        });
        return selectedOptions;
    }

    Vue.getCascaderSelectedLabels = function(options, props, selectedValues) {
        var selectedOptions = Vue.getCascaderSelectedOptions(options, props, selectedValues);

        return selectedOptions.map(function(option) { return option[props.label] });
    }

    function pickTime(picker, start, end) {
        var defaultTime = picker.defaultTime;
        if (defaultTime && defaultTime.length == 2) {
            start = moment(moment(start).format('L') + ' ' + defaultTime[0]).toDate();
            end = moment(moment(end).format('L') + ' ' + defaultTime[1]).toDate();
        }
        picker.$emit('pick', [start, end]);
    }

    Vue.prototype.$pickerOptions = {
        shortcuts: [{
            text: '今天',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today();
                pickTime(picker, start, end);
            }
        }, {
            text: '昨天',
            onClick: function(picker) {
                var end = Date.today().addDate(-1);
                var start = Date.today().addDate(-1);
                pickTime(picker, start, end);
            }
        }, {
            text: '本周',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today().thisWeek();
                pickTime(picker, start, end);
            }
        }, {
            text: '上周',
            onClick: function(picker) {
                var start = Date.today().thisWeek(-1);
                var end = start.addDate(6);
                pickTime(picker, start, end);
            }
        }, {
            text: '本月',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today().thisMonth();
                pickTime(picker, start, end);
            }
        }, {
            text: '上月',
            onClick: function(picker) {
                var start = Date.today().thisMonth(-1);
                var end = start.thisMonth(1).addDate(-1);
                pickTime(picker, start, end);
            }
        }, {
            text: '最近一周',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today().lastWeek();
                pickTime(picker, start, end);
            }
        }, {
            text: '最近一个月',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today().lastMonth();
                pickTime(picker, start, end);
            }
        }, {
            text: '最近三个月',
            onClick: function(picker) {
                var end = Date.today();
                var start = Date.today().lastMonth(3);
                pickTime(picker, start, end);
            }
        }]
    }
})();