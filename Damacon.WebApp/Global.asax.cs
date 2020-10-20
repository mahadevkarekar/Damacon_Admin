using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Damacon.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            if (Context.Items["AjaxPermissionDenied"] is bool)
            {
                Context.Response.StatusCode = 306;
                Context.Response.End();
            }
        }
    }
}
