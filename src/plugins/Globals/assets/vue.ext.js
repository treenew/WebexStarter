(function() {

    Vue.directive('class', function(el, binding, vnode) {
        var key = binding.arg;
        var value = binding.value;
        el.classList.remove(key);
        value && el.classList.add(key);
    });

})();