using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeClock.Data.Models;

namespace WebApplication1.Tests
{
    public interface ITimeService
    {
        void AddTimePunch(Employee employee, TimePunch timePunch);
    }
}
