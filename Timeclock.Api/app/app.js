var timeClock = angular.module('timeClock', ['ngRoute', 'ngResource'])
        .config(function ($routeProvider, $locationProvider) {
            $routeProvider.when('/admin', )
        $routeProvider.otherwise({ templateUrl: '/templates/employees.html', controller: 'EmployeeListController' });
    });