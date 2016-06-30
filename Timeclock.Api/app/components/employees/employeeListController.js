(function() {
    angular.module('timeClock.employee').controller("EmployeeListController", employeeListController);

    function employeeListController($scope, employeeRepository, NgTableParams, $uibModal) {
        var self = this;

        $scope.employees = [];
        self.tableParams = new NgTableParams({}, {});

        $scope.selectedEmployee = null;

        $scope.changeClockStatus = changeClockStatus;

        var init = function() {
            employeeRepository.get().success(function(data) {
                $scope.employees = data;
                $scope.tableParams = new NgTableParams({}, {dataset: data});
            });

        }

        function changeClockStatus(employee) {
            $scope.selectedEmployee = employee;

            if (employee.requiresAuthentication) {
                showPinModal();
            } else {
                submitChangeClockRequest();
            }
        }

        function submitChangeClockRequest (pin) {

            var employee = $scope.selectedEmployee;

            employeeRepository.changeClockStatus(employee.employeeId, !employee.currentStatus, pin)
                .success(function(data, status, headers, config) {
                    employee.currentStatus = !employee.currentStatus;
                    employee.lastPunchTime = data.lastPunchTime;
                    $scope.employee = {};
                })
                .error(function() {
                    $scope.employee = {};
                });
        }
        function showPinModal() {

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/app/components/employees/pinModal.html',
                controller: 'EmployeePinModalController'
            });
            modalInstance.result.then(function (pin) {
                submitChangeClockRequest(pin);
            }.bind(this));
        }
        init();
    }
})();