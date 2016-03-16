using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TimeClock.Data.Models
{
    public class Employee
    {
        private TimePunchStatus _status;

        [Key]
        public int Key { get; set; }
        public int EmployeeId { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }

        public TimePunchStatus CurrentStatus
        {
            get { return _status; }
            set 
            {
                _status = value;
                LastPunchTime = DateTime.Now;
            }
        }

        public DateTime LastPunchTime { get; set; }
    }

    public class TimePunch
    {
        public TimePunchStatus Status { get; set; }
        public DataSetDateTime Time { get; set; }
    }

    public enum TimePunchStatus
    {
       PunchedIn,
        PunchedOut
    }
}