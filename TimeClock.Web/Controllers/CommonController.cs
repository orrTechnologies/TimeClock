using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TimeClock.Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        [ChildActionOnly]
        public ActionResult SideBarMenu()
        {
            var model = new SideBarMenuViewModel();
            model.IsAdmin = Roles.IsUserInRole("Admin");


            return PartialView(model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }

    public class SideBarMenuViewModel
    {
        public bool IsAdmin { get; set; }
    }
}