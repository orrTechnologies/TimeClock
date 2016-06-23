(function() {
    timeClock.controller("EmployeeListController", employeeListController);

    function employeeListController($scope, employeeRepository, NgTableParams, $uibModal) {
        var self = this;

        $scope.employees = [];
        self.tableParams = new NgTableParams({}, {});

        $scope.selectedEmployee = null;

        var init = function() {
            employeeRepository.get().success(function(data) {
                $scope.employees = data;
                $scope.tableParams = new NgTableParams({}, {dataset: data});
            });

        }

        $scope.changeClockStatus = function (employee) {
            $scope.selectedEmployee = employee;

            if (employee.requiresAuthentication) {
                showPinModal();
            } else {
                submitChangeClockRequest();
            }
        }

        var submitChangeClockRequest = function (pin) {

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

        $scope.formatTime = function (dateTime) {
            return moment(dateTime).format("hh:mm A - D/MM/YY");
        }

        var showPinModal = function () {

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/templates/pinModal.html',
                controller: 'EmployeePinModalController'
            });
            modalInstance.result.then(function (pin) {
                submitChangeClockRequest(pin);
            }.bind(this));
        }
        init();
    }
})();