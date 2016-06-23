var timeClock = angular.module('timeClock', ['ngRoute', 'ngResource', 'ui.bootstrap', "ngTable",
                                'timeClock.statusMessage', 'timeClock.employee', 'timeClock.admin'])
        .config(function ($routeProvider) {
        $routeProvider.otherwise({ templateUrl: 'app/components/employees/employees.html', controller: 'EmployeeListController' });
    });