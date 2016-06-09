var timeClock = angular.module('timeClock', ['ngRoute', 'ngResource'])
        .config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/admin', {
        templateUrl: '/templates/adminPanel.html',
        controller: 'AdminPanelController'
    });
        $routeProvider.otherwise({ templateUrl: '/templates/employees.html', controller: 'EmployeeListController' });
    });