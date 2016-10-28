using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Timeclock.Api.Models;
using Timeclock.Core;
using Timeclock.Core.Domain;
using Timeclock.Services;
using TimeClock.Data.Models;

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
            return _employeeService.GetEmployeeList().Select(MapEmployeeToBindingModel);
        }
        [Route("Load/{id}")]
        [HttpGet]
        // GET api/employee/5
        public EmployeeBindingModel Get([FromUri] int id)
        {
            Employee employee = _employeeService.FindById(id);
            return MapEmployeeToBindingModel(employee);
        }


        //TODO: Use auto mapper
        private EmployeeBindingModel MapEmployeeToBindingModel(Employee employee)
        {
            return new EmployeeBindingModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeId = employee.EmployeeId,
                LastPunchTime = employee.LastPunchTime,
                CurrentStatus = employee.PunchStatus,
                RequiresAuthentication = employee.HasPin()
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

            var request = new TimePunchRequest(timePunchBindingModel.Id, timePunchBindingModel.Status);
            if (timePunchBindingModel.PIN != null)
            {
                request.PIN = timePunchBindingModel.PIN;
            }


            //if changing the employees status was successfull and saved to database. 
            if (_employeeService.ChangeClockStatus(request))
            {
                _timeService.AddTimePunch(employee,
                    new TimePunch(timePunchBindingModel.Status, DateTime.Now));
                return Ok(MapEmployeeToBindingModel(employee));
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
                //TODO: Use auto mapper here to auto copy properties. 
                employee.FirstName = editBindingModal.FirstName;
                employee.LastName = editBindingModal.LastName;

                _employeeService.UpdateEmployee(employee);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
