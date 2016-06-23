using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeClock.Data;

namespace Timeclock.Api.Models
{
    public class ReportRequest
    {
        [Required]
        public int[] EmployeeIds { get; set; }
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
