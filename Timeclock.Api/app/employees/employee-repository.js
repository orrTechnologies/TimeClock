timeClock.factory('employeeRepository', function ($resource, $http) {
    return {
        get: function () {
             return $resource('/api/employee').query();
        },

        changeClockStatus: function (employeeId, timePunchStatus) {
            return $http.post('/api/employee/clock', { id: employeeId, timePunchStatus: timePunchStatus }).promise;
        }
    }
});