export let menu = {
    getMenus() {
        return api("/bms/menu/getMenus");
    }
};

export let user = {
    login(username, password) {
        return api("/bms/user/login", username, password);
    },
    logout() {
        return api("/bms/user/logout");
    },
    getIdentity() {
        return api("/bms/user/getIdentity");
    },
    findAll(args) {
        return api("/bms/user/findAll", args);
    },
    modifyStatus(id, status) {
        return api("/bms/user/modifyStatus", id, status);
    },
    find(id) {
        return api("/bms/user/find", id);
    },
    manage(args) {
        return api("/bms/user/manage", args);
    }
};

