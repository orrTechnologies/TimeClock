(function () {
    timeClock.factory('reportsRepository', function ($resource, $http) {

        return {

            load: function (reportOptions) {
                return $http.post('/api/reports/load/', reportOptions);
            }
        }
    });
})();