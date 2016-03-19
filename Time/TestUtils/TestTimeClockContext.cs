//using System.Data.Entity;
//using TimeClock.Data;
//using TimeClock.Data.Models;
//using WebApplication1.Tests.TestUtils;

//namespace WebApplication1.Tests
//{
//    public class TestTimeClockContext : ITimeClockContext
//    {

//        public TestTimeClockContext()
//        {
//            this.Employees = new TestDbSet<Employee>();
//        }

//        public DbSet<Employee> Employees { get; set; }
//        public int SaveChangesCount { get; private set; }
//        public int SaveChanges()
//        {
//            this.SaveChangesCount++;
//            return 1;
//        }
//    }
//}
