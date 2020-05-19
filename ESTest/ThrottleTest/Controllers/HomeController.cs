using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrottleTest.Attribute.ThrottleAttribute;

namespace ThrottleTest.Controllers
{
    [Throttle(QPSLimit.LimitingType.LeakageBucket, 100, 99)]
    public class HomeController : Controller
    {
        //[Throttle(QPSLimit.LimitingType.LeakageBucket, 1, 20)]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ContentResult Test1()
        {

            return Content("11111");
        }

        public ContentResult Test2()
        {

            return Content("22222");
        }
    }
}