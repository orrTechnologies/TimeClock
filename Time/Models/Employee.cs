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
        public int? PIN { get; set; }
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
            bool set = (PIN == null);
            bool matches = (PINNumber == PIN);
            return (PIN == null || PINNumber == PIN);
        }

        public DateTime? LastPunchTime { get; set; }
    }
}