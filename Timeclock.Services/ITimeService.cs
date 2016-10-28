using System.Collections.Generic;
using Timeclock.Core.Domain;
using TimeClock.Data.Models;

namespace Timeclock.Services
{
    public interface ITimeService
    {
        void AddTimePunch(Employee employee, TimePunch punch);
        List<TimePunch> GetPunchList(int employeeId, TimeClockSpan timeClockSpan);
    }
}