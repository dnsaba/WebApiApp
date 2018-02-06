(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("decksService", DecksService);

    DecksService.$inject = ["$http", "$q"];

    function DecksService($http, $q) {
        return {
            getUrlData: _getUrlData,
            createDeck: _createDeck
        };

        function _getUrlData(data) {
            return $http.post("/api/urlData/get", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _createDeck(data) {
            return $http.post("/api/decks", data, { withCredentials: true })
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