(function () {
    "use strict";
    angular
        .module("routerApp")
        .controller("homeController", HomeController);

    HomeController.$inject = ["$scope", "homeService"];

    function RegisterController($scope, HomeService) {
        var vm = this;
        vm.homeService = HomeService;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("home controller init");
        }
    }
})();