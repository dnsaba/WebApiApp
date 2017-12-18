(function () {
    "use strict";
    angular
        .module("routerApp")
        .controller("profileController", ProfileController);

    ProfileController.$inject = ["$scope", "profileService"];

    function ProfileController($scope, ProfileService) {
        var vm = this;
        vm.$scope = $scope;
        vm.profileService = ProfileService;
        vm.uploadImage = {};
        vm.cropper = {};
        vm.cropper.sourceImage = null;
        vm.cropper.croppedImage = null;
        vm.bounds = {};
        vm.bounds.left = 0;
        vm.bounds.right = 0;
        vm.bounds.top = 0;
        vm.bounds.bottom = 0;

        vm.$onInit = _onInit;
        vm.uploadFile = _uploadFile;
        vm.uploadFileSuccess = _uploadFileSuccess;
        vm.uploadFileError = _uploadFileError;

        function _onInit() {
            console.log("profile controller init");
        }

        function _uploadFile() {
            var image = vm.cropper.croppedImage;
            var imageInfo = image.split(",");
            var getExtension = imageInfo[0].split("/");
            var extension = getExtension[1].split(";");
            vm.uploadImage.encodedImageFile = imageInfo[1];
            vm.uploadImage.fileExtension = "." + extension[0];

            vm.profileService.uploadFile(vm.uploadImage)
                .then(vm.uploadFileSuccess).catch(vm.uploadFileError);

        }

        function _uploadFileSuccess(res) {
            console.log(res);
        }

        function _uploadFileError(err) {
            //vm.logService.error("An error occurred when attempting to upload an image/file ", "[public] - GalleryController.uploadFileError", err);
            console.log(err);
        }
    }
})();