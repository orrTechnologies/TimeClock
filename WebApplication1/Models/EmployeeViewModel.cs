using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeClock.Data.Models;

namespace WebApplication1.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public DateTime? LastPunchTime { get; set; }

        [Display(Name = "Status")]
        public TimePunchStatus CurrentStatus { get; set; }
        [Display(Name = "Name")]
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

    public class EmployeeCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}