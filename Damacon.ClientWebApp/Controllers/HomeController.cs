using Damacon.ClientWebApp.Models;
using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.SharedWeb.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Damacon.ClientWebApp.Controllers
{
    public class HomeController : BaseController
    {
        //[HttpGet]
        ////public ActionResult ShowShopping()
        //{
        //    PopulateShoppingRecordTypes();
        //    var getShoppingData = DALFactory.GetDALObject<IShoppingDetailDAL>().GetShoppingDetailByClient(ClientManager.GetLoggedInUser().ID);
        //    if (getShoppingData.IsSuccess)
        //    {
        //        return View(getShoppingData.GetSingleResult());
        //    }
        //    else
        //    {
        //        return View(new ShoppingByClientData());
        //    }
        //}

        //public ActionResult ShoppingDetail_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    var getShoppingData = DALFactory.GetDALObject<IShoppingDetailDAL>().GetShoppingDetailByClient(ClientManager.GetLoggedInUser().ID);
        //    if (getShoppingData.IsSuccess)
        //    {
        //        return Json(getShoppingData.GetSingleResult().ShoppingDetailList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    }
        //    return Json( new List<ShoppingDetailView>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //}

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RefreshLanguageCache()
        {
            Resources.RefreshResourceCache();
            return Json(new { Status = "Done" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetChangePasswordView()
        {
           return PartialView("~/Views/Shared/EditorTemplates/ChangePasswordView.cshtml", new ChangePasswordViewModel());
        }

        [HttpPost]
        public ActionResult UpdatePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = DALFactory.GetDALObject<IClientDAL>().ChangeClientPassword(ClientManager.GetLoggedInUser().ID, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
                return Json(new { IsSuccess = result.IsSuccess, ErrorMessage = result.ErrorMessage });
            }
            var errorList = ModelState.Values.SelectMany(x => x.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(x => x.ErrorMessage).ToList();
            return Json(new { IsSuccess = false, ErrorMessage = string.Join(System.Environment.NewLine, errorList) });
        }


        [HttpGet]
        public ActionResult GetChangeEmailView()
        {
            ChangeEmailViewModel changeEmailViewModel = new ChangeEmailViewModel();
            var result = DALFactory.GetDALObject<IClientDAL>().GetById(ClientManager.GetLoggedInUser().ID);
            if (result.IsSuccess)
            {
                changeEmailViewModel.CurrentEmail = result.GetSingleResult().Email;
            }
            return PartialView("~/Views/Shared/EditorTemplates/ChangeEmailView.cshtml", changeEmailViewModel);
        }

        [HttpPost]
        public ActionResult UpdateEmail(ChangeEmailViewModel changeEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = DALFactory.GetDALObject<IClientDAL>().ChangeClientEmail(ClientManager.GetLoggedInUser().ID, changeEmailViewModel.NewEmail);
                return Json(new { IsSuccess = result.IsSuccess, ErrorMessage = result.ErrorMessage });
            }
            var errorList = ModelState.Values.SelectMany(x => x.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(x => x.ErrorMessage).ToList();
            return Json(new { IsSuccess = false, ErrorMessage = string.Join(System.Environment.NewLine, errorList) });
        }

        //[HttpPost]
        //public ActionResult PrintAndEmailClientBalanceDetail()
        //{
        //    var result = ClientPrintAndEmail.ProcessClientBalancePrintAndEmail(ClientManager.GetLoggedInUser().ID, true, false, SaveDocument);
        //    return ConvertToJsonResult(result);
        //}

        public void SaveDocument(Stream stream, string fileName, string clientName, PrintAndEmailResult printAndEmailResult)
        {
        }

        private JsonResult ConvertToJsonResult(PrintAndEmailResult printAndEmailResult)
        {
            return Json(new
            {
                IsSuccess = string.IsNullOrEmpty(printAndEmailResult.PrintError) && string.IsNullOrEmpty(printAndEmailResult.EmailError),
                PrintData = printAndEmailResult.PrintData,
                PrintError = printAndEmailResult.PrintError ?? "",
                EmailError = printAndEmailResult.EmailError ?? ""
            }, JsonRequestBehavior.AllowGet);
        }
    }
}