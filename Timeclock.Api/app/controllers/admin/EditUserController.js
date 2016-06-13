(function () {
    timeClock.controller("EditUserController", editUserController);

    function editUserController($scope, $location, $routeParams, employeeRepository, statusMessageService) {
        $scope.loaded = false;

        $scope.employee = {
            id: 0,
            firstName: "",
            lastName: ""
        };

        var init = function() {
            employeeRepository.load($routeParams.id).success(function(response) {
                $scope.employee = response;
                $scope.loaded = true;
            });
        }

        $scope.save = function () {
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