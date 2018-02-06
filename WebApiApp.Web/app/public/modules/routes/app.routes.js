(function () {
    "use strict";
    var myApp = angular.module("publicApp" + '.routes', []);
    myApp.config(function ($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');

        $stateProvider

            .state('home', {
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/partialHome.html',
                controller: "userController as userCtrl"
            })

            .state('profile', {
                name: 'profile',
                url: '/profile',
                templateUrl: '/app/public/modules/profile/profile.html',
                controller: "profileController as proCtrl"
            })

            .state('duel', {
                name: 'duel',
                url: '/duel',
                templateUrl: '/app/public/modules/duel/duel.html'
            })

            .state('create', {
                name: 'create',
                url: '/create',
                templateUrl: '/app/public/modules/newCard/createCard.html',
                controller: "createController as createCtrl"
            })

            .state('decks', {
                name: 'decks',
                url: '/decks',
                templateUrl: '/app/public/modules/createDeck/decks.html',
                controller: "decksController as decksCtrl"
            });

    });
})();



//(function () {
//    "use strict";
//    var myApp = angular.module("routerApp" + '.routes', []);
//    myApp.config(function ($stateProvider, $locationProvider, $urlRouterProvider) {
//        $locationProvider.html5Mode({
//            enabled: true,
//            requireBase: false,
//        });
//        $urlRouterProvider.otherwise('/home');

//        $stateProvider

//            // HOME STATES AND NESTED VIEWS ========================================
//            .state('home', {
//                name: 'home',
//                url: '/home',
//                templateUrl: '/app/public/partialHome.html',
//                controller: "userController as userCtrl"
//            })

//            // ABOUT PAGE AND MULTIPLE NAMED VIEWS =================================
//            .state('profile', {
//                name: 'profile',
//                url: '/profile',
//                templateUrl: '/app/public/profile.html',
//                controller: "profileController as proCtrl"
//            })

//            .state('duel', {
//                name: 'duel',
//                url: '/duel',
//                templateUrl: '/app/public/duel.html'
//            })

//            .state('create', {
//                name: 'create',
//                url: '/create',
//                templateUrl: '/app/public/createCard.html',
//                controller: "createController as createCtrl"
//            })

//            .state('decks', {
//                name: 'decks',
//                url: '/decks',
//                templateUrl: '/app/public/decks.html',
//                controller: "decksController as decksCtrl"
//            });

//    });
//})();


