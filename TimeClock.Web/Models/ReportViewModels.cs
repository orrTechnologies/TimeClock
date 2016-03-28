using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeClock.Data.Models;

namespace TimeClock.Web.Models
{
    public class ReportRequest
    {
        public IEnumerable<SelectListItem> EmployeSelectListItems { get; set; }
        [Required]
        public IEnumerable<int> EmployeeIds { get; set; }
        [Required]
        public DateTime StartTime {
            get { return DateTime.Now; } }
        [Required]
        public DateTime EndTime { get; set; }
    }

    public class Report
    {
        public IEnumerable<TimeReport> TimeReports { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class TimeReport
    {
        public Employee Employee { get; set; }
        public IEnumerable<TimeReportDaily> DailyReports { get; set; }

        public double TimeWorked { get; set; }
    }

    public class TimeReportDaily
    {
        public DateTime Date { get; set; }
        public IEnumerable<TimePunch> TimePunch { get; set; }
    }

    public class ReportIndex
    {
        public IEnumerable<int> EmployeeIds { get; set; }
        public IEnumerable<SelectListItem> EmployeSelectListItems { get; set; }
    }
}