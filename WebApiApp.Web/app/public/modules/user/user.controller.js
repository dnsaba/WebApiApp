(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("userController", UserController);

    UserController.$inject = ["$scope", "userService"];

    function UserController($scope, UserService) {
        var vm = this;
        vm.userService = UserService;
        vm.item = {};
        vm.loginItem = {};

        // functions
        vm.$onInit = _onInit;
        vm.submitRegisterForm = _submitRegisterForm;
        vm.submitRegisterFormSuccess = _submitRegisterFormSuccess;
        vm.submitRegisterFormError = _submitRegisterFormError;
        vm.login = _login;
        vm.loginSuccess = _loginSuccess;
        vm.loginError = _loginError;

        function _onInit() {
            console.log("home controller init");
        }

        function _submitRegisterForm() {
            vm.userService.register(vm.item)
                .then(vm.submitRegisterFormSuccess).catch(vm.submitRegisterFormError);
        }

        function _submitRegisterFormSuccess(res) {
            console.log(res);
        }

        function _submitRegisterFormError(err) {
            console.log(err);
        }

        function _login() {
            vm.userService.login(vm.loginItem)
                .then(vm.loginSuccess)
                .catch(vm.loginError);
        }

        function _loginSuccess(res) {
            console.log(res);
        }

        function _loginError(err) {
            console.log(err);
        }
    }
})();