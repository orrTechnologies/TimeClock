(function () {
    timeClock.controller("AddUserController", addUserController);

    function addUserController($scope, $location, employeeRepository) {
        $scope.hasError = false;
        $scope.employee = {
            firstName: "",
            lastName: ""
        };

        $scope.save = function() {
            console.log(this.employee);
            employeeRepository.saveNewEmployee(this.employee)
                .success(function () {
                    this.hasError = false;
                    console.log("Employee successfully saved.");
                    $location.path('admin');
                })
                .error(function () {
                    this.hasError = true;
            });
        }
    }
})();