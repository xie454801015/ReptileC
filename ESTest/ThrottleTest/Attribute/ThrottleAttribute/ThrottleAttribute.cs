using Microsoft.Ajax.Utilities;
using QPSLimit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ThrottleTest.Attribute.ThrottleAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        public string Message { get; set; }
        public ILimitingService  service{get;set;}

        private int qps;
        private int maxSize;

        public ThrottleAttribute(LimitingType limitingType,int pqs,int maxSize)
        {
            this.qps = pqs;
            this.maxSize = maxSize;

            //service = LimitingFactory.Build(limitingType, pqs, maxSize);
        }

        //public override void OnActionExecuting(ActionExecutingContext c)
        //{
        //    var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
        //    var allowExecute = false;

        //    if (HttpRuntime.Cache[key] == null)
        //    {
        //        HttpRuntime.Cache.Add(key,
        //            true, // is this the smallest data we can have?
        //            null, // no dependencies
        //            DateTime.Now.AddSeconds(Seconds), // absolute expiration
        //            Cache.NoSlidingExpiration,
        //            CacheItemPriority.Low,
        //            null); // no callback

        //        allowExecute = true;
        //    }
        //    if (!allowExecute)
        //    {
        //        if (String.IsNullOrEmpty(Message))
        //            Message = "You may only perform this action every {n} seconds.";

        //        c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
        //        // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
        //        c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
        //    }

        //}


        public override void OnActionExecuted(ActionExecutedContext c)
        {
            string key = c.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + c.ActionDescriptor.ActionName;

            var service = TokenBucketLimitManager.GetLeakageBucketLimitingService(key, qps, maxSize);
            bool allowExecute = service.Request();
            if (!allowExecute)
            {
                if (String.IsNullOrEmpty(Message))
                    Message = "超限";
                c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                //c.HttpContext.Response.StatusCode = 429;
            }

        }
}
}