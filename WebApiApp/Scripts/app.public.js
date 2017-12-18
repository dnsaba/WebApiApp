(function () {
    'use strict';
    window.APP = window.APP || {};
    APP.NAME = "routerApp";
    angular.module(APP.NAME, ['ui.router', 'routerApp.routes', 'angular-img-cropper']);
})();