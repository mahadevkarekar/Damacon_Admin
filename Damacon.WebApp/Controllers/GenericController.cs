using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using System.Web.Mvc;

namespace Damacon.WebApp.Controllers
{
    public class GenericController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult CheckSession()
        {
            return Json(new { SessionActive = UserManager.IsUserLoggedIn() }, JsonRequestBehavior.AllowGet);
        }
    }
}