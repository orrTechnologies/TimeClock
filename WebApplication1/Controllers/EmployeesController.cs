using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TimeClock.Data.Models;
using TimeClock.Web.Models;
using TimeClock.Web.Services;

namespace TimeClock.Web.Controllers
{
 
    //TODO: Add role based security here, to make sure only admins can edit employees;
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITimeService _timeService;
        //ApplicationDbContext db = new ApplicationDbContext();
        public EmployeesController(IEmployeeService employeeService, ITimeService timeService)
        {
            _employeeService = employeeService;
            _timeService = timeService;
        }

        // GET: Employees
        public ActionResult Index()
        {
            return  View();
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            return PartialView(_employeeService.GetEmployeeList().Select(e => new EmployeeViewModel()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeId = e.EmployeeId,
                LastPunchTime = e.LastPunchTime,
                CurrentStatus = e.CurrentStatus
            }));
        }

         //GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.FindById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            IEnumerable<TimePunch> TimePunches = _timeService.GetPunchList(employee.EmployeeId, new TimeClockSpan( DateTime.Now.AddDays(-7),
                DateTime.Now));
            View().ViewBag.TimePunches = TimePunches;
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName")] EmployeeCreateViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    LastPunchTime = DateTime.Now
                };
                _employeeService.CreateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employeeViewModel);
        }

        //// GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.FindById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //// POST: Employees/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Clock(int? id, TimePunchStatus status)
        {
            if (id != null)
            {
                Employee employee = _employeeService.FindById(id);
                _employeeService.ChangeClockStatus(employee, status);

                _timeService.AddTimePunch(employee, new TimePunch((int)id, status, DateTime.Now));
            }
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
        }

        //// GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
}
