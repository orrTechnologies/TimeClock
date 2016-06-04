﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeClock.Data.Models;
using TimeClock.Web.Models;

namespace TimeClock.Web.Services
{
    public interface IReportService
    {
        ITimeReport GenerateTimeWorkReport(int employeeIds, TimeClockSpan span);
    }
    public class ReportService : IReportService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITimeService _timeService;

        public ReportService(IEmployeeService employeeService, ITimeService timeService)
        {
            _employeeService = employeeService;
            _timeService = timeService;
        }

        public ITimeReport GenerateTimeWorkReport(int employeeId, TimeClockSpan span)
        {
            Employee employee = _employeeService.FindById(employeeId);
            var timePuches = _timeService.GetPunchList(employee.EmployeeId, span);

            ITimeReport timeReport = new TimeReport(employee, timePuches);

            return timeReport;
        }
    }
}