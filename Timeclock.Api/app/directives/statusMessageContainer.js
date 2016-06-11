timeClock.directive('statusMessageContainer', function() {
    return {
        restrict: 'E',
        templateUrl: "/templates/statusMessage/statusMessageContainer.html",
        replace: true,
        controller: 'StatusMessageController'
    }
});