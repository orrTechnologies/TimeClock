(function() {
    timeClock.controller("EmployeeListController", employeeListController);

    function employeeListController($scope, employeeRepository, NgTableParams) {
        var self = this;

        $scope.employees = [];
        self.tableParams = new NgTableParams({}, {});

        var init = function() {
            employeeRepository.get().success(function(data) {
                $scope.employees = data;
                $scope.tableParams = new NgTableParams({}, {dataset: data});
            });

        }

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

        init();
    }
})();