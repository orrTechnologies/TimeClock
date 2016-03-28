using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApplication1.Tests
{
    [TestClass]
    public class TimeReport
    {
        public class HoursWorkedMethod
        {
            [TestMethod]
            public void Given_Two_Times_Return_Difference()
            {
                var timeOne = new DateTime(2016, 1, 1, 1, 1, 1);
                var timeTwo = new DateTime(2016, 1, 1, 2, 1, 1);

                TimeReport report = new TimeReport();

            }  
        }
    }
}
