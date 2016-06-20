timeClock.directive('employeeTable', function (NgTableParams) {
    return {
        restrict: 'E',
        templateUrl: "/templates/employees.html",
        replace: true,
        controller: function($scope) {
            $scope.colsList = [
                { field: "fullName", title: "Full Name", show: true },
                { field: 'currentStatus', title: 'Status', show: true }
            ];
            $scope.cols = _.indexBy($scope.colsList, "field");

            $scope.debug = function(object) {
                console.log(object);
            }
        },
        scope: {
            showStatus: "=",
            employees: '='
        },
        link: function (scope, element, attrs) {

            scope.$watch('employees', function (employees) {
                scope.tableParams = new NgTableParams({}, { dataset: employees });
            });

            //scope.tableParams = new NgTableParams({}, { dataset: [{name: 'last'} ] });
        }
    }
});