using TimeClock.Data.Models;

namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeClockContext>
    {
        public Configuration()
        {
            Database.SetInitializer<TimeClockContext>(new CreateDatabaseIfNotExists<TimeClockContext>());
            AutomaticMigrationsEnabled = true;
            ContextKey = "TimeClock.Data.TimeClockContext";
        }

        protected override void Seed(TimeClockContext context)
        {
            for (int i = 0; i < 30; i++)
            {
                context.Employees.AddOrUpdate(new Employee()
                {
                    FirstName = "EmployeeNumber" + i,
                    LastName = "Last",
                    LastPunchTime = DateTime.Now
                });
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

    }
}
