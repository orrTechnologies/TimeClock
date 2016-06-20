using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timeclock.Api.Models;

namespace Timeclock.Api.Controllers
{
    [RoutePrefix("Time/")]
    public class TimeController : ApiController
    {
        public IEnumerable<TimePunchBindingModel> GetTimePunchList()
        {
            return new List<TimePunchBindingModel>();
        }
    }
}
