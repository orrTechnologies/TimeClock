using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Timeclock.Services;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace TimeClock.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TimeClockContext _context;

        public EmployeeService(TimeClockContext context)
        {
            _context = context;
        }

        public List<Employee> FindByStatus(TimePunchStatus status)
        {
            return _context.Employees.Where(e => e.CurrentStatus == status).ToList();
        }

        public void CreateEmployee(Employee employee)
        {
            //TODO: Validate Employee
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public bool ChangeClockStatus(TimePunchRequest request)
        {
            Employee employee = FindById(request.EmployeeId);
            if (employee == null) { return false; }

            //Can not change status to current status. 
            if (employee.CurrentStatus == request.Status) return false;

            //Check PIN Number If pin exist and pin does not match employee pin

            if (employee.HasPin())
            {
                //No Pin sent in request or the pin that was sent does not match.
                if (request.PIN == null || !employee.CheckPIN((int) request.PIN))
                {
                    return false;
                }
            }
            //Clock the employee out, and save. 
            employee.CurrentStatus = request.Status;
            UpdateEmployee(employee);

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

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}