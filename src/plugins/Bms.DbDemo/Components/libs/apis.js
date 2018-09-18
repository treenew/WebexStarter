export let testTable = {
    findAll(args) {
        return api("/bms-dbdemo/testTable/findAll", args);
    },
    remove(id) {
        return api("/bms-dbdemo/testTable/remove", id);
    },
    find(id) {
        return api("/bms-dbdemo/testTable/find", id);
    },
    manage(args) {
        return api("/bms-dbdemo/testTable/manage", args);
    }
};

