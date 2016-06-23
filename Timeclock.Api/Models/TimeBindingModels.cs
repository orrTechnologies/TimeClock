using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeClock.Data.Models;

namespace Timeclock.Api.Models
{
        public class TimePunchBindingModel
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public TimePunchStatus Status { get; set; }
            public int? PIN { get; set; }
        }
}