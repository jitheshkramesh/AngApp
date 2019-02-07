using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngApp.Controllers
{
    //[Authorize(Users=@"ROOPA JITHESH,DHARA JITHESH")]
    public class ViewPageController : Controller
    {
        // GET: ViewPage
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Users=@"ROOPA JITHESH")] // Specific User Access
        public ActionResult Admin()
        {
            return View();
        }

        //[Authorize(Users = @"ROOPA JITHESH,DHARA JITHESH")]
        public ActionResult SimpleUsers()
        {
            return View();
        }
    }
}