angular.module('timeClock.timePicker').directive('timePicker', function () {
    return {
        restrict: 'E',
        templateUrl: "app/components/timePicker/timePicker.html",
        replace: true,
        controller: 'TimePickerController',

        scope: {
            datePicked: '=date'
        },
        link: function () {
        }
    }
});