using System;
using TimeClock.Data.Models;

namespace Timeclock.Core.Domain
{
    public class TimePunch
    {
        public int Id { get; set; }
        internal TimePunch() { }
        public TimePunch(TimePunchStatus status, DateTime time)
        {
            Status = status;
            Time = time;
        }

        #region Properties

        public TimePunchStatus Status { get; private set; }
        public DateTime Time { get; private set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the employee
        /// </summary>
        public virtual Employee Employee { get; set; }

        #endregion
    }
}