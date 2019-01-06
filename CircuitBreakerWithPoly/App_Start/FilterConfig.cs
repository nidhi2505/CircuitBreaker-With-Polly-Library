using System.Web;
using System.Web.Mvc;

namespace CircuitBreakerWithPoly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
