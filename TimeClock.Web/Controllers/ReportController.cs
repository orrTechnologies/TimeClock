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

            MultiSelectList list = new MultiSelectList(employees);

            return View(new ReportIndex()
            {
                Employees = list
            });

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