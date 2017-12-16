(function () {
    "use strict";
    var myApp = angular.module("routerApp" + '.routes', []);
    myApp.config(function ($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');

        $stateProvider

            // HOME STATES AND NESTED VIEWS ========================================
            .state('home', {
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/partialHome.html',
                controller: "userController as userCtrl"
            })

            // ABOUT PAGE AND MULTIPLE NAMED VIEWS =================================
            .state('profile', {
                name: 'profile',
                url: '/profile',
                templateUrl: '/app/public/profile.html'
            })

            .state('duel', {
                name: 'duel',
                url: '/duel',
                templateUrl: '/app/public/duel.html'
            });

    });
})();


