using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClock.Data.Models
{
    public class TimeClockSpan
    {
        public readonly DateTime _start;
        public readonly DateTime _end;

        public TimeClockSpan(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }
        
        public TimeSpan Span()
        {
            return (_start - _end);
        }
    }
}
