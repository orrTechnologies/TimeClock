timeClock.controller("EmployeeController", function ($scope, employeeRepository) {
    $scope.employees = employeeRepository.get();

    $scope.changeClockStatus = function(employee) {
        employeeRepository.changeClockStatus(employee.employeeId, 1)
            .success(function() {
            console.log("success");
        });
    }
});