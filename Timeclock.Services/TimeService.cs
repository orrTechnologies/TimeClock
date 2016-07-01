using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace Timeclock.Services
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

        public List<TimePunch> GetPunchList(int employeeId, TimeClockSpan timeClockSpan)
        {
            var punchList = _context.TimePunches.Where(t => t.EmployeeId == employeeId
                && t.Time >= timeClockSpan._start && t.Time <= timeClockSpan._end).ToList();
            return punchList;
        }


        public TimePunch FindById(int id)
        {
            var punch = _context.TimePunches.Find(id);
            return punch;
        }
    }

    public interface ITimeService
    {
        void AddTimePunch(Employee employee, TimePunch punch);

        TimePunch FindById(int id);
       List<TimePunch> GetPunchList(int employeeId, TimeClockSpan timeClockSpan);
    }
}