(function() {
    timeClock.controller("UserManagerController", userManagerController);

    function userManagerController($scope, $location, employeeRepository, $uibModal, statusMessageService) {
        $scope.employees = [];

        $scope.add = add;
        $scope.edit = edit;
        $scope.delete = deleteEmployee;

        var init = function() {
            employeeRepository.get().success(function(data) {
                $scope.employees = data;
            });
        }

       function add() {
            $location.url('/admin/addUser');
        }
       function edit(employee) {
            $location.url('/admin/editUser/' + employee.employeeId);
        }
       function deleteEmployee(employee) {
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