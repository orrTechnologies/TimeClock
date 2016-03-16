using TimeClock.Data.Models;

namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeClock.Data.TimeClockContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TimeClock.Data.TimeClockContext";
        }

        protected override void Seed(TimeClock.Data.TimeClockContext context)
        {

            context.Employees.AddOrUpdate(
                new Employee() { FirstName = "dylan", LastName = "orr", LastPunchTime = DateTime.Now},
                    new Employee() { FirstName = "Tyler", LastName = "Mork", LastPunchTime = DateTime.Now }
                    );
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
