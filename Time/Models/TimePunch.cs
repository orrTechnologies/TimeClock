using System.Data;

namespace TimeClock.Data.Models
{
    public class TimePunch
    {
        public TimePunchStatus Status { get; set; }
        public DataSetDateTime Time { get; set; }
    }
}