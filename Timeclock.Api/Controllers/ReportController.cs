﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using TimeClock.Data.Models;
using TimeClock.Web.Models;

namespace Timeclock.Api.Controllers
{
    [RoutePrefix("api/Reports")]
    public class ReportController : ApiController
    {
        // GET api/report/5/{employeeIds
        [Route("")]
        [HttpGet]
        public string Get(string ids, string startTime, string endTime)
        {
            return "true";

        }
        [Route("Test/")]
        [HttpPost]
        public string Test(ReportRequest request)
        {
            return request.StartTime.ToString();

        }
    }
}