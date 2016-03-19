using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
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
        internal static ITimeService CreateService(Mock<TimeClockContext> context = null, Mock <DbSet<TimePunch>> punchListDbSet = null,
            List<TimePunch> timePunchData = null )
        {
            Mock<TimeClockContext> mockContext = context ?? new Mock<TimeClockContext>();
            Mock<DbSet<TimePunch>> mockSet = punchListDbSet ?? new Mock<DbSet<TimePunch>>();

            var employee = new Employee()
            {
                CurrentStatus = TimePunchStatus.PunchedIn,
                EmployeeId = 1,
            };

            List<TimePunch> data;
            if (timePunchData == null)
            {
                data = new List<TimePunch>()
                {
                    new TimePunch(1, TimePunchStatus.PunchedIn, DateTime.Now)
                };
            }
            else
            {
                data = timePunchData;
            }


            mockSet.As<IQueryable<TimePunch>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<TimePunch>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<TimePunch>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<TimePunch>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            mockContext.Setup(c => c.TimePunches).Returns(mockSet.Object);

            ITimeService timeService = new TimeService(mockContext.Object);
            return timeService;
        }
        [TestClass]
        public class TheAddTimePunchMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<TimePunch>> _punchList;
            [TestInitialize]
            public void Initialize()
            {
                _context = new Mock<TimeClockContext>();
                _punchList = new Mock<DbSet<TimePunch>>();
            }
            [TestMethod]
            public void Add_A_New_Time_Punch_To_Context()
            {
                var employee = new Employee();

                var timeService = CreateService(context: _context, punchListDbSet: _punchList);
                timeService.AddTimePunch(employee, new TimePunch(1, TimePunchStatus.PunchedIn, DateTime.Now));

                _punchList.Verify(m => m.Add(It.IsAny<TimePunch>()), Times.Once);
                _context.Verify(m => m.SaveChanges(), Times.Once);
            }
        }
        [TestClass]
        public class ThePunchListMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<TimePunch>> _punchList;
            [TestInitialize]
            public void Initialize()
            {
                _context = new Mock<TimeClockContext>();
                _punchList = new Mock<DbSet<TimePunch>>();
            }

            [TestMethod]
            public void Return_A_List_Of_Punch_Time()
            {
                var timeService = CreateService();
                var employee = new Employee()
                {
                    EmployeeId = 1
                };

               IEnumerable<TimePunch> punchList = timeService.GetPunchList(employee.EmployeeId,
                    new TimeClockSpan(DateTime.Now.AddDays(-1), DateTime.Now));
            }

            [TestMethod]
            public void Return_Punch_List_In_Time_Range()
            {
                var startTime = new DateTime(2016, 1, 1);
                var stopTime = new DateTime(2016, 1, 2);

                var employee = new Employee()
                {
                    EmployeeId = 5
                };

                List<TimePunch> timePunchData = new List<TimePunch>()
                {
                    new TimePunch(5, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 1)),
                    new TimePunch(5, TimePunchStatus.PunchedOut, new DateTime(2016, 1, 2)),
                    new TimePunch(5, TimePunchStatus.PunchedIn, new DateTime(2016, 1, 3))
                };

                var timeService = CreateService(timePunchData: timePunchData);
                var timeSheet = timeService.GetPunchList(5, new TimeClockSpan(new DateTime(2016, 1, 1), new DateTime(2016, 1, 2)));

                bool inTimeRange = true;

                foreach (TimePunch punch in timeSheet.Where(punch => punch.Time < startTime || punch.Time > stopTime))
                {
                    inTimeRange = false;
                }
                Assert.IsTrue(inTimeRange);
                Assert.IsTrue(timeSheet.Count() == 2);
            }
        }
    }
}
