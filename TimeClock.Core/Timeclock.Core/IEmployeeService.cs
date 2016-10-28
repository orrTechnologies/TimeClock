using System.Collections.Generic;
using Timeclock.Core.Domain;
using Timeclock.Services;
using TimeClock.Data.Models;

namespace Timeclock.Core
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