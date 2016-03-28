using System;
using System.ComponentModel.DataAnnotations;

namespace TimeClock.Data.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        private TimePunchStatus _status;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public TimePunchStatus CurrentStatus
        {
            get { return _status; }
            set 
            {
                _status = value;
                LastPunchTime = DateTime.Now;
            }
        }

        public DateTime? LastPunchTime { get; set; }
    }
}