using System.Web;
using System.Web.Mvc;
using Robno.Filters;

namespace Robno
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //mijenjano
            filters.Add(new ExceptionFilter());
        }
    }
}
