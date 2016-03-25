using System;
using System.Collections.Generic;
using System.Linq;
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

        public ReportController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //
        // GET: /Report/
        public ActionResult Index()
        {
            ReportIndex view = new ReportIndex();
            List<Employee> employees = _employeeService.GetEmployeeList();

            List<SelectListItem> list = employees.Select(e => new SelectListItem()
            {
                Text = e.FirstName,
                Value = Convert.ToString(e.EmployeeId)
            }).ToList();

            view.Employees = list;

            return View(view);
        }
        [HttpPost]
        public ActionResult Index(ReportRequest reportRequest)
        {
            return null;
        }

        public ActionResult TimeReport(ReportRequest reportRequest)
        {
            return null;
        }
	}
}