using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeclock.Test.TestUtils;
using TimeClock.Data;
using TimeClock.Data.Models;

namespace Timeclock.Test
{
    internal class TestTimeClockContext : ITimeClockContext
    {

        public TestTimeClockContext()
        {
            this.Employees = new TestDbSet<Employee>();
        }

        public DbSet<Employee> Employees { get; set; }
        public int SaveChangesCount { get; private set; } 
        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1; 
        }
    }
}
