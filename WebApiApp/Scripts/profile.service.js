(function () {
    "use strict";
    angular
        .module("routerApp")
        .factory("profileService", ProfileService);

    ProfileService.$inject = ["$http", "$q"];

    function ProfileService($http, $q) {
        return {
            uploadFile: _uploadFile
        };

        function _uploadFile(file) {
            return $http.post("/api/profile/fileUpload", file, { withCredentials: true })
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