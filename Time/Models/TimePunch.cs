using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TimeClock.Data.Models
{
    public class TimePunch
    {
        [Key]
        public int Id { get; set; }
        internal TimePunch() { }
        public TimePunch(int employeeId, TimePunchStatus status, DateTime time)
        {
            EmployeeId = employeeId;
            Status = status;
            Time = time;
        }

        public int EmployeeId { get; set; }
        public TimePunchStatus Status { get; private set; }
        public DateTime Time { get; private set; }
    }
}