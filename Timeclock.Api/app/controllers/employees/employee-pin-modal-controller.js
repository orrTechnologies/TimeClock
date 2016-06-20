(function () {
    timeClock.controller("EmployeePinModalController", employeePinModalController);

    function employeePinModalController($scope, $uibModalInstance) {

        $scope.pin = "";
        $scope.hasError = false;

        $scope.confirmDelete = function () {
            if ($scope.pin == "") {
                $scope.hasError = true;
            } else {
                $uibModalInstance.close($scope.pin);
            }
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }
})();