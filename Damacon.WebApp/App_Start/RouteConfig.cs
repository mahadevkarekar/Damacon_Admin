using Damacon.DAL.i18n;
using System.Web.Mvc;
using System.Web.Routing;

namespace Damacon.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ContinueSession",
                url: "Account/ContinueSession",
                defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Account", action = "ContinueSession", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
