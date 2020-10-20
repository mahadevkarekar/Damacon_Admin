using Damacon.SharedWeb.HttpFilters;
using System.Web;
using System.Web.Mvc;

namespace Damacon.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizationFilter());
        }
    }
}
