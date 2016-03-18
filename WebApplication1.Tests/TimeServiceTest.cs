using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeClock.Data;
using TimeClock.Data.Models;
using TimeClock.Web.Services;

namespace WebApplication1.Tests
{
    [TestClass]
    public class TimeServiceTest
    {
        internal static ITimeService CreateService(Mock<TimeClockContext> context = null, Mock <DbSet<TimePunch>> punchList = null)
        {
            Mock<TimeClockContext> mockContext = context ?? new Mock<TimeClockContext>();

            ITimeService timeService = new TimeService(mockContext.Object);
            return timeService;
        }
        [TestClass]
        public class TheAddTimePunchMethod
        {
            [TestMethod]
            public void Add_A_New_Time_Punch_To_Context()
            {
                var context = new Mock<TimeClockContext>();
                var punchList = new Mock <DbSet<TimePunch>>();

                var timeService = CreateService(context: context, punchList: punchList);
                timeService.AddTimePunch(new Employee(), new TimePunch(TimePunchStatus.PunchedIn, DateTime.Now));

                punchList.Verify(m => m.Add(It.IsAny<TimePunch>()), Times.Once);
                context.Verify(m => m.SaveChanges(), Times.Once);
            }
        }
    }
}
