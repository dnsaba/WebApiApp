(function () {
    "use strict";
    angular
        .module("routerApp")
        .factory("decksService", Deckservice);

    DecksService.$inject = ["$http", "$q"];

    function DecksService($http, $q) {
        return {
            getUrlData: _getUrlData
        };

        function _getUrlData(data) {
            return $http.post("/api/urlData/get", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return (res);
        }

        function error(err) {
            return (err);
        }
    }
})();