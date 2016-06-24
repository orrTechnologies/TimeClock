(function () {
    angular.module('timeClock.timeManager').factory('timePunchRepository', function ($resource, $http) {

        return {
            load: function (employeeId, startDate, endDate) {
                return $http.post('/api/time/load/', { id:employeeId,  startTime: startDate, endTime: endDate });
            }

        }
    });
})();