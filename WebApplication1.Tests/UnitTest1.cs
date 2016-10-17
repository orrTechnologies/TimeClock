using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeClock.Data;
using TimeClock.Data.Models;

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
            [TestMethod]
            public void Given_Four_Times_Return_Time_ClockedIn()
            {
                var timePunches = new List<TimePunch>();
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1, 1, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 2, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1, 3, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 4, 1, 1)));
                var report = new TimeReport(new Employee(), timePunches);
                Assert.IsTrue(report.TimeWorked == 2);
            }
            [TestMethod]
            public void If_First_Time_Is_Clocked_Out_Skip()
            {
                var timePunches = new List<TimePunch>();
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 1, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1, 2, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 3, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1, 4, 1, 1)));
                timePunches.Add(new TimePunch(1, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 1, 5, 1, 1)));
                var report = new TimeReport(new Employee(), timePunches);
                Assert.IsTrue(report.TimeWorked == 2);
            }
            [TestMethod]
            public void Given_No_Time_Punches_Returns_0()
            {
                var timePunches = new List<TimePunch>();
                var report = new TimeReport(new Employee(), timePunches);

                Assert.IsTrue(report.TimeWorked == 0);
            }
        }
    }
}
