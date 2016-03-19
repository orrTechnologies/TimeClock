using System;
using System.Collections.Generic;
using System.Linq;
using TimeClock.Data;
using TimeClock.Data.Models;
using WebApplication1.Tests;

namespace TimeClock.Web.Services
{
    public class TimeService : ITimeService
    {
        private readonly TimeClockContext _context;

        public TimeService(TimeClockContext context)
        {
            _context = context;
        }

        public void AddTimePunch(Employee employee, TimePunch punch)
        {
            _context.TimePunches.Add(punch);
            _context.SaveChanges();
        }

        public IEnumerable<TimePunch> GetPunchList(int employeeId, TimeClockSpan timeClockSpan)
        {
            var punchList = _context.TimePunches.Where(t => t.EmployeeId == employeeId
                && t.Time >= timeClockSpan._start && t.Time <= timeClockSpan._end).ToList();
            return punchList;
        }
    }

    public interface ITimeService
    {
        void AddTimePunch(Employee employee, TimePunch punch);
        IEnumerable<TimePunch> GetPunchList(int employeeId, TimeClockSpan timeClockSpan);
    }
}