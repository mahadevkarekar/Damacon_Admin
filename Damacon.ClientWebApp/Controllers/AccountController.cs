using System.Threading.Tasks;
using System.Web.Mvc;
using Damacon.ClientWebApp.Models;
using Damacon.DAL.Operations.Contracts;
using Damacon.SharedWeb.Helpers;
using Damacon.DAL;
using System.Linq;
using Damacon.DAL.Database.EF;
using Damacon.DAL.Utils;
using Damacon.DAL.i18n;
using System.Collections.Generic;

namespace Damacon.ClientWebApp.Controllers
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
            if (!model.IsPrivacyPolicyAccepted)
            {
                ModelState.AddModelError("", Resources.M_KindlyAcceptPrivacyPolicy);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            var result = DAL.DALFactory.GetDALObject<IClientDAL>().VerifyClientCredentials(model.Username, model.Password, GetIP());
            if (result.IsSuccess)
            {
                var user = result.GetSingleResult();
                ClientManager.SetLoggedInUser(user);
                return RedirectToLocal(returnUrl, "Home", "ShowShopping");
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetRecoverPasswordView()
        {
            return PartialView("~/Views/Shared/EditorTemplates/RecoverPasswordView.cshtml", new RecoverPasswordViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        //public ActionResult RecoverPassword(RecoverPasswordViewModel recoverPasswordViewModel)
        //{
            //if (ModelState.IsValid)
            //{
            //    bool isSuccess = false;
            //    string errorMessage = string.Empty;
            //    var result = DALFactory.GetDALObject<IClientDAL>().RecoverClientPassword(recoverPasswordViewModel.CardCodeOrEmail);
            //    if(result.IsSuccess)
            //    {
            //        var emailSetting = DALFactory.GetDALObject<IEmailServiceDAL>().GetEmailServiceItemByType(EmailSendConditionType.RecoverClientPasswordEmailTask);
            //        if (emailSetting.IsSuccess && emailSetting.Result.Count > 0)
            //        {
            //            Dictionary<string, string> tokenToReplace = new Dictionary<string, string>();
            //            tokenToReplace.Add("{ClientName}", result.GetSingleResult().FullName);
            //            tokenToReplace.Add("{ClientPassword}", result.GetSingleResult().ClientPassword);
            //            bool isEmailSent = EmailServiceHelper.SendEmailWithAttachment(emailSetting.GetSingleResult(), result.GetSingleResult().Email, null, null, tokenToReplace);
            //            if (isEmailSent)
            //            {
            //                isSuccess = true;
            //            }
            //            else
            //            {
            //                errorMessage = Resources.M_UnableToEmail;
            //            }
            //        }
            //        else
            //        {
            //            errorMessage = Resources.M_EmailNotConfiguredInStore;
            //        }
            //    }
            //    else
            //    {
            //        errorMessage = result.ErrorMessage;
            //    }
            //    return Json(new { IsSuccess = isSuccess, ErrorMessage = errorMessage });
            //}
            //var errorList = ModelState.Values.SelectMany(x => x.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(x => x.ErrorMessage).ToList();
            //return Json(new { IsSuccess = false, ErrorMessage = string.Join(System.Environment.NewLine, errorList) });
       // }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}