(function () {
    timeClock.controller("ReportsController", reportsController);

    function reportsController($scope, $location, employeeRepository, statusMessageService) {
        $scope.selectedIds = [];
        $scope.employeeList = [];
        $scope.startTime = '';
        $scope.endTime = '';

        var init = function() {
           employeeRepository.get().$promise.then(function (employees) {
               employees.forEach(function (employee) {
                    $scope.employeeList.push(employee);
                });
            });
        }

        $scope.submit = function() {
            console.log($scope.dt);
        }

        init();
    }
})();