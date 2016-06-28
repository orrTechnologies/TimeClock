angular.module('timeClock.timePicker', []).config(function($routeProvider) {
    $routeProvider.when('/timepickerdev', {
        templateUrl: '/app/components/timePicker/timePickerDev.html',
    });
});