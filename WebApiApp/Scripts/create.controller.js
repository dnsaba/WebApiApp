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

        function _onInit() {
            console.log("create controller init");
            vm.drawBase();
            vm.drawCanvas();
            vm.cardText();
            vm.selectAllCards();
        }

        function _drawBase() {

            var myCanvas = document.getElementById('cardCanvas');
            var ctx = myCanvas.getContext('2d');
            var img = new Image();
            img.onload = function () {
                myCanvas.width = 350;
                myCanvas.height = 450;

                ctx.drawImage(img, 0, 0, 350, 450);
            };

            img.setAttribute('crossOrigin', 'anonymous');
            img.src = "http://www.navilagando.com/wp-content/uploads/2017/08/Yugioh-Card-Template-7.png";

        }

        function _drawCanvas() {
            document.getElementById('myCanvas').addEventListener('click', function (event) {
                var myCanvas = document.getElementById('cardCanvas');
                var ctx = myCanvas.getContext('2d');
                var img = new Image();
                img.onload = function () {
                    ctx.drawImage(img, 43, 81, 266, 236);
                };

                img.setAttribute('crossOrigin', 'anonymous');
                img.src = vm.cropper.croppedImage;
            });
        }

        function _cardText() {
            $('#cardName').keypress(function () {
                var myCanvas = document.getElementById('cardCanvas');
                var ctx = myCanvas.getContext('2d');
                var text = document.getElementById('cardName').value;

                ctx.font = "20px Sans-serif"
                ctx.strokeStyle = 'black';
                ctx.lineWidth = 4;
                ctx.strokeText(text, 10, 50);
                ctx.fillStyle = 'white';
                ctx.fillText(text, 10, 50);
                console.log(text);

            });
        }

        function _selectAllCards() {
            vm.createService.selectAll()
                .then(vm.selectAllCardsSuccess).catch(vm.selectAllCardsError);
        }

        function _selectAllCardsSuccess(res) {
            vm.allCards = res.data.items;
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
            vm.cropper.sourceImage = null;
            vm.cropper.croppedImage = null;
            document.getElementById("fileUploadInput").val(null);
        }

        function _uploadFileError(err) {
            //vm.logService.error("An error occurred when attempting to upload an image/file ", "[public] - GalleryController.uploadFileError", err);
            console.log(err);
        }

        function _createCard() {
            vm.createService.createCard(vm.newcard)
                .then(vm.createCardSuccess).catch(vm.createCardError);
        }

        function _createCardSuccess(res) {
            console.log(res);
        }

        function _createCardError(err) {
            console.log(err);
        }
    }
})();