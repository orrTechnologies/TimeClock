using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Timeclock.Api.Models;
using Timeclock.Services;
using TimeClock.Data.Models;

namespace Timeclock.Api.Controllers
{
        [RoutePrefix("api/Time")]
    public class TimeController : ApiController
    {
            private readonly ITimeService _timeService;

            public TimeController(ITimeService timeService)
            {
                _timeService = timeService;
            }

            [HttpGet]
            public TimePunchBindingModal Load(int id)
            {
                TimePunch punch = _timeService.FindById(id);
                return new TimePunchBindingModal()
                {
                    Id = punch.Id,
                    Status = punch.Status,
                    Time = punch.Time
                };
            }

            [HttpPost]
            [Route("LoadByDate/")]
            public List<TimePunchBindingModal> LoadByDate(TimeCardRequest timeCardRequest)
            {
                if (ModelState.IsValid)
                {
                     return _timeService.GetPunchList(timeCardRequest.Id,
                        new TimeClockSpan(timeCardRequest.StartTime, timeCardRequest.EndTime))
                        .Select(t => new TimePunchBindingModal()
                        {
                            Id = t.Id,
                            Status = t.Status,
                            Time = t.Time
                        }).ToList();
                }
                return null;
            }
    }
}