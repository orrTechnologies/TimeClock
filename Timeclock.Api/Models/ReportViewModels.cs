using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace TimeClock.Web.Models
{
    public class ReportRequest
    {
        [Display(Name = "Select Employees")]
        public IEnumerable<SelectListItem> EmployeSelectListItems { get; set; }
        [Required]
        public IEnumerable<int> EmployeeIds { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Required]
        [Display(Name = "End Time")]
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

}
