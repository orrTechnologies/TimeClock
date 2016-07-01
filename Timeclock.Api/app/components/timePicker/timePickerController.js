(function () {
    angular.module('timeClock.timePicker').controller("TimePickerController", timePickerController);

    function timePickerController($scope, moment) {

        $scope.momentTime = moment();

        $scope.addHour = addHour;
        $scope.subtractHour = subtractHour;
        $scope.addTenMinutes = addTenMinutes;
        $scope.subtractTenMinutes = subtractTenMinutes;
        $scope.addMinute = addMinute;
        $scope.subtractMinute = subtractMinute;
        $scope.time = time;

        function time() {
            return $scope.momentTime.format();
        }

        function addHour() {
            $scope.momentTime.add(1, 'h');
            updateDate();
        }
        function subtractHour() {
            $scope.momentTime.add(-1, 'h');
            updateDate();
        }
        function addTenMinutes() {
            $scope.momentTime.add(10, 'm');
            updateDate();
        }
        function subtractTenMinutes() {
            $scope.momentTime.add(-10, 'm');
            updateDate();
        }
        function addMinute() {
            $scope.momentTime.add(1, 'm');
            updateDate();
        }
        function subtractMinute() {
            $scope.momentTime.add(-1, 'm');
            updateDate();
        }

        function updateDate() {
            $scope.datePicked = $scope.momentTime.toDate();
        }

    }
})();