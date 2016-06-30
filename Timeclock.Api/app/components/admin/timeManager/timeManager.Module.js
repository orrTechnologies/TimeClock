angular.module('timeClock.timeManager', []).config(function ($routeProvider) {
    $routeProvider.when('/admin/time', {
        templateUrl: '/app/components/admin/timeManager/timeManager.html',
        controller: 'TimeManagerController'
    });
    $routeProvider.when('/admin/time/edit/', {
        templateUrl: '/app/components/admin/timeManager/timeEditor.html',
        controller: 'TimeEditorController'
    });
    $routeProvider.when('/admin/time/edit/:timeId', {
        templateUrl: '/app/components/admin/timeManager/timeEditor.html',
        controller: 'TimeEditorController'
    });
});