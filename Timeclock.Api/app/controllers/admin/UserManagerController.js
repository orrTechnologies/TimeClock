(function() {
    timeClock.controller("EmployeeListController", employeeListController);

    function employeeListController($scope, employeeRepository, statusMessageService) {
        $scope.employees = employeeRepository.get();

        $scope.hasError = false;
        $scope.newEmployee = {
            firstName: "",
            lastName: ""
        };
        $scope.changeClockStatus = function(employee) {
            var self = this;
            employeeRepository.changeClockStatus(employee.employeeId, !employee.currentStatus)
                .success(function(data, status, headers, config) {
                    self.employee.currentStatus = !self.employee.currentStatus;
                    self.employee.lastPunchTime = data.lastPunchTime;
                });
        }

        $scope.save = function () {
            console.log(this.newEmployee);
            employeeRepository.saveNewEmployee(this.newEmployee)
                .success(function () {
                    this.hasError = false;
                    console.log("Employee successfully saved.");
                    statusMessageService.add("Successfully added employee", 1);
                    $location.path('admin');
                })
                .error(function () {
                    statusMessageService.add("Failed to add employee", 4);
                });
        }
        $scope.formatTime = function (dateTime) {
            return moment(dateTime).format("hh:mm A - D/MM/YY");
        }
    }
})();