﻿(function() {
    angular.module('timeClock.employee').factory('employeeRepository', function ($resource, $http) {

        return {
            get: function() {
                return $http.get('/api/employee/');
            },

            changeClockStatus: function(employeeId, timePunchStatus, pin) {
                this.employee = { id: employeeId, status: timePunchStatus, pin: pin }
                return $http.post('/api/employee/clock', this.employee);
            },

            saveNewEmployee: function(employee) {
                return $http.post('/api/employee/add', employee);
            },

            load: function(employeeId) {
                return $http.get('/api/employee/load/' + employeeId);
            },

            save: function(employee) {
                return $http.post('/api/employee/edit', employee);
            },

            deleteEmployee: function(employee) {
                return $http.delete('/api/employee/Delete/' + employee.employeeId);
            }
        }
    });
})();