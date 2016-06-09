(function () {
    timeClock.controller("EmployeeController", employeeController);

    function employeeController($scope) {

        $scope.formatTime = function (dateTime) {
            return moment(dateTime).format("hh:mm D/MM/YY");
        }

    }
})();