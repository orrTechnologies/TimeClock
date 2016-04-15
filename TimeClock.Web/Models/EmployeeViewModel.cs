using System;
using System.ComponentModel.DataAnnotations;
using TimeClock.Data.Models;

namespace TimeClock.Web.Models
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
        [Display(Name="Time")]
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
    public class EmployeeManagerViewModel
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
      
}