(function () {
    angular.module('timeClock.employee').controller("EmployeePinModalController", employeePinModalController);

    function employeePinModalController($scope, $uibModalInstance) {

        $scope.pin = "";
        $scope.hasError = false;

        $scope.confirmDelete = confirmDelete;
        $scope.cancel = cancel;

        function confirmDelete() {
            if ($scope.pin == "") {
                $scope.hasError = true;
            } else {
                $uibModalInstance.close($scope.pin);
            }
        }

        function cancel() {
            $uibModalInstance.dismiss('cancel');
        }
    }
})();