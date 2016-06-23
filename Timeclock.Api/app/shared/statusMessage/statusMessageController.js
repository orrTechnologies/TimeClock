(function() {
    angular.module('timeClock.statusMessage').controller("StatusMessageController", statusMessageController);

    function statusMessageController($scope, statusMessageService) {

        $scope.messages = statusMessageService.get(false);

        $scope.remove = function(message) {
            statusMessageService.remove(message);
        }

        $scope.$watch(function() { return statusMessageService.get(false); }, function(newVal) {
            $scope.messages = newVal;
        }, true);
    }
})();