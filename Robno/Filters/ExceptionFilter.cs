using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Robno.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //FileLogger fileL = new FileLogger();
            //fileL.LogException(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}