using System;
using System.Collections.Generic;
using System.Linq;
using TimeClock.Data.Models;

namespace TimeClock.Web.Models
{
    public interface ITimeReport
    {
        Employee Employee { get; }
        IEnumerable<TimeReportDaily> DailyReports();
        double TimeWorked();
    }

    public class TimeReport : ITimeReport
    {

        public  Employee Employee { get; private set; }
        private readonly IEnumerable<TimePunch> _timePunches;

        public TimeReport(Employee employee, IEnumerable<TimePunch> timePunches)
        {
            Employee = employee;
            _timePunches = timePunches;
        }

        public IEnumerable<TimeReportDaily> DailyReports()
        {
            var timePunchByDay = _timePunches.GroupBy(t => t.Time.Date);
            var dailyReports = timePunchByDay.Select(t => new TimeReportDaily()
            {
                Date = t.Key,
                TimePunches = t.ToList()
            });

            return dailyReports;
        }

        public double TimeWorked()
        {
            double totalTime = 0;
            var punchCount = _timePunches.Count();
            for (int i = 1; i < punchCount; i += 2)
            {
                var timeOne = _timePunches.ElementAt(i).Time;
                DateTime timeTwo;
                //We are not the last element, use the next clockout time as to calculate time worked.
                if (i != punchCount)
                {
                    timeTwo = _timePunches.ElementAt(i + 1).Time;
                }
                else
                {
                    timeTwo = DateTime.Now;
                }
                TimeSpan span = (timeTwo - timeOne);
                totalTime += span.Hours;
            }
            return totalTime;
        }
    }

  
}