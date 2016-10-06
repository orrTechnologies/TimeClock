using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Proxies;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Timeclock.Services;
using TimeClock.Data;
using TimeClock.Data.Models;
using TimeClock.Web.Models;
using TimeClock.Web.Services;

namespace TimeClock.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IEmployeeService _employeeService;

        public ReportController(IReportService reportService, IEmployeeService employeeService)
        {
            _reportService = reportService;
            _employeeService = employeeService;
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
                Text = e.FullName,
                Value = Convert.ToString(e.EmployeeId)
            }));

            return View(new ReportIndex()
            {
                EmployeSelectListItems = employeSelectList
            });

        }

        public ActionResult Report(ReportRequest reportRequest)
        {
            //For each employee get a list of punchtimes in start and end time range.
            if (reportRequest.EmployeeIds == null)
            {
                return new HttpNotFoundResult("Must select at least one employee");
            }
            List<ITimeReport> timeReports = reportRequest.EmployeeIds
                .Select(employeeId => _reportService.GenerateTimeWorkReport(employeeId, new TimeClockSpan(reportRequest.StartTime, reportRequest.EndTime)))
                .ToList();

            Report report = new Report()
            {
                StartTime = reportRequest.StartTime,
                EndTime = reportRequest.EndTime,
                TimeReports = timeReports.Select(r => new TimeReportViewModel()
                {
                    DailyReports = r.DailyReports(),
                    EmployeeName = r.Employee.FirstName + " " + r.Employee.LastName,
                    TimeWorked = r.TimeWorked
                })
            };
            return View(report);
        }

        private IEnumerable<TimeReportDaily> MakeDailyReports(IEnumerable<TimePunchRequest> timePunches)
        {
            var timePunchByDay = timePunches.GroupBy(t => t.Time.Date);
            var dailyReports = timePunchByDay.Select(t => new TimeReportDaily()
            {
                Date = t.Key,
                TimePunches = t.ToList()
            });

            return dailyReports;
        }
    }
}