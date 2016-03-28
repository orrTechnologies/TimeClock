using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeClock.Data.Models;
using TimeClock.Web.Models;

namespace WebApplication1.Tests
{
    [TestClass]
    public class TimeReportTest
    {
        [TestClass]
        public class HoursWorkedMethod
        {
            [TestMethod]
            public void Given_Two_Times_Return_Difference()
            {
                var timePunches = new List<TimePunch>();
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1, 1, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 2, 1, 1)));

                var report = new TimeReport(new Employee(), timePunches);
                Assert.IsTrue(report.TimeWorked == 1);
            }  
        }
    }
}
