export let role = {
    findAll(args) {
        return api("/bms-sys/role/findAll.json", args);
    },
    modifyStatus(id, status) {
        return api("/bms-sys/role/modifyStatus.json", id, status);
    },
    find(id) {
        return api("/bms-sys/role/find.json", id);
    },
    manage(args) {
        return api("/bms-sys/role/manage.json", args);
    }
};

