(function () {
    "use strict";
    angular
        .module("routerApp")
        .controller("createController", CreateController);

    CreateController.$inject = ["$scope", "createService"];

    function CreateController($scope, CreateService) {
        var vm = this;
        vm.$scope = $scope;
        vm.createService = CreateService;
        vm.showImg = false;
        vm.creating = false;
        vm.allCards = [];
        vm.uploadImage = {};
        vm.cropper = {};
        vm.cropper.sourceImage = null;
        vm.cropper.croppedImage = null;
        vm.bounds = {};
        vm.bounds.left = 0;
        vm.bounds.right = 0;
        vm.bounds.top = 0;
        vm.bounds.bottom = 0;
        vm.newcard = {};
        vm.comboCard;
        vm.editItem = {};
        vm.editId = null;
        vm.cardList;
        vm.databaseCards = [];
        vm.urlData = {
            url: ""
        };
        vm.deleteFileData = {};

        vm.$onInit = _onInit;
        vm.selectAllCards = _selectAllCards;
        vm.selectAllCardsSuccess = _selectAllCardsSuccess;
        vm.selectAllCardsError = _selectAllCardsError;
        vm.uploadFile = _uploadFile;
        vm.uploadFileSuccess = _uploadFileSuccess;
        vm.uploadFileError = _uploadFileError;
        vm.createCard = _createCard;
        vm.createCardSuccess = _createCardSuccess;
        vm.createCardError = _createCardError;
        vm.drawCanvas = _drawCanvas;
        vm.drawBase = _drawBase;
        vm.cardText = _cardText;
        vm.editCard = _editCard;
        vm.editCardSuccess = _editCardSuccess;
        vm.editCardError = _editCardError;
        vm.deleteCard = _deleteCard;
        vm.deleteCardSuccess = _deleteCardSuccess;
        vm.deleteCardError = _deleteCardError;
        vm.getUrlData = _getUrlData;
        vm.getUrlDataSuccess = _getUrlDataSuccess;
        vm.getUrlDataError = _getUrlDataError;
        vm.deleteFile = _deleteFile;
        vm.deleteFileSuccess = _deleteFileSuccess;
        vm.deleteFileError = _deleteFileError;

        function _onInit() {
            vm.selectAllCards();
            vm.drawBase("/images/cardtemp.jpg");
            vm.drawCanvas();
            vm.cardText();
        }

        function _drawBase(imgSrc) {

            var myCanvas = document.getElementById('cardCanvas');
            var ctx = myCanvas.getContext('2d');
            var img = new Image();

            img.onload = function () {
                myCanvas.width = 350;
                myCanvas.height = 450;
                ctx.drawImage(img, 0, 0, 350, 450);
            };
            img.crossOrigin = '';
            img.src = imgSrc;
        }

        function _drawCanvas() {
            document.getElementById('myCanvas').addEventListener('click', function (event) {
                var myCanvas = document.getElementById('cardCanvas');
                var ctx = myCanvas.getContext('2d');
                var img = new Image();
                img.onload = function () {
                    ctx.drawImage(img, 43, 81, 266, 236);
                };
                img.crossOrigin = '';
                img.src = vm.cropper.croppedImage;
            });
        }

        function _cardText() {
            $('#cardName').keypress(function () {
                var key = event.keyCode || event.charCode;

                if (key == 8 || key == 46) {
                    return false;
                } else {
                    var myCanvas = document.getElementById('cardCanvas');
                    var ctx = myCanvas.getContext('2d');
                    var text = document.getElementById('cardName').value;

                    ctx.font = "20px Georgia"
                    ctx.strokeStyle = 'black';
                    ctx.lineWidth = 1;
                    ctx.strokeText(text, 30, 45);
                    ctx.fillStyle = 'black';
                    ctx.fillText(text, 30, 45);
                }
            });

            $('#cardDes').keypress(function () {
                var key = event.keyCode || event.charCode;

                if (key == 8 || key == 46) {
                    return false;
                } else {
                    var myCanvas = document.getElementById('cardCanvas');
                    var ctx = myCanvas.getContext('2d');
                    var text = document.getElementById('cardDes').value;

                    ctx.font = "18px Georgia"
                    ctx.strokeStyle = 'black';
                    ctx.lineWidth = 1;
                    ctx.strokeText(text, 30, 360);
                    ctx.fillStyle = 'black';
                    ctx.fillText(text, 30, 360);
                }
            });
        }

        function _selectAllCards() {
            vm.createService.selectAll()
                .then(vm.selectAllCardsSuccess).catch(vm.selectAllCardsError);
        }

        function _selectAllCardsSuccess(res) {
            vm.allCards = [];
            var list = res.data.items;
            for (var i = 0; i < list.length; i++) {

                vm.allCards.push(list[i]);
            }
            console.log(vm.allCards);
        }

        function _selectAllCardsError(err) {
            console.log(err);
        }

        function _uploadFile() {
            var canvas = document.getElementById('cardCanvas');
            var dataURL = canvas.toDataURL();

            var imageInfo = dataURL.split(",");
            var getExtension = imageInfo[0].split("/");
            var extension = getExtension[1].split(";");
            vm.uploadImage.encodedImageFile = imageInfo[1];
            vm.uploadImage.fileExtension = "." + extension[0];

            vm.createService.uploadFile(vm.uploadImage)
                .then(vm.uploadFileSuccess).catch(vm.uploadFileError);
        }

        function _uploadFileSuccess(res) {
            console.log(res);
            vm.newcard.fileId = res.data.item;
            vm.createCard();
        }

        function _uploadFileError(err) {
            //vm.logService.error("An error occurred when attempting to upload an image/file ", "[public] - GalleryController.uploadFileError", err);
            console.log(err);
        }

        function _createCard() {
            vm.newcard.cardCombo = vm.comboCard.name;
            vm.newcard.cardComboAtk = vm.comboCard.attackLevel;
            vm.newcard.cardComboDef = vm.comboCard.defenseLevel;
            vm.createService.createCard(vm.newcard)
                .then(vm.createCardSuccess).catch(vm.createCardError);
        }

        function _createCardSuccess(res) {
            vm.creating = false;
            console.log(res);
            vm.newcard = {};
            vm.cropper.sourceImage = null;
            vm.cropper.croppedImage = null;
            $("#fileUploadInput").val(null);
            vm.drawBase("/images/cardtemp.jpg");
            vm.selectAllCards();
        }

        function _createCardError(err) {
            console.log(err);
        }

        function _editCard() {
            vm.editItem.cardCombo = vm.comboCard.name;
            vm.editItem.cardComboAtk = vm.comboCard.attackLevel;
            vm.editItem.cardComboDef = vm.comboCard.defenseLevel;
            vm.createService.update(vm.editId, vm.editItem)
                .then(vm.editCardSuccess).catch(vm.editCardError);
        }

        function _editCardSuccess(res) {
            console.log(res);
            vm.editItem = {};
            vm.selectAllCards();
        }

        function _editCardError(err) {
            console.log(err);
            vm.editId = null;
            vm.editItem = {};
        }

        function _deleteCard(card) {
            vm.deleteFileData.id = card.fileId;
            vm.deleteFileData.systemFileName = card.systemFileName;
            vm.createService.delete(card.id)
                .then(vm.deleteCardSuccess).catch(vm.deleteCardError);
        }

        function _deleteCardSuccess(res) {
            console.log(res);
            vm.deleteFile(vm.deleteFileData);
            vm.selectAllCards();
        }

        function _deleteCardError(err) {
            console.log(err);
        }

        function _deleteFile(data) {
            vm.createService.deleteFile(data)
                .then(vm.deleteFileSuccess).catch(vm.deleteFileError);
        }

        function _deleteFileSuccess(res) {
            console.log(res);
            vm.deleteFile = {};
        }

        function _deleteFileError(err) {
            console.log(err);
        }

        function _getUrlData() {
            if (vm.cardlist === '1') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=13301000&rp=99999";
            } else if (vm.cardlist === '2') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=11115001&rp=99999";
            } else if (vm.cardlist === '3') {
                vm.urlData.url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=1&sess=1&pid=16611000&rp=99999";
            }
            vm.createService.getUrlData(vm.urlData)
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
    }
})();