using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeClock.Data.Models;

namespace Timeclock.Api.Models
{
    public class EmployeeBindingModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastPunchTime { get; set; }
        public TimePunchStatus CurrentStatus { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string CurrentStatusClass
        {
            get
            {
                return CurrentStatus == TimePunchStatus.PunchedIn ?
                    "punched-in" : "punched-out";
            }
        }
    }

    public class EmployeeAddBindingMdoel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? PIN { get; set; }
    }
    public class EmployeeEditBindingModal
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? PIN { get; set; }
    }
}