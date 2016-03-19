using System.Collections.Generic;
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
}