(function () {
angular.module('timeClock.timeManager').controller("TimeManagerController", timeManagerController);

    function timeManagerController($scope, $location, employeeRepository, timePunchRepository) {
        $scope.selectedEmployee = null;

        $scope.employeeList = [];
        $scope.startTime = '';
        $scope.endTime = '';
        $scope.endDateOptions = {};
        $scope.startDateOptions = {};
        $scope.timeCard = [];
        $scope.formats = ['MMMM-dd', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        
        /**** Public Methods *******/
        $scope.openStart = openStart;
        $scope.openEnd = openEnd;
        $scope.startTimeChanged = startTimeChanged;
        $scope.endTimeChanged = endTimeChanged;
        $scope.checkForm = checkForm;

        $scope.startInfo = {
            time: '',
            opened: false
        };
        $scope.endInfo = {
            time: '',
            opened: false
        };

        var init = function () {
           employeeRepository.get().success(function (employees) {
               employees.forEach(function (employee) {
                    $scope.employeeList.push(employee);
                });
            });
        }

        $scope.$watch('timeSelectionForm.$valid', formValidity);

        function formValidity() {
            checkForm();
        }
        function selectedEmployeeChanged() {
            checkForm();
        }
        function checkForm() {
            if ($scope.timeSelectionForm.$valid && $scope.timeSelectionForm.$dirty) {
                timePunchRepository.load($scope.selectedEmployee.employeeId, $scope.startTime, $scope.endTime)
                    .success(function(data) {
                        $scope.timeCard = data;
                    });
            }
        }

        function openStart() {
            $scope.startInfo.opened = true;
        };
        function openEnd() {
            $scope.endInfo.opened = true;
        };
        function startTimeChanged() {
            setEndDateOptions();
        }
        function endTimeChanged() {
            setStartDateOptions();
        }

        function setEndDateOptions() {
            $scope.endDateOptions = { minDate: $scope.startTime };
        }
        function setStartDateOptions() {
            $scope.startDateOptions = { maxDate: $scope.endTime };
        }


        
        init();
    }
})();