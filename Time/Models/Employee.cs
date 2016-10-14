using System;
using System.ComponentModel.DataAnnotations;
// ReSharper disable InconsistentNaming

namespace TimeClock.Data.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        private TimePunchStatus _status;

        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        public Int16? PIN { get; set; }

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

        public bool HasPin()
        {
            return PIN != null;
        }

        public bool CheckPIN(int PINNumber)
        {
            return (PIN == null || PINNumber == PIN);
        }

        public DateTime? LastPunchTime { get; set; }
    }
}