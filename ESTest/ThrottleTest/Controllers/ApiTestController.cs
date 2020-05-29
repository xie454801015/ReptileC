using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ThrottleTest.Controllers
{
    /// <summary>
    /// TESTaPI
    /// </summary>
    public class ApiTestController:ApiController
    {
        /// <summary>
        /// test1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}