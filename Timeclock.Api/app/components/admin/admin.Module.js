angular.module('timeClock.admin', []).config(function ($routeProvider) {
    $routeProvider.when('/admin', {
        templateUrl: '/app/components/admin/adminPanel.html',
        controller: 'AdminPanelController'
    });
    $routeProvider.when('/admin/userManager', {
        templateUrl: '/app/components/admin/employeeManager/employeeManager.html',
        controller: 'UserManagerController'
    });
    $routeProvider.when('/admin/addUser', {
        templateUrl: '/app/components/admin/employeeManager/addUser.html',
        controller: 'AddUserController'
    });
    $routeProvider.when('/admin/editUser/:id', {
        templateUrl: '/app/components/admin/employeeManager/addUser.html',
        controller: 'EditUserController'
    });
    $routeProvider.when('/admin/reports/', {
        templateUrl: '/app/components/admin/reports/timeWorkedReport.html',
        controller: 'TimeWorkedReportController'
    });
});