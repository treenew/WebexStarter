export let role = {
    findAll(args) {
        return api("/bms-sys/role/findAll", args);
    },
    modifyStatus(id, status) {
        return api("/bms-sys/role/modifyStatus", id, status);
    },
    find(id) {
        return api("/bms-sys/role/find", id);
    },
    manage(args) {
        return api("/bms-sys/role/manage", args);
    }
};

