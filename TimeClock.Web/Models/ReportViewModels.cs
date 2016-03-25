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
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }

    public class TimeReport
    {
        public Employee Employee { get; set; }
        public IEnumerable<TimePunch> TimePunches { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ReportIndex
    {
        public IEnumerable<int> EmployeeIds { get; set; }
        public IEnumerable<SelectListItem> EmployeSelectListItems { get; set; }
    }
}