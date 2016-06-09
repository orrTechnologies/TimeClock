(function() {
    timeClock.factory('employeeRepository', function($resource, $http) {

        return {
            get: function() {
                return $resource('/api/employee').query();
            },

            changeClockStatus: function(employeeId, timePunchStatus) {
                this.employee = { id: employeeId, status: timePunchStatus }
                return $http.post('/api/employee/clock', this.employee);
            }
        }
    });
})();