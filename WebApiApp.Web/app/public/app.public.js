﻿(function () {
    "use strict";
    window.APP = window.APP || {};
    APP.NAME = "publicApp";
    angular.module(APP.NAME, ['ui.router', 'publicApp.routes', 'angular-img-cropper'])
})();
