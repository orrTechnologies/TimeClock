using System;
using System.Collections.Generic;
using Timeclock.Core.Domain;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public interface ITimeReportDaily
    {
        DateTime Date { get; set; }
        IEnumerable<TimePunch> TimePunches { get; set; }
        double TimeWorked { get; set; }
    }
}