using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace TimeClock.Web.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeeList();
        void CreateEmployee(Employee employee);
        bool ChangeClockStatus(Employee employee, TimePunchStatus status);

        Employee FindById(int? id);
        void UpdateEmployee(Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly TimeClockContext _context;
        private readonly ITimeService _timeService;

        public EmployeeService(TimeClockContext context, ITimeService timeService)
        {
            _context = context;
            _timeService = timeService;
        }

        public void CreateEmployee(Employee employee)
        {
            //TODO: Validate Employee
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public bool ChangeClockStatus(Employee employee, TimePunchStatus status)
        {
            //Can not change status to current status. 
            if (employee.CurrentStatus == status) return false;

            //Clock the employee out, and save. 
            employee.CurrentStatus = status;
            UpdateEmployee(employee);

            _timeService.AddTimePunch(employee, new TimePunch(status, DateTime.Now));
            return true;
        }

        public List<Employee> GetEmployeeList()
        {
            return _context.Employees.ToList();
        }


        public Employee FindById(int? id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void AddPunchTime(Employee employee, TimePunch time)
        {
            
        }
    }
}