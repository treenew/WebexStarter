export default {
    // 过期时间，默认30天
    age: 30 * 24 * 60 * 60 * 1000,
    /**
     * 设置过期时间
     * @param age
     * @returns {exports}
     */
    setAge(age) {
        this.age = age;
        return this;
    },
    /**
     * 设置 localStorage
     * @param key
     * @param value
     * @param age
     */
    set(key, value, age) {
        localStorage.removeItem(key);
        var _time = new Date().getTime(),
            _age = age || this.age;

        // 如果不是对象，新建一个对象把 value 存起来

        value = { _value: value };

        // 加入时间
        value._time = _time;
        // 过期时间
        value._age = _time + _age;
        localStorage.setItem(key, JSON.stringify(value));
        return this;
    },
    /**
     * 判断一个 localStorage 是否过期
     * @param key
     * @returns {boolean}
     */
    isExpire(key) {

        var isExpire = true,
            value = localStorage.getItem(key),
            now = new Date().getTime();

        if (value) {
            value = JSON.parse(value);
            // 当前时间是否大于过期时间
            isExpire = now > value._age;
        } else {
            // 没有值也是过期
        }
        return isExpire;
    },
    /**
     * 获取某个 localStorage 值
     * @param key
     * @returns {*}
     */
    get(key) {
        var isExpire = this.isExpire(key),
            value = null;
        if (!isExpire) {
            value = localStorage.getItem(key);
            value = JSON.parse(value);
            value = value._value;
        }
        return value;
    }
}