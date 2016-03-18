using System;
using TimeClock.Data;
using TimeClock.Data.Models;
using WebApplication1.Tests;

namespace TimeClock.Web.Services
{
    public class TimeService : ITimeService
    {
        private TimeClockContext _context;

        public TimeService(TimeClockContext context)
        {
            _context = context;
        }
        public void AddTimePunch(Employee employee, TimePunch timePunch)
        {
            //TODO: Finish method
           // _context.TimePunches.Add()
        }
    }
}
