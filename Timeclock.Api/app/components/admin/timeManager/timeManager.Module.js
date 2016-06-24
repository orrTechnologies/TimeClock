angular.module('timeClock.timeManager', []).config(function ($routeProvider) {
    $routeProvider.when('/admin/time', {
        templateUrl: '/app/components/admin/timeManager/timeManager.html',
        controller: 'TimeManagerController'
    });
});