(function () {
    angular.module('timeClock.timePicker').controller("TimePickerController", timePickerController);

    function timePickerController($scope) {

        $scope.time = new Date();
        $scope.hour = hour();

        $scope.addHour = addHour;

        function hour() {
            return $scope.time.getHours() % 12 || 12;
        }

        function addHour() {
            $scope.time.setHours();
        }


    }
})();