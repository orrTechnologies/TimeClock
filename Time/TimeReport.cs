using System;
using System.Collections.Generic;
using System.Linq;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace TimeClock.Web.Models
{
    public interface ITimeReport
    {
        Employee Employee { get; }
        IEnumerable<TimeReportDaily> DailyReports { get; }
        double TimeWorked { get; }
    }

    public class TimeReport : ITimeReport
    {

        public  Employee Employee { get; private set; }
        private  IReadOnlyCollection<TimePunch> _timePunches;
        private double _totalMinutesTimeWorked = -1;
        public TimeReport(Employee employee, IReadOnlyCollection<TimePunch> timePunches)
        {
            Employee = employee;
            _timePunches = timePunches;
        }

        //Cache the result. 
        public double TimeWorked
        {
            get
            {
                if (_totalMinutesTimeWorked == -1)
                {
                    _totalMinutesTimeWorked = Calculate();
                }
                return Math.Round(_totalMinutesTimeWorked / 60, 2);
            }
        }

        public IEnumerable<TimeReportDaily> DailyReports
        {
            get
            {
                var timePunchByDay = _timePunches.GroupBy(t => t.Time.Date);
                var dailyReports = timePunchByDay.Select(t => new TimeReportDaily()
                {
                    Date = t.Key,
                    TimePunches = t.ToList()
                });

                return dailyReports.ToList();
            }
        }

        /// <summary>
        /// Enumerate over the list of timePunches. Return when either index would cause and outOfBounds exceptions.
        /// Recursivly call until you get a pair of punch in and punch out. Calculate time span, and recursively call until base case reached.  
        /// </summary>
        /// <param name="inIndex"></param>
        /// <param name="outIndex"></param>
        /// <param name="totalMinutes"></param>
        /// <returns></returns>
        private double Calculate(int inIndex = 0, int outIndex = 1, double totalMinutes = 0)
        {
            //if we are out of time punches in enumeration return current calculation
            //Base Case: end of enumeration. 
            //Make sure we have at
            int indexCount = _timePunches.Count - 1;

            if (inIndex > indexCount || outIndex > indexCount) return totalMinutes;

            //We need to start with a time punch with status in.
            TimePunch inTimePunch = _timePunches.ElementAt(inIndex);
            if (inTimePunch.Status == TimePunchStatus.PunchedOut) return Calculate(++inIndex, ++outIndex, totalMinutes);

            //Make sure we have a punchOut.
            TimePunch outTimePunch = _timePunches.ElementAt(outIndex);
            if (outTimePunch.Status == TimePunchStatus.PunchedIn) return Calculate(inIndex, ++outIndex, totalMinutes);

            TimeSpan span = (outTimePunch.Time - inTimePunch.Time);
            totalMinutes += span.Minutes;

            return Calculate(++outIndex, ++outIndex, totalMinutes);
        }

        //private double CalculateTotalHoursWorked()
        //{
        //    double totalTime = 0;
        //    var punchCount = _timePunches.Count();

        //    if (punchCount == 0) return 0;
        //    //skip first time if it is punched out. No punch in time to calulcate time worked with. 
        //    if (_timePunches.First().Status == TimePunchStatus.PunchedOut)
        //    {
        //        _timePunches = _timePunches.Skip(1);
        //    }


        //    for (int i = 0; i < punchCount; i += 2)
        //    {
        //        var timeOne = _timePunches.ElementAt(i).Time;
        //        DateTime timeTwo;
        //        //We are not the last element, use the next clockout time as to calculate time worked.
        //        if (i != punchCount - 1)
        //        {
        //            timeTwo = _timePunches.ElementAt(i + 1).Time;
        //        }
        //        else
        //        {
        //            timeTwo = DateTime.Now;
        //        }
        //        TimeSpan span = (timeTwo - timeOne);
        //        totalTime += span.Minutes;
        //    }

        //    return Math.Round(totalTime / 60, 2);
        //}
    }

  
}