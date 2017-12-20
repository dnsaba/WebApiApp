(function () {
    "use strict";
    angular
        .module("routerApp")
        .controller("decksController", DecksController);

    DecksController.$inject = ["$scope", "decksService"];

    function DecksController($scope, DecksService) {
        var vm = this;
        vm.$scope = $scope;
        vm.decksService = DecksService;

        vm.$onInit = _onInit;
        vm.getUrlData = _getUrlData;

        function _onInit() {
            console.log("deck controller init");
            vm.getUrlData();
        }

        function _getUrlData() {
            vm.decksService.getUrlData()
                .then(vm.getUrlDataSuccess)
        }
    }
})();