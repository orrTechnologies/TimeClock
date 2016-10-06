using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeClock.Web.Services;

namespace TimeClock.Web.Controllers
{
    public class AdminController : Controller
    {


        public AdminController()
        {

        }

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
    }

}