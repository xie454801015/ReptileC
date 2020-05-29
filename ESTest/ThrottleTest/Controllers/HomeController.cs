using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThrottleTest.Attribute.ThrottleAttribute;

namespace ThrottleTest.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    //[Throttle(QPSLimit.LimitingType.LeakageBucket, 15, 1)]
    [Throttle(QPSLimit.LimitingType.TokenBucket, 15, 1)]
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页1
        /// </summary>
        /// <returns></returns>
        //[Throttle(QPSLimit.LimitingType.LeakageBucket, 1, 20)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// about
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// contact
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 测试1
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public ContentResult Test1()
        {

            return Content("11111");
        }
        /// <summary>
        /// 测试2
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public ContentResult Test2()
        {

            return Content("22222");
        }
    }
}