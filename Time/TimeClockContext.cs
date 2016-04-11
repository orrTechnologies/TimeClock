using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public interface ITimeClockContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<TimePunch> TimePunches { get; set; }
        int SaveChanges();
    }
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class TimeClockContext : IdentityDbContext, ITimeClockContext
    {
        public TimeClockContext()
            : base("name=DefaultConnection")
        {

        }
        public static TimeClockContext Create()
        {
            return new TimeClockContext();
        }
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<TimePunch> TimePunches { get; set; }

    }
}
