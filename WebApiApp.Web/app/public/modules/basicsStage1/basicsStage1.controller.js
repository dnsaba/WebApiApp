(function () {
    "use strict";
    angular
        .module("publicApp")
        .component("basicsStage1", {
            templateUrl: "/app/public/modules/basicsStage1.html",
            controller: "basicsStage1Controller"
        });
})();

(function () {
    "use strict";
    angular
        .module("publicApp")
        .controller("basicsStage1Controller", BasicsStage1Controller);

    BasicsStage1Controller.$inject = ["$scope", "basicsStage1Service"];

    function BasicsStage1Controller($scope, basicsStage1Service) {
        var vm = this;
        vm.$scope = $scope;
        vm.basicsStage1Service = basicsStage1Service;
        vm.learning = true;
        vm.reviewing = false;

        vm.$onInit = _onInit;
        vm.startQuiz = _startQuiz;
        vm.submitQuiz = _submitQuiz;

        function _onInit() {
            console.log("basics stage 1 onInit");
        }

        function _startQuiz() {
            console.log("starting quiz");
            vm.learning = false;
            vm.reviewing = true;
        }

        function _submitQuiz() {

        }
    }
})()