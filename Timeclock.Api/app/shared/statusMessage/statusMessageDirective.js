angular.module('timeClock.statusMessage').directive('statusMessage', function () {
    return {
        restrict: 'E',
        templateUrl: "app/shared/statusMessage/statusMessage.html",
        replace: true,
        scope: {
            statusMessage: "=message",
            remove: "&"
        },
        link: function() {
        }
    }
});