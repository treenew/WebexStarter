export let testTable = {
    findAll(args) {
        return api("/bms-dbdemo/testTable/findAll.json", args);
    },
    remove(id) {
        return api("/bms-dbdemo/testTable/remove.json", id);
    },
    find(id) {
        return api("/bms-dbdemo/testTable/find.json", id);
    },
    manage(args) {
        return api("/bms-dbdemo/testTable/manage.json", args);
    }
};

