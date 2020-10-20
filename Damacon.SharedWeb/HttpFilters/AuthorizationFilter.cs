using Damacon.SharedWeb.Helpers;
using System.Web;
using System.Web.Mvc;

namespace Damacon.SharedWeb.HttpFilters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (!UserManager.IsUserLoggedIn() && !ClientManager.IsUserLoggedIn())
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Items["AjaxPermissionDenied"] = true;
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/");
                }
            }
        }
    }
}