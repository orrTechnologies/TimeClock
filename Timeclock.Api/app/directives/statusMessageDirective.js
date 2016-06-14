timeClock.directive('statusMessage', function() {
    return {
        restrict: 'E',
        templateUrl: "/templates/statusMessage/statusMessage.html",
        replace: true,
        scope: {
            statusMessage: "=message",
            remove: "&"
        },
        link: function() {
        }
    }
});