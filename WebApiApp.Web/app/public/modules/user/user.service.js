(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("userService", UserService);

    UserService.$inject = ["$http", "$q"];

    function UserService($http, $q) {
        return {
            register: _register,
            login: _login
        };

        function _register(data) {
            return $http.post("/api/users/register", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _login(data) {
            return $http.post("/api/users/login", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return(res);
        }

        function error(err) {
            return(err);
        }
    }
})();