using System.Collections.Generic;
using TimeClock.Data.Models;

namespace Timeclock.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeeList();
        List<Employee> FindByStatus(TimePunchStatus status);
        void CreateEmployee(Employee employee);
        bool ChangeClockStatus(TimePunchRequest request);
        Employee FindById(int? id);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}