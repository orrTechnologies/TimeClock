using System;
using System.Data;

namespace TimeClock.Data.Models
{
    public class TimePunch
    {
        public TimePunch(TimePunchStatus status, DateTime time)
        {
            Status = status;
            Time = time;
        }
        public TimePunchStatus Status { get; private set; }
        public DateTime Time { get; private set; }
    }
}