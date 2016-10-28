using System.Collections.Generic;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public interface ITimeReport
    {
        Employee Employee { get; }
        IEnumerable<ITimeReportDaily> DailyReports { get; }
        double TimeWorked { get; }
    }
}