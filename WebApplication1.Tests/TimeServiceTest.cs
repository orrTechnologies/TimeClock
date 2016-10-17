using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Timeclock.Services;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace WebApplication1.Tests
{
    [TestClass]
    public class TimeServiceTest
    {
        internal static ITimeService CreateService(Mock<TimeClockContext> context = null, Mock <DbSet<TimePunchRequest>> punchListDbSet = null,
            List<TimePunchRequest> timePunchData = null )
        {
            Mock<TimeClockContext> mockContext = context ?? new Mock<TimeClockContext>();
            Mock<DbSet<TimePunchRequest>> mockSet = punchListDbSet ?? new Mock<DbSet<TimePunchRequest>>();

            var employee = new Employee()
            {
                PunchStatus = TimePunchStatus.PunchedIn,
                EmployeeId = 1,
            };

            List<TimePunchRequest> data;
            if (timePunchData == null)
            {
                data = new List<TimePunchRequest>()
                {
                    new TimePunchRequest(1, TimePunchStatus.PunchedIn, DateTime.Now)
                };
            }
            else
            {
                data = timePunchData;
            }


            mockSet.As<IQueryable<TimePunchRequest>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<TimePunchRequest>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<TimePunchRequest>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<TimePunchRequest>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            mockContext.Setup(c => c.TimePunches).Returns(mockSet.Object);

            ITimeService timeService = new TimeService(mockContext.Object);
            return timeService;
        }
        [TestClass]
        public class TheAddTimePunchMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<TimePunchRequest>> _punchList;
            [TestInitialize]
            public void Initialize()
            {
                _context = new Mock<TimeClockContext>();
                _punchList = new Mock<DbSet<TimePunchRequest>>();
            }
            [TestMethod]
            public void Add_A_New_Time_Punch_To_Context()
            {
                var employee = new Employee();

                var timeService = CreateService(context: _context, punchListDbSet: _punchList);
                timeService.AddTimePunch(employee, new TimePunchRequest(1, TimePunchStatus.PunchedIn, DateTime.Now));

                _punchList.Verify(m => m.Add(It.IsAny<TimePunchRequest>()), Times.Once);
                _context.Verify(m => m.SaveChanges(), Times.Once);
            }
        }
        [TestClass]
        public class ThePunchListMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<TimePunchRequest>> _punchList;
            [TestInitialize]
            public void Initialize()
            {
                _context = new Mock<TimeClockContext>();
                _punchList = new Mock<DbSet<TimePunchRequest>>();
            }

            [TestMethod]
            public void Return_A_List_Of_Punch_Time()
            {
                var timeService = CreateService();
                var employee = new Employee()
                {
                    EmployeeId = 1
                };

               IEnumerable<TimePunchRequest> punchList = timeService.GetPunchList(employee.EmployeeId,
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

                List<TimePunchRequest> timePunchData = new List<TimePunchRequest>()
                {
                    new TimePunchRequest(5, TimePunchStatus.PunchedIn),
                    new TimePunchRequest(5, TimePunchStatus.PunchedOut),
                    new TimePunchRequest(5, TimePunchStatus.PunchedIn)
                };

                var timeService = CreateService(timePunchData: timePunchData);
                var timeSheet = timeService.GetPunchList(5, new TimeClockSpan(new DateTime(2016, 1, 1), new DateTime(2016, 1, 2)));

                bool inTimeRange = true;

                foreach (TimePunchRequest punch in timeSheet.Where(punch => punch.Time < startTime || punch.Time > stopTime))
                {
                    inTimeRange = false;
                }
                Assert.IsTrue(inTimeRange);
                Assert.IsTrue(timeSheet.Count() == 2);
            }
        }
    }
}
