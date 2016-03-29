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
        double TimeWorked { get; }
    }

    public class TimeReport : ITimeReport
    {

        public  Employee Employee { get; private set; }
        private  IEnumerable<TimePunch> _timePunches;
        private double _totalTimeWorked = -1;
        //Cache the result. 
        public double TimeWorked
        {
            get
            {
                if (_totalTimeWorked == -1)
                {
                    _totalTimeWorked = CalculateTotalHoursWorked();
                }
                return _totalTimeWorked;;
            }
        }

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

        private double CalculateTotalHoursWorked()
        {
            double totalTime = 0;
            //skip first time if it is punched out. No punch in time to calulcate time worked with. 
            if (_timePunches.First().Status == TimePunchStatus.PunchedOut)
            {
                _timePunches = _timePunches.Skip(1);
            }
            var punchCount = _timePunches.Count();
            for (int i = 0; i < punchCount; i += 2)
            {
                var timeOne = _timePunches.ElementAt(i).Time;
                DateTime timeTwo;
                //We are not the last element, use the next clockout time as to calculate time worked.
                if (i != punchCount - 1)
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

        private double t()
        {
            double totalTime = 0;
            var punchCount = _timePunches.Count();
            for (int i = 0; i < punchCount; i += 2)
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