using System.Collections.Generic;
using Timeclock.Core.Domain;
using TimeClock.Data;

namespace Timeclock.Core
{
    public interface ITimeReport
    {
        Employee Employee { get; }
        IEnumerable<ITimeReportDaily> DailyReports { get; }
        double TimeWorked { get; }
    }
}