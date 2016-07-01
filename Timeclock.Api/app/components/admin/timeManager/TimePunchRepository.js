(function () {
    angular.module('timeClock.timeManager').factory('timePunchRepository', function ($resource, $http) {

        return {
            load: function (timePunchId) {
                return $http.get('/api/time/load/' + timePunchId );
            },

            loadByDate: function (employeeId, startDate, endDate) {
                return $http.post('/api/time/loadByDate/', { id:employeeId,  startTime: startDate, endTime: endDate });
            }



        }
    });
})();