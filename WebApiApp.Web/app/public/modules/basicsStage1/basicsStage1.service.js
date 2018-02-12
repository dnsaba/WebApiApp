(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("basicsStage1Service", BasicsStage1Service);

    BasicsStage1Service.$inject = ["$http", "$q"];

    function BasicsStage1Service($http, $q) {
        return {
            get: _get
        }

        function _get() {
            return $http.get("/api/basicsstg1", { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();