using System;
using System.Web.Mvc;

namespace LAB.Controllers
{
    internal class ElapsedTimeWriteToDebugAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.StartDt = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Action executed, elapsed time (ms): {(DateTime.Now - (DateTime)filterContext.Controller.ViewBag.StartDt).TotalMilliseconds}");
            
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ResultStartDt = DateTime.Now;
            
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine(
                $"ActionResult executed, elapsed time (ms): {(DateTime.Now - (DateTime)filterContext.Controller.ViewBag.ResultStartDt).TotalMilliseconds}");

            base.OnResultExecuted(filterContext);
        }
    }
}