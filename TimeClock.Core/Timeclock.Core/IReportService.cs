using Timeclock.Core;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace Timeclock.Services
{
    public interface IReportService
    {
        ITimeReport GenerateTimeWorkReport(int employeeIds, TimeClockSpan span);
    }
}