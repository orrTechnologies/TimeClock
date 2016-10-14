using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Timeclock.Api.Models;
using Timeclock.Services;
using TimeClock.Data.Models;
using TimeClock.Web.Models;

namespace Timeclock.Api.Controllers
{
    [RoutePrefix("api/Reports")]
    public class ReportController : ApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET api/report/5/{employeeIds
        [Route("")]
        [HttpGet]
        public string Get(string ids, string startTime, string endTime)
        {
            return "true";

        }
        [Route("Load/")]
        [HttpPost]
        public List<ITimeReport> Load(ReportRequest reportRequest)
        {
            //Make sure startDate is at the very beginning of the day
            //Make sure endTime is at the VERY end of day.
            reportRequest.StartTime = reportRequest.StartTime.Date;
            reportRequest.EndTime = reportRequest.EndTime.Date.AddDays(1).AddTicks(-1);

            List<ITimeReport> timeReports = reportRequest.EmployeeIds
                .Select(employeeId => _reportService.GenerateTimeWorkReport(employeeId, new TimeClockSpan(reportRequest.StartTime, reportRequest.EndTime)))
                .ToList();

            return timeReports;
        }
    }
}
