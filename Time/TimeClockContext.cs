using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public interface ITimeClockContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<TimePunch> TimePunches { get; set; } 
        int SaveChanges();
    }
    public class TimeClockContext : DbContext, ITimeClockContext
    {
        public TimeClockContext()
            : base("name=DefaultConnection")
        {
            
        }
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<TimePunch> TimePunches { get; set; }
    }
}
