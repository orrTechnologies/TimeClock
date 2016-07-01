(function () {
    angular.module('timeClock.timeManager').controller("TimeEditorController", timeEditorController);

    function timeEditorController($scope, $location, $routeParams, timePunchRepository) {


        function init() {
            timePunchRepository.load($routeParams.id).success(function(data) {
                $scope.date = new Date(data.time);
            });
        }

        init();
    }
})();