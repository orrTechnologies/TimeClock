var timeClock = angular.module('timeClock', ['ngRoute', 'ngResource', 'ui.bootstrap', "ngTable"])
        .config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/admin', {
        templateUrl: '/templates/admin/adminPanel.html',
        controller: 'AdminPanelController'
    });
    $routeProvider.when('/admin/userManager', {
        templateUrl: '/templates/admin/employeeManager/employeeManager.html',
        controller: 'UserManagerController'
    });
    $routeProvider.when('/admin/addUser', {
        templateUrl: '/templates/admin/employeeManager/addUser.html',
        controller: 'AddUserController'
    });
    $routeProvider.when('/admin/editUser/:id', {
        templateUrl: '/templates/admin/employeeManager/addUser.html',
        controller: 'EditUserController'
    });
    $routeProvider.when('/admin/reports/', {
        templateUrl: '/templates/admin/reports/timeWorkedReport.html',
        controller: 'TimeWorkedReportController'
    });
        $routeProvider.otherwise({ templateUrl: '/templates/employees.html', controller: 'EmployeeListController' });
    });