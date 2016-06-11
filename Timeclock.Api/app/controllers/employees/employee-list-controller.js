(function() {
    timeClock.controller("EmployeeListController", employeeListController);

    function employeeListController($scope, employeeRepository) {
        $scope.employees = employeeRepository.get();

        $scope.changeClockStatus = function(employee) {
            var self = this;
            employeeRepository.changeClockStatus(employee.employeeId, !employee.currentStatus)
                .success(function(data, status, headers, config) {
                    self.employee.currentStatus = !self.employee.currentStatus;
                    self.employee.lastPunchTime = data.lastPunchTime;
                });
        }

        $scope.formatTime = function (dateTime) {
            return moment(dateTime).format("hh:mm A - D/MM/YY");
        }
    }
})();