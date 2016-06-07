using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Timeclock.Api.Models;
using Timeclock.Services;
using TimeClock.Data.Models;
using TimeClock.Web.Services;

namespace Timeclock.Api.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITimeService _timeService;

        public EmployeeController(IEmployeeService employeeService, ITimeService timeService)
        {
            _employeeService = employeeService;
            _timeService = timeService;
        }

        // GET api/employee
        public IEnumerable<EmployeeViewModel> Get()
        {
            return _employeeService.GetEmployeeList().Select(e => new EmployeeViewModel()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeId = e.EmployeeId,
                LastPunchTime = e.LastPunchTime,
                CurrentStatus = e.CurrentStatus
            });
        }

        // GET api/employee/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/employee
        public void Post([FromBody]string value)
        {
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
        }
        public HttpResponseMessage Clock(int? id, TimePunchStatus status)
        {
            if (id != null)
            {
                Employee employee = _employeeService.FindById(id);
                _employeeService.ChangeClockStatus(employee, status);

                _timeService.AddTimePunch(employee, new TimePunch((int)id, status, DateTime.Now));
            }
            return Request.CreateResponse(HttpStatusCode.Found, true);
        }
    }
}
