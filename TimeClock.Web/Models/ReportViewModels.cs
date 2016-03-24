using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeClock.Web.Models
{
    public class ReportRequest
    {
        public MultiSelectList Employees { get; set; }
        public IEnumerable<int> EmployeeIds { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ReportIndex
    {
        public MultiSelectList Employees { get; set; }
        public IEnumerable<SelectListItem> EmployeeIds { get; set; }

    }
}