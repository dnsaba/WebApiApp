(function () {
    "use strict";
    angular
        .module("publicApp")
        .factory("createService", CreateService);

    CreateService.$inject = ["$http", "$q"];

    function CreateService($http, $q) {
        return {
            uploadFile: _uploadFile,
            createCard: _createCard,
            selectAll: _selectAll,
            selectById: _selectById,
            update: _update,
            delete: _delete,
            getUrlData: _getUrlData,
            deleteFile: _deleteFile
        };

        function _uploadFile(file) {
            return $http.post("/api/create/fileUpload", file, { withCredentials: true })
                .then(success).catch(error);
        }

        function _createCard(data) {
            return $http.post("/api/create/newcard", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectAll() {
            return $http.get("/api/create", { withCredentials: true })
                .then(success).catch(error);
        }

        function _selectById(id) {
            return $http.get("/api/create/" + id, { withCredentials: true })
                .then(success).catch(error);
        }

        function _update(id, data) {
            return $http.put("/api/create/" + id, data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _delete(id) {
            return $http.delete("/api/create/" + id, { withCredentials: true })
                .then(success).catch(error);
        }

        function _getUrlData(data) {
            return $http.post("/api/urlData/get", data, { withCredentials: true })
                .then(success).catch(error);
        }

        function _deleteFile(data) {
            return $http.delete("/api/create/fileDelete",
                {
                    data,
                    headers: {
                        "Content-Type": "application/json;charset=utf-8"
                    }
                }
            ).then(success).catch(error);
        }

        function success(res) {
            return (res);
        }

        function error(err) {
            return (err);
        }
    }
})();