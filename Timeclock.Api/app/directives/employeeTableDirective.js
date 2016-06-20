timeClock.directive('employeeTable', function (NgTableParams) {
    return {
        restrict: 'E',
        templateUrl: "/templates/employees.html",
        replace: true,
        controller: function($scope) {
            $scope.cols = [
                { field: "fullName", title: "Full Name", show: true },
                { field: 'statusHtml', title: 'Status', show: true }
            ];
        },
        scope: {
            showStatus: "=",
            employees: '=',
        },
        link: function (scope, element, attrs) {

            scope.$watch('employees', function (employees) {

                employees.forEach(function(employee) {
                    employee.statusHtml = "<div ng-show=\"employee.currentStatus == 0\" class=\"punch-status  punched-in\"><\/div><div ng-hide=\"employee.currentStatus == 0\" class=\"punch-status  punched-out\"><\/div>"
                });

                scope.tableParams = new NgTableParams({}, { dataset: employees });
                console.log(employees);
            });

            //scope.tableParams = new NgTableParams({}, { dataset: [{name: 'last'} ] });
        }
    }
});