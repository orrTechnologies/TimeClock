using TimeClock.Data.Models;
using TimeClock.Web.Models;

namespace Timeclock.Services
{
    public interface IReportService
    {
        ITimeReport GenerateTimeWorkReport(int employeeIds, TimeClockSpan span);
    }
}