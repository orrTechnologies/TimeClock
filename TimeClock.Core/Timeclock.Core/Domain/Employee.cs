using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace TimeClock.Data.Models
{
    public class Employee
    {
        private ICollection<TimePunch> _timePunches;
        private TimePunchStatus _status;
        public Employee()
        {
            
        }

        /// <summary>
        /// Gets or sets EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets Employee's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Employee's First Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Returns current punch status of employee.
        /// </summary>
        public TimePunchStatus PunchStatus
        {
            get { return _status; }
            set
            {
                _status = value;
                LastPunchTime = DateTime.Now;
            }
        }

        public Int16? PIN { get; set; }

        /// <summary>
        /// Returns formatted first and last name. 
        /// </summary>
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        /// <summary>
        /// Returns true if customer has a PIN set.
        /// </summary>
        /// <returns></returns>
        public bool HasPin()
        {
            return PIN != null;
        }

        /// <summary>
        /// Returns true if pin provied matches customers set pin.
        /// </summary>
        /// <param name="PINNumber">PIN to check.</param>
        /// <returns></returns>
        public bool CheckPIN(int PINNumber)
        {
            return (PIN == null || PINNumber == PIN);
        }

        /// <summary>
        /// Gets the time of the last punch time.
        /// </summary>
        public DateTime? LastPunchTime { get; set; }

        /// <summary>
        /// Gets or sets employee TimePunches 
        /// </summary>
        public virtual ICollection<TimePunch> TimePunches
        {
            get { return _timePunches ?? (_timePunches = new List<TimePunch>()); }
            protected set { _timePunches = value; }
        }
    }
}