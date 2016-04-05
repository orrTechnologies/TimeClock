using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
    public class Report
    {
        public IEnumerable<TimeReportViewModel> TimeReports { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndTime { get; set; }
    }

    public class TimeReportViewModel
    {
        public string EmployeeName { get; set; }
        public IEnumerable<TimeReportDaily> DailyReports { get; set; }
        public double TimeWorked { get; set; }
    }
    public class TimeReportDaily
    {
        public DateTime Date { get; set; }
        public IEnumerable<TimePunch> TimePunches { get; set; }
        //TODO: Place holder, we need to calculate time worked for display+
        public double TimeWorked { get; set; }
        }

    public class ReportIndex
    {
        public IEnumerable<int> EmployeeIds { get; set; }
        public IEnumerable<SelectListItem> EmployeSelectListItems { get; set; }
    }
}
