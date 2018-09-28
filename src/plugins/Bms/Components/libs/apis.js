export let menu = {
    getMenus() {
        return api("/bms/menu/getMenus.json");
    }
};

export let user = {
    login(username, password) {
        return api("/bms/user/login.json", username, password);
    },
    logout() {
        return api("/bms/user/logout.json");
    },
    getIdentity() {
        return api("/bms/user/getIdentity.json");
    },
    findAll(args) {
        return api("/bms/user/findAll.json", args);
    },
    modifyStatus(id, status) {
        return api("/bms/user/modifyStatus.json", id, status);
    },
    find(id) {
        return api("/bms/user/find.json", id);
    },
    manage(args) {
        return api("/bms/user/manage.json", args);
    }
};

