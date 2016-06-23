(function () {
    timeClock.controller("EditUserController", editUserController);

    function editUserController($scope, $location, $routeParams, employeeRepository, statusMessageService) {
        $scope.loaded = false;
        $scope.employee = {
            id: 0,
            firstName: "",
            lastName: ""
        };

        $scope.save = save;

        var init = function() {
            employeeRepository.load($routeParams.id).success(function(response) {
                $scope.employee = response;
                $scope.loaded = true;
            });
        }

       function save() {
            console.log(this.employee);
            employeeRepository.save(this.employee)
                .success(function () {
                    console.log("Employee successfully saved.");
                    statusMessageService.add("Successfully added employee", 1);
                    $location.path('admin/userManager');
                })
                .error(function () {
                    statusMessageService.add("Failed to add employee", 4);
                });
        }
        init();
    }
})();