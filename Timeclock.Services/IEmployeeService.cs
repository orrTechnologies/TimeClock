using System.Collections.Generic;
using Timeclock.Services;
using TimeClock.Data.Models;

namespace TimeClock.Web.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeeList();
        List<Employee> FindByStatus(TimePunchStatus status); 
        void CreateEmployee(Employee employee);
        bool ChangeClockStatus(TimePunchRequest timePunch);

        Employee FindById(int? id);
        void UpdateEmployee(Employee employee);


        void DeleteEmployee(Employee employee);
    }
}