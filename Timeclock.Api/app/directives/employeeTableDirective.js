timeClock.directive('employeeTable', function (NgTableParams) {
    return {
        restrict: 'E',
        templateUrl: "/templates/employees.html",
        replace: true,

        controller: function ($scope) {

            $scope.colList = [
                { field: "fullName", title: "Full Name", show: true }
            ];
            $scope.cols = _.indexBy($scope.colList, "field");

            $scope.debug = function(object) {
                console.log(object);
            }


        },
        scope: {
            showStatus: "=",
            employees: '='
        },
        link: function (scope, element, attrs) {

            //scope.$watch('employees', function (employees) {
            //    console.log(employees);
            //    scope.tableParams = new NgTableParams({}, { dataset: employees });
            //});

            scope.tableParams = new NgTableParams({}, {
                dataset: [
                    { fullName: 'dylan' },
                    { fullName: 'dylan2' },
                    { fullName: 'dylan3' },
                    { fullName: 'dylan4' },
                    { fullName: 'dylan5' },
                ]
            });
        }
    }
});