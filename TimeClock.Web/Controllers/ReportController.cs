using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Proxies;
using System.Web;
using System.Web.Mvc;
using TimeClock.Data.Models;
using TimeClock.Web.Models;
using TimeClock.Web.Services;

namespace TimeClock.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITimeService _timeService;

        public ReportController(IEmployeeService employeeService, ITimeService timeService)
        {
            _employeeService = employeeService;
            _timeService = timeService;
        }

        //
        // GET: /Report/
        public ActionResult Index()
        {
            ReportIndex view = new ReportIndex();
            List<Employee> employees = _employeeService.GetEmployeeList();

            List<SelectListItem> employeSelectList = new List<SelectListItem>();
            employees.ForEach(e => employeSelectList.Add(new SelectListItem()
            {
                Text = e.FirstName,
                Value = Convert.ToString(e.EmployeeId)
            }));

            return View(new ReportIndex()
            {
                EmployeSelectListItems = employeSelectList
            });

        }

        public ActionResult TimeReport(ReportRequest reportRequest)
        {
                   //For each employee get a list of punchtimes in start and end time range.
            if (reportRequest.EmployeeIds == null)
            {
                return new HttpNotFoundResult("Must select at least one employee");
            }
            var timeReports = new List<TimeReport>();

            foreach (var employeeId in reportRequest.EmployeeIds)
            {
                Employee employee = _employeeService.FindById(employeeId);

                var timePuches = _timeService.GetPunchList(employee.EmployeeId,
                    new TimeClockSpan(reportRequest.StartTime, reportRequest.EndTime));

                var report = new TimeReport()
                {
                    Employee = employee,
                    StartTime = reportRequest.StartTime,
                    EndTime = reportRequest.EndTime,
                    TimePunches = timePuches
                };
                timeReports.Add(report);
            }

            return View(timeReports);
        }
	}
}