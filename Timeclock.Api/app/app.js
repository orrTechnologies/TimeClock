var timeClock = angular.module('timeClock', ['ngRoute', 'ngResource'])
        .config(function($routeProvider, $locationProvider) {
        $routeProvider.otherwise({ templateUrl: '/templates/employees.html', controller: 'EmployeeController' });
    });