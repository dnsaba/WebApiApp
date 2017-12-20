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
        vm.cardlist;
        vm.urlData = {
            url: ""
        };
        vm.databaseCards = [];
        vm.userDeck = [];

        vm.$onInit = _onInit;
        vm.getUrlData = _getUrlData;
        vm.getUrlDataSuccess = _getUrlDataSuccess;
        vm.getUrlDataError = _getUrlDataError;
        vm.selectedCards = _selectedCards;
        vm.createDeck = _createDeck;
        vm.createDeckSuccess = _createDeckSuccess;
        vm.createDeckError = _createDeckError;

        function _onInit() {
            console.log("deck controller init");
        }

        function _getUrlData() {
            if (vm.cardlist === '1') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=13301000&rp=99999";
            } else if (vm.cardlist === '2') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=11115001&rp=99999";
            } else if (vm.cardlist === '3') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=16611000&rp=99999";
            }
            vm.decksService.getUrlData(vm.urlData)
                .then(vm.getUrlDataSuccess).catch(vm.getUrlDataError);
        }

        function _getUrlDataSuccess(res) {
            console.log(res);
            vm.databaseCards = res.data.item.cardsInfo;
            for (var i = 0; i < vm.databaseCards.length; i++) {
                vm.databaseCards[i].Selected = false;
            }
        }

        function _getUrlDataError(err) {
            console.log(err);
        }

        function _selectedCards() {
            for (var i = 0; i < vm.databaseCards.length; i++) {
                if (vm.databaseCards[i].Selected === true) {
                    vm.userDeck.push(vm.databaseCards[i]);
                    }
            }
        }

        function _createDeck() {
            vm.decksService.createDeck(vm.userDeck)
                .then(vm.createDeckSuccess).catch(vm.createDeckError);
        }

        function _createDeckSuccess(res) {
            console.log(res);
        }

        function _createDeckError(err) {
            console.log(err);
        }
    }
})();