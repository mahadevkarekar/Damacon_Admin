using System.Threading.Tasks;
using System.Web.Mvc;
using Damacon.WebApp.Models;
using Damacon.DAL.Operations.Contracts;
using Damacon.WebApp.Helpers;
using Damacon.SharedWeb.Helpers;

namespace Damacon.WebApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            var result = DAL.DALFactory.GetDALObject<IUserDAL>().VerifyUserCredentials(model.Username, model.Password, GetIP());
            if (result.IsSuccess)
            {
                var user = result.GetSingleResult();
                UserManager.SetLoggedInUser(user);
                return RedirectToLocal(returnUrl, user.DefaultLoginPage.LinkController, user.DefaultLoginPage.LinkAction);
            }
            else
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            UserManager.SetLoggedInUser(null);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ContinueSession()
        {
            return Json(new object());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}