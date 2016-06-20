(function() {
    timeClock.controller("EmployeeListController", employeeListController);

    function employeeListController(employeeRepository, NgTableParams) {
        var vm = this;
        this.employees = [];
        this.tableParams = new NgTableParams({}, {});

        //testing
        this.showStatus = false;

        var init = function() {
            employeeRepository.get().success(function(data) {
                vm.employees = data;
                vm.tableParams = new NgTableParams({}, { dataset: data });
            });

        }

        this.changeClockStatus = function (employee) {
            var self = this;
            employeeRepository.changeClockStatus(employee.employeeId, !employee.currentStatus)
                .success(function(data, status, headers, config) {
                    self.employee.currentStatus = !self.employee.currentStatus;
                    self.employee.lastPunchTime = data.lastPunchTime;
                });
        }

        this.formatTime = function (dateTime) {
            return moment(dateTime).format("hh:mm A - D/MM/YY");
        }

        init();
    }
})();