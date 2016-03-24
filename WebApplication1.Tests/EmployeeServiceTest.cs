using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using TimeClock.Data;
using TimeClock.Data.Models;
using TimeClock.Web.Services;

namespace WebApplication1.Tests
{
    /// <summary>
    /// Employee should be able to clock in, out and for lunch.
    /// Employee can not change clock status to their current status(IE can not clock in if already clocked in)
    /// Emplo
    /// </summary>
[TestClass]
    public class EmployeeServicesTest
    {
        internal static class ServicesTestHelper
        {
            internal static IEmployeeService CreateService(
                Mock<TimeClockContext> context = null, Mock<DbSet<Employee>> employeData = null)
            {
                Mock<DbSet<Employee>> mockSet = employeData ?? new Mock<DbSet<Employee>>();
                Mock<TimeClockContext> mockContext = context ?? new Mock<TimeClockContext>();

                var data = new List<Employee>()
                {
                    new Employee() {EmployeeId = 1, FirstName = "dylan", LastName = "orr", CurrentStatus = TimePunchStatus.PunchedIn},
                    new Employee() {EmployeeId = 2, FirstName = "Tyler", LastName = "Mork", CurrentStatus = TimePunchStatus.PunchedOut}
                }.AsQueryable();


                mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                mockContext.Setup(c => c.Employees).Returns(mockSet.Object);
                mockContext.Setup(m => m.Employees).Returns(mockSet.Object);

                mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => mockContext.Object.Employees.FirstOrDefault(e => e.EmployeeId == (int)ids[0]));

                return new EmployeeService(mockContext.Object);
            }
            internal static Employee CreateEmployee(TimePunchStatus status = TimePunchStatus.PunchedIn)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = 1,
                    FirstName = "Dylan",
                    LastName = "Orr",
                    CurrentStatus = status
                };
                return employee;
            }
        }
        [TestClass]
        public class TheClockMethod
        {
            private Mock<TimeClockContext> _context;

            [TestInitialize]
            public void Initialize()
            {
                _context = new Mock<TimeClockContext>();

            }

            [TestMethod]
            public void Returns_True_When_ClockedOut_Employee_ClocksIn()
            {
                Employee employee = ServicesTestHelper.CreateEmployee(TimePunchStatus.PunchedOut);

                IEmployeeService service = ServicesTestHelper.CreateService(context: _context);
                bool success = service.ChangeClockStatus(employee, TimePunchStatus.PunchedIn);

                Assert.IsTrue(success);
            }

            [TestMethod]
            public void Returns_False_When_ClockedOut_Employee_ClocksOut()
            {
                Employee employee = ServicesTestHelper.CreateEmployee(TimePunchStatus.PunchedOut);

                IEmployeeService service = ServicesTestHelper.CreateService();
                var success = service.ChangeClockStatus(employee, TimePunchStatus.PunchedOut);

                Assert.IsFalse(success);
            }
            [TestMethod]
            public void Sets_Employee_Current_Status_To_Last_Punch()
            {
                Employee employee = ServicesTestHelper.CreateEmployee();
                IEmployeeService service = ServicesTestHelper.CreateService();

                service.ChangeClockStatus(employee, TimePunchStatus.PunchedOut);
                Assert.IsTrue(employee.CurrentStatus == TimePunchStatus.PunchedOut);
            }
            [TestMethod]
            public void Sets_Employee_Last_Punch_Time_To_Last_Punch()
            {
                Employee employee = ServicesTestHelper.CreateEmployee();
                IEmployeeService service = ServicesTestHelper.CreateService();

                service.ChangeClockStatus(employee, TimePunchStatus.PunchedOut);
                var span = (DateTime.Now - employee.LastPunchTime).Value.Seconds <= 100;

                Assert.IsTrue(span);
            }
            [TestMethod]
            public void Saves_Current_Punch_Status()
            {
                Employee employee = ServicesTestHelper.CreateEmployee();
                IEmployeeService service = ServicesTestHelper.CreateService(context: _context);

                service.ChangeClockStatus(employee, TimePunchStatus.PunchedOut);
                _context.Verify(m => m.SaveChanges(), Times.Once);
                
                Assert.IsTrue(employee.CurrentStatus == TimePunchStatus.PunchedOut);
            }
        }

        [TestClass]
        public class TheCreateEmployeeMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<Employee>> _employees;

            [TestInitialize]
            public void Initialize()
            {
                _employees = new Mock<DbSet<Employee>>();
                _context = new Mock<TimeClockContext>();
            }

            [TestMethod]
            public void Add_Employee_To_Context()
            {
                Employee employee = ServicesTestHelper.CreateEmployee();
                IEmployeeService service = ServicesTestHelper.CreateService(context: _context, employeData: _employees);

                service.CreateEmployee(employee);
                //Make sure an employee is added to the context, and saveChanges is called
                _employees.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
                _context.Verify(m => m.SaveChanges(), Times.Once);
            }
        }

        [TestClass]
        public class TheGetEmployeeListMethod
        {
            private Mock<TimeClockContext> _context;
            private Mock<DbSet<Employee>> _employees;

            [TestInitialize]
            public void Initialize()
            {
                _employees = new Mock<DbSet<Employee>>();
                _context = new Mock<TimeClockContext>();

                var data = new List<Employee>()
                {
                    new Employee() {FirstName = "dylan", LastName = "orr"},
                    new Employee() {FirstName = "Tyler", LastName = "Mork"}
                }.AsQueryable();

                _employees.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
                _employees.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
                _employees.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
                _employees.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                _context.Setup(c => c.Employees).Returns(_employees.Object);
            }

            [TestMethod]
            public void Return_List_Of_Employees_In_Context()
            {
                var service = ServicesTestHelper.CreateService(_context);
                List<Employee> employees = service.GetEmployeeList();

                Assert.IsTrue(employees.Count == 2);
            }

        }

        [TestClass]
        public class TheFindByIdMethod
        {
            private Mock<DbSet<Employee>> _employees;
            private Mock<TimeClockContext> _context;

            [TestInitialize]
            public void Initialize()
            {
                _employees = new Mock<DbSet<Employee>>();
                _context = new Mock<TimeClockContext>();

            }
            [TestMethod]
            public void GivenAValidIdShouldReturnEmployee()
            {
                IEmployeeService service = ServicesTestHelper.CreateService();
                Employee employee = service.FindById(1);

                Assert.IsNotNull(employee);
            }
            [TestMethod]
            public void GivenAnIdofONeShouldReturnEmployeeOne()
            {
                IEmployeeService service = ServicesTestHelper.CreateService();
                Employee employee = service.FindById(1);

                Assert.IsTrue(employee.EmployeeId == 1);
            }

            [TestMethod]
            public void GivenAnInvalidIdShouldReturnNull()
            {
                IEmployeeService service = ServicesTestHelper.CreateService();
                Employee employee = service.FindById(3);

                Assert.IsNull(employee);
            }
        }

        [TestClass]
        public class TheUpdateEmployeeMethod
        {
            private Mock<DbSet<Employee>> _employees;
            private Mock<TimeClockContext> _context;

            [TestInitialize]
            public void Initialize()
            {
                _employees = new Mock<DbSet<Employee>>();
                _context = new Mock<TimeClockContext>();

            }
            [TestMethod]
            public void ShouldUpdateCustomerInContext()
            {
                Employee employee = ServicesTestHelper.CreateEmployee();
                IEmployeeService service = ServicesTestHelper.CreateService(context: _context, employeData: _employees);

                service.UpdateEmployee(employee);
                _context.Verify(m => m.SaveChanges(), Times.Once);
            }
        }

        [TestClass]
        public class TheGetByStatusMethod
        {
            private Mock<DbSet<Employee>> _employees;
            private Mock<TimeClockContext> _context;

            [TestInitialize]
            public void Initialize()
            {
                _employees = new Mock<DbSet<Employee>>();
                _context = new Mock<TimeClockContext>();

            }
            [TestMethod]
            public void Given_A_Status_Should_return_Employees_Of_That_Status()
            {
                IEmployeeService service = ServicesTestHelper.CreateService();

                List<Employee> employees = service.FindByStatus(TimePunchStatus.PunchedIn);
                employees.ForEach(e => Assert.IsTrue(e.CurrentStatus == TimePunchStatus.PunchedIn));
            }
        }

    }
}
