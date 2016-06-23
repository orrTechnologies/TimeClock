(function () {
    timeClock.controller("AddUserController", addUserController);

    function addUserController($scope, $location, employeeRepository, statusMessageService) {
        $scope.hasError = false;
        $scope.employee = {
            firstName: "",
            lastName: ""
        };

        $scope.save = save;

        function save() {
            console.log(this.employee);
            employeeRepository.saveNewEmployee(this.employee)
                .success(function () {
                    this.hasError = false;
                    console.log("Employee successfully saved.");
                    statusMessageService.add("Successfully added employee", 1);
                    $location.path('admin/userManager');
                })
                .error(function () {
                statusMessageService.add("Failed to add employee", 4);
            });
        }
    }
})();