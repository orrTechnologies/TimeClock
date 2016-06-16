(function() {
    timeClock.controller("UserManagerController", userManagerController);

    function userManagerController($scope, $location, employeeRepository, $uibModal, statusMessageService) {
        $scope.employees = [];

        var init = function() {
            employeeRepository.get().success(function(data) {
                $scope.employees = data;
            });
        }

        $scope.add = function () {
            $location.url('/admin/addUser');
        }
        $scope.edit = function(employee) {
            $location.url('/admin/editUser/' + employee.employeeId);
        }
        $scope.delete = function(employee) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/templates/admin/employeeManager/deleteEmployeeModal.html',
                controller: 'DeleteUserModalController',
                resolve: {
                    employee: function() {
                        return employee;
                    }
                }
            });
            modalInstance.result.then(function (employee) {
                var index = this.employees.indexOf(employee);
                if (index > -1) {
                    this.employees.splice(index, 1);
                }
            }.bind(this));
        }

        init();
    }
})();