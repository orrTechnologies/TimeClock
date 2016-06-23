(function () {
    timeClock.controller("DeleteUserModalController", deleteUserModalController);

    function deleteUserModalController($scope, $uibModalInstance, employee, employeeRepository, statusMessageService) {

        $scope.employee = employee;

        $scope.confirmDelete = function () {
            employeeRepository.deleteEmployee(this.employee)
                .success(function () {
                    this.hasError = false;
                    console.log("Employee delete saved.");
                    statusMessageService.add("Successfully added employee", 1);
                    $uibModalInstance.close(this.employee);

                }.bind(this))
                .error(function () {
                    statusMessageService.add("Failed to delete employee", 4);
                });
        }

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        }
    }
})();