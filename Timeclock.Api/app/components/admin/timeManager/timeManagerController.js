(function () {
angular.module('timeClock.timeManager').controller("TimeManagerController", timeManagerController);

    function timeManagerController($scope, $location, employeeRepository, reportsRepository) {
        $scope.selectedId = [];
        $scope.employeeList = [];
        $scope.startTime = '';
        $scope.endTime = '';

        $scope.endDateOptions = {};
        $scope.startDateOptions = {};

        $scope.timeReports = [];

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.startInfo = {
            time: '', 
            opened: false
         }
        $scope.endInfo = {
            time: '',
            opened: false
        }
        var init = function () {
           employeeRepository.get().success(function (employees) {
               employees.forEach(function (employee) {
                    $scope.employeeList.push(employee);
                });
            });
        }

        $scope.openStart = function () {
            $scope.startInfo.opened = true;
        };
        $scope.openEnd = function () {
            $scope.endInfo.opened = true;
        };

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