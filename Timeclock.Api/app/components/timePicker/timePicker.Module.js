angular.module('timeClock.timePicker', ['angularMoment']).config(function($routeProvider) {
    $routeProvider.when('/timepickerdev', {
        templateUrl: '/app/components/timePicker/timePickerDev.html',
        controller: 'TimePickerDevController'
    });
});