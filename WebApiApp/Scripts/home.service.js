(function () {
    "use strict";
    angular
        .module("routerApp")
        .factory("homeService", HomeService);

   // RegisterService.$inject = ["$http", "$q"];

    function HomeService() {
        return {
            register: _register
        };

        function _register() {
            return "hi";
        }
    }
})();