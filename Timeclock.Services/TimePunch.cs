using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TimeClock.Data.Models;

namespace Timeclock.Services
{
    public class TimePunchRequest
    {
        private readonly int _employeeId;
        private readonly TimePunchStatus _status;

        public TimePunchRequest(int id, TimePunchStatus status)
        {
            _employeeId = id;
            _status = status;
        }

        public int EmployeeId { get { return _employeeId; } }
        public TimePunchStatus Status { get{ return _status; } }
        public int? PIN { get; set; }
    }
}

