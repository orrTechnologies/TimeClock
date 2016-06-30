(function () {
    angular.module('timeClock.timePicker').controller("TimePickerDevController", timePickerDevController);

    function timePickerDevController($scope, moment) {
        $scope.date = new Date();
    }
})();