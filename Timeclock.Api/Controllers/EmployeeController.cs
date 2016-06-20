using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Timeclock.Api.Models;
using Timeclock.Services;
using TimeClock.Data.Models;
using TimeClock.Web.Services;

namespace Timeclock.Api.Controllers
{
    [RoutePrefix("api/Employee")]
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
        [Route("")]
        [HttpGet]
        public IEnumerable<EmployeeBindingModel> GetEmployees()
        {
            return _employeeService.GetEmployeeList().Select(e => new EmployeeBindingModel()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeId = e.EmployeeId,
                LastPunchTime = e.LastPunchTime,
                CurrentStatus = e.CurrentStatus
            });
        }
        [Route("Load/{id}")]
        [HttpGet]
        // GET api/employee/5
        public EmployeeBindingModel Get([FromUri] int id)
        {
            Employee employee = _employeeService.FindById(id);
            return new EmployeeBindingModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeId = employee.EmployeeId,
                LastPunchTime = employee.LastPunchTime,
                CurrentStatus = employee.CurrentStatus
            };
        }

        //// POST api/employee
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/employee/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/employee/5       
        [AllowAnonymous]
        [Route("Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            Employee employee = _employeeService.FindById(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeService.DeleteEmployee(employee);

            return Ok();
        }

        [AllowAnonymous]
        [Route("Clock/")]
        [HttpPost]
        public IHttpActionResult Clock(TimePunchBindingModel timePunchBindingModel)
        {
            Employee employee = _employeeService.FindById(timePunchBindingModel.Id);
            if (employee == null)
            {
                return NotFound();
            }

            //if changing the employees status was successfull and saved to database. 
            if (_employeeService.ChangeClockStatus(employee, timePunchBindingModel.Status))
            {
                _timeService.AddTimePunch(employee,
                    new TimePunch(timePunchBindingModel.Id, timePunchBindingModel.Status, DateTime.Now));
                return Ok(employee);
            }
            return BadRequest();
        }

        public IHttpActionResult Add(EmployeeAddBindingMdoel addBm)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    FirstName = addBm.FirstName,
                    LastName = addBm.LastName,
                    LastPunchTime = DateTime.Now
                };
                _employeeService.CreateEmployee(employee);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [Route("Edit/")]
        [HttpPost]
        public IHttpActionResult Edit(EmployeeEditBindingModal editBindingModal)
        {
            Employee employee = _employeeService.FindById(editBindingModal.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                employee.FirstName = editBindingModal.FirstName;
                employee.LastName = editBindingModal.LastName;

                _employeeService.UpdateEmployee(employee);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
