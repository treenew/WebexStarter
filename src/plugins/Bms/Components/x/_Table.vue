<template>
    <template v-show="show">
        <el-table :data='data' stripe border ref="table"
                  v-loading="loading"
                  @selection-change="selectionChange">
            <slot></slot>
        </el-table>
        <el-pagination background ref="pagination"
                       @size-change="sizeChange"
                       @current-change="currentChange"
                       :current-page="current"
                       :page-sizes="[10, 20, 50, 100]"
                       :page-size="size"
                       layout="total, sizes, prev, pager, next, jumper"
                       :total="total">
        </el-pagination>
    </template>
</template>
<script>

    export default {
        props: {
            url: {
                type: [String, Function],
                required: true
            },
            params: Object,
            selectedRows: Array,
            filterRef: String,
            size: {
                default: 10,
                type: Number
            }
        },
        data() {
            return {
                loading: false,
                show: false,
                data: [],
                total: 100,
                current: 1
            }
        },
        created() {
            const vm = this;
            if (vm.filterRef) {
                var page = vm.$findPage();
                var filterRef = page.$refs[vm.filterRef];
                if (!filterRef) return;
                var form = filterRef.$el;
                if (!form || form.nodeName != 'FORM') return;

                form.onsubmit = function($event) {
                    vm.refresh();
                    $event.preventDefault();
                };
            }
        },
        init() {
            const vm = this;

            vm.show = true;
            vm.$nextTick(() => vm.refresh(vm.current, vm.size));
        },
        deinit() {
            const vm = this;
            vm.show = false;
        },
        methods: {
            doLayout() {
                const vm = this;
                vm.$refs.table.doLayout();
            },
            sizeChange(size) {
                const vm = this;
                vm.refresh(1, size);

            },
            currentChange(current) {
                const vm = this;
                vm.refresh(current, vm.size);
            },
            refresh(current, size) {
                const vm = this;
                if (vm.loading) return;
                vm.loading = true;

                if (vm.current != current) vm.current = current || 1;
                if (vm.size != size) vm.size = size || vm.size;

                var params = vm.params || {};
                params.pageNumber = vm.current;
                params.pageSize = vm.size;
                if (!vm.url) {
                    msg.error("表格配置错误，没有传递 url 属性。");
                    return;
                }
                var fc = typeof vm.url === 'function' ? vm.url(params) : api(vm.url, params);
                fc.then((r) => {
                    if (r.status) {
                        msg.error(r.message);
                        return;
                    }
                    var resp = r.value;

                    vm.data = resp.rows;
                    vm.total = +resp.total;
                    vm.$emit('refresh', resp);
                    vm.$nextTick(() => {
                        vm.loading = false;
                    });
                });
            },
            selectionChange(rows) {
                const vm = this;
                vm.$emit('update:selectedRows', rows);
                vm.$emit('selection-change', rows);
            }
        }
    }
</script>