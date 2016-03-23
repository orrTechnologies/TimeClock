using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeClock.Data;
using TimeClock.Data.Models;
using TimeClock.Web.Controllers;
using TimeClock.Web.Services;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private static EmployeesController _controller;
        private Mock<IEmployeeService> _service;


        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<IEmployeeService>();
          //  _controller = new EmployeesController(_service.Object);
        }
        public class TheIndexAction
        {
            [TestMethod]
            public void ShouldReturnANotNullResult()
            {
           //     _service.Setup(s => s.)
                // Act
                ViewResult result = _controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(result);
            }
        }

        [TestClass]
        public class TheDetailAction
        {
            private static EmployeesController _controller;
            private Mock<IEmployeeService> _service;


            [TestInitialize]
            public void Initialize()
            {
                _service = new Mock<IEmployeeService>();
                //_controller = new EmployeesController(_service.Object);


            }
            public void GivenAValidIdShouldReturnViewWithEmployeViewModel()
            {

            }

        }
    }
}
