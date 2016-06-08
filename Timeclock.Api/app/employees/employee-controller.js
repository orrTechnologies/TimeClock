timeClock.controller("EmployeeController", function ($scope, employeeRepository) {
    $scope.employees = employeeRepository.get();

    $scope.changeClockStatus = function (employee) {
        var self = this;
        employeeRepository.changeClockStatus(employee.employeeId, !employee.currentStatus)
            .success(function (data, status, headers, config) {
                self.employee.currentStatus = !self.employee.currentStatus;
                self.employee.lastPunchTime = data.lastPunchTime;
        });
    }
});