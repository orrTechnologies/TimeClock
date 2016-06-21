using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        protected override void Seed(TimeClockContext context)
        {
            if (!context.Employees.Any())
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
            }
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Dylan"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Dylan" };

                manager.Create(user, "password");
                manager.AddToRole(user.Id, "Admin");
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
