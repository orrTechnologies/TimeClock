(function () {
    timeClock.controller("TimeWorkedReportController", timeWorkedReportController);

    function timeWorkedReportController($scope, $location, employeeRepository, reportsRepository) {
        $scope.selectedIds = [];
        $scope.employeeList = [];
        $scope.startTime =  '';
        $scope.endTime = '';

        $scope.endDateOptions = {};
        $scope.startDateOptions = {};

        $scope.timeReports = [];
        var init = function() {
           employeeRepository.get().$promise.then(function (employees) {
               employees.forEach(function (employee) {
                    $scope.employeeList.push(employee);
                });
            });
        }

        $scope.submit = function() {
            reportsRepository.load({ employeeIds: $scope.selectedIds, startTime: $scope.startTime, endTime: $scope.endTime })
                .success(function(data) {
                $scope.timeReports = data;
            });
        }

        var setEndDateOptions = function() {
            $scope.endDateOptions = { minDate: $scope.startTime };
        }

        var setStartDateOptions = function() {
            $scope.startDateOptions = { maxDate: $scope.endTime };
        }
        $scope.startTimeChanged = function (startTime) {
            setEndDateOptions();
        }
        $scope.endTimeChanged = function (endTime) {
            setStartDateOptions();
        }
        $scope.back = function() {
            $scope.timeReports = [];
        }
        init();
    }
})();