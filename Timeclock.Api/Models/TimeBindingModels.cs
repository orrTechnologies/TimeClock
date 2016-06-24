using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeClock.Data.Models;

namespace Timeclock.Api.Models
{
    public class TimePunchRequestBindingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public TimePunchStatus Status { get; set; }
        public int? PIN { get; set; }
    }

    public class TimeCardRequest
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

    }

    public class TimePunchBindingModal
    {
        public int Id { get; set; }
        public TimePunchStatus Status { get; set; }

        public DateTime Time { get; set; }
    }
}