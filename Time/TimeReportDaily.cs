using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeclock.Core.Domain;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public class TimeReportDaily : ITimeReportDaily
    {
        public DateTime Date { get; set; }
        public IEnumerable<TimePunch> TimePunches { get; set; }
        //TODO: Place holder, we need to calculate time worked for display+
        public double TimeWorked { get; set; }
    }
}
