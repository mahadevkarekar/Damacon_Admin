using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.DAL.Utils;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using Damacon.WebApp.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Damacon.WebApp.Controllers
{
    public class SettingsController : BaseController
    {
        #region LanguageResources
        [HttpGet]
        public ActionResult Languages()
        {
            return View();
        }

        public ActionResult ManageLanguageResourcesEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        {
            var getAllLanguageResourcesResult = DALFactory.GetDALObject<ILanguageDAL>().GetAll(showDeleted.GetValueOrDefault());
            if (getAllLanguageResourcesResult.IsSuccess)
            {
                SetUpFilters<LanguageResourceModel>(request);
                var languageResourceModels = ModelMapper.Instance.Mapper.Map<IList<LanguageResourceModel>>(getAllLanguageResourcesResult.Result);
                return Json(languageResourceModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("", getAllLanguageResourcesResult.ErrorMessage);
                return Json(new LanguageResourceModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageLanguageResourcesEditingPopup_Create([DataSourceRequest] DataSourceRequest request, LanguageResourceModel languageResourceModel)
        {
            if (languageResourceModel != null && ModelState.IsValid)
            {
                var addLanguageResourceResult = DALFactory.GetDALObject<ILanguageDAL>().AddNew(ModelMapper.Instance.Mapper.Map<LanguageResource>(languageResourceModel), UserManager.GetLoggedInUser(), GetIP());
                if (addLanguageResourceResult.IsSuccess)
                {
                    languageResourceModel = ModelMapper.Instance.Mapper.Map<LanguageResourceModel>(addLanguageResourceResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", addLanguageResourceResult.ErrorMessage);
                }
            }
            return Json(new[] { languageResourceModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageLanguageResourcesEditingPopup_Update([DataSourceRequest] DataSourceRequest request, LanguageResourceModel languageResourceModel)
        {
            if (languageResourceModel != null && ModelState.IsValid)
            {
                var updateLanguageResourcesResult = DALFactory.GetDALObject<ILanguageDAL>().Update(ModelMapper.Instance.Mapper.Map<LanguageResource>(languageResourceModel), UserManager.GetLoggedInUser(), GetIP());
                if (updateLanguageResourcesResult.IsSuccess)
                {
                    languageResourceModel = ModelMapper.Instance.Mapper.Map<LanguageResourceModel>(updateLanguageResourcesResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", updateLanguageResourcesResult.ErrorMessage);
                }
            }

            return Json(new[] { languageResourceModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageLanguageResourcesEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, LanguageResourceModel languageResourceModel)
        {
            if (languageResourceModel != null)
            {
                languageResourceModel.Deleted = true;
                var updateLanguageResourcesResult = DALFactory.GetDALObject<ILanguageDAL>().Update
                    (ModelMapper.Instance.Mapper.Map<LanguageResource>(languageResourceModel), UserManager.GetLoggedInUser(), GetIP());
                if (!updateLanguageResourcesResult.IsSuccess)
                {
                    ModelState.AddModelError("", updateLanguageResourcesResult.ErrorMessage);
                }
            }

            return Json(new[] { languageResourceModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public FileStreamResult ExportLanguageResourcesGrid(string columnSettings, string selectedOptions)
        {
            var exportData = ModelMapper.Instance.Mapper.Map<IList<LanguageResourceModel>>(DALFactory.GetDALObject<ILanguageDAL>().GetAll(false).Result);
            return ExportGrid<LanguageResourceModel>(exportData, columnSettings, selectedOptions, Resources.HM_Languages);
        }

        [HttpPost]
        public ActionResult RefreshLanguageCache()
        {
            Resources.RefreshResourceCache();
            CacheManager.RemoveFromCache(UIExtensions.LanguageMetaCacheKey);
            var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllLanguageMetas();
            if (result.IsSuccess)
            {
                CacheManager.AddToCache(UIExtensions.LanguageMetaCacheKey, result.Result);
            }
            //ToDo
            return View("Languages");
        }
        #endregion

        //#region Differentials
        //[HttpGet]
        //public ActionResult ManageDifferentials()
        //{
        //    PopulateStores();
        //    PopulateYearValues();
        //    PopulateWeakNumbersValues();
        //    return View();
        //}

        //public ActionResult ManageDifferentialsEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        //{
        //    var getAllDifferentialsResult = DALFactory.GetDALObject<IDifferentialDAL>().GetAll(showDeleted.GetValueOrDefault());
        //    if (getAllDifferentialsResult.IsSuccess)
        //    {
        //        SetUpFilters<DifferentialModel>(request);
        //        var differentialModels = ModelMapper.Instance.Mapper.Map<IList<DifferentialModel>>(getAllDifferentialsResult.Result);
        //        return Json(differentialModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", getAllDifferentialsResult.ErrorMessage);
        //        return Json(new DifferentialModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDifferentialsEditingPopup_Create([DataSourceRequest] DataSourceRequest request, DifferentialModel differentialModel)
        //{
        //    PreProcessDifferentialModelState(differentialModel);
        //    if (differentialModel != null && ModelState.IsValid)
        //    {
        //        var addDifferentialResult = DALFactory.GetDALObject<IDifferentialDAL>().AddNew(ModelMapper.Instance.Mapper.Map<Differential>(differentialModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addDifferentialResult.IsSuccess)
        //        {
        //            differentialModel = ModelMapper.Instance.Mapper.Map<DifferentialModel>(addDifferentialResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addDifferentialResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { differentialModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDifferentialsEditingPopup_Update([DataSourceRequest] DataSourceRequest request, DifferentialModel differentialModel)
        //{
        //    PreProcessDifferentialModelState(differentialModel);
        //    if (differentialModel != null && ModelState.IsValid)
        //    {
        //        var updateDifferentialsResult = DALFactory.GetDALObject<IDifferentialDAL>().Update(ModelMapper.Instance.Mapper.Map<Differential>(differentialModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateDifferentialsResult.IsSuccess)
        //        {
        //            differentialModel = ModelMapper.Instance.Mapper.Map<DifferentialModel>(updateDifferentialsResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateDifferentialsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { differentialModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDifferentialsEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, DifferentialModel differentialModel)
        //{
        //    if (differentialModel != null)
        //    {
        //        differentialModel.Deleted = true;
        //        var updateDifferentialsResult = DALFactory.GetDALObject<IDifferentialDAL>().DeleteRecord(differentialModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateDifferentialsResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateDifferentialsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { differentialModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportDifferentialsGrid(string columnSettings, string selectedOptions)
        //{
        //    var exportData = ModelMapper.Instance.Mapper.Map<IList<DifferentialModel>>(DALFactory.GetDALObject<IDifferentialDAL>().GetAll(false).Result);
        //    return ExportGrid<DifferentialModel>(exportData, columnSettings, selectedOptions, Resources.HM_Periods);
        //}

        //private void PreProcessDifferentialModelState(DifferentialModel differentialModel)
        //{
        //    //if (string.IsNullOrWhiteSpace(differentialModel.FirstName) && string.IsNullOrWhiteSpace(differentialModel.CompanyName))
        //    //{
        //    //    this.ModelState.AddModelError("", Resources.M_EitherFirstNameOrCompanyIsRequired);
        //    //}
        //}
        //#endregion

        #region ManageBackup
        public ActionResult ManageBackup()
        {
            BackupModel backupModel = new BackupModel();
            var getLastDBBackupDetail = DALFactory.GetDALObject<IBackupDAL>().GetLastDBBackupDetail();
            if (getLastDBBackupDetail.IsSuccess)
            {
                var result = getLastDBBackupDetail.GetSingleResult();
                backupModel.BackupUserName = string.Format("{0} {1}", result.User.FirstName, result.User.Surname);
                backupModel.BackupFileName = result.FileName;
            }
            return View(backupModel);
        }


        [HttpPost]
        public ActionResult CreateNewBackup()
        {
            BackupModel backupModel = new BackupModel();
            var createBackupResult = DALFactory.GetDALObject<IBackupDAL>().CreateNewBackup(UserManager.GetLoggedInUser().ID, Server.MapPath("~/DBBackups"));
            if (createBackupResult.IsSuccess)
            {
                var result = createBackupResult.GetSingleResult();
                backupModel.BackupUserName = string.Format("{0} {1}", result.User.FirstName, result.User.Surname);
                backupModel.BackupFileName = result.FileName;
            }
            return Json(backupModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult DownloadBackup(string filePath)
        {
            string fullPath = Path.Combine(Server.MapPath("~/DBBackups"), filePath);
            if (!System.IO.File.Exists(fullPath))
            {
                return HttpNotFound();
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
        #endregion

        #region EmailServices
        [HttpGet]
        public ActionResult ManageEmailServices()
        {
            PopulateEnabledDisabledValues();
           // PopulateEmailConditions();
            PopulateTimeIntervales();
         //   PopulateEmailDataDates();
            return View();
        }

        public ActionResult ManageEmailServicesEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        {
            var getAllEmailServicesResult = DALFactory.GetDALObject<IEmailServiceDAL>().GetAll(showDeleted.GetValueOrDefault());
            if (getAllEmailServicesResult.IsSuccess)
            {
                SetUpFilters<EmailServiceItemModel>(request);
                var emailServiceItemModels = ModelMapper.Instance.Mapper.Map<IList<EmailServiceItemModel>>(getAllEmailServicesResult.Result);
                return Json(emailServiceItemModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("", getAllEmailServicesResult.ErrorMessage);
                return Json(new EmailServiceItemModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageEmailServicesEditingPopup_Create([DataSourceRequest] DataSourceRequest request, EmailServiceItemModel emailServiceItemModel)
        {
            PreProcessEmailServiceItemModelState(emailServiceItemModel);
            if (emailServiceItemModel != null && ModelState.IsValid)
            {
                var addEmailServiceResult = DALFactory.GetDALObject<IEmailServiceDAL>().AddNew(ModelMapper.Instance.Mapper.Map<EmailServiceItem>(emailServiceItemModel), UserManager.GetLoggedInUser(), GetIP());
                if (addEmailServiceResult.IsSuccess)
                {
                    emailServiceItemModel = ModelMapper.Instance.Mapper.Map<EmailServiceItemModel>(addEmailServiceResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", addEmailServiceResult.ErrorMessage);
                }
            }
            return Json(new[] { emailServiceItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageEmailServicesEditingPopup_Update([DataSourceRequest] DataSourceRequest request, EmailServiceItemModel emailServiceItemModel)
        {
            PreProcessEmailServiceItemModelState(emailServiceItemModel);
            if (emailServiceItemModel != null && ModelState.IsValid)
            {
                var updateEmailServicesResult = DALFactory.GetDALObject<IEmailServiceDAL>().Update(ModelMapper.Instance.Mapper.Map<EmailServiceItem>(emailServiceItemModel), UserManager.GetLoggedInUser(), GetIP());
                if (updateEmailServicesResult.IsSuccess)
                {
                    emailServiceItemModel = ModelMapper.Instance.Mapper.Map<EmailServiceItemModel>(updateEmailServicesResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", updateEmailServicesResult.ErrorMessage);
                }
            }

            return Json(new[] { emailServiceItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageEmailServicesEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, EmailServiceItemModel emailServiceItemModel)
        {
            if (emailServiceItemModel != null)
            {
                emailServiceItemModel.Deleted = true;
                var updateEmailServicesResult = DALFactory.GetDALObject<IEmailServiceDAL>().DeleteRecord(emailServiceItemModel.ID, UserManager.GetLoggedInUser(), GetIP());
                if (!updateEmailServicesResult.IsSuccess)
                {
                    ModelState.AddModelError("", updateEmailServicesResult.ErrorMessage);
                }
            }

            return Json(new[] { emailServiceItemModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public FileStreamResult ExportEmailServicesGrid(string columnSettings, string selectedOptions)
        {
            var exportData = ModelMapper.Instance.Mapper.Map<IList<EmailServiceItemModel>>(DALFactory.GetDALObject<IEmailServiceDAL>().GetAll(false).Result);
            return ExportGrid<EmailServiceItemModel>(exportData, columnSettings, selectedOptions, Resources.HM_EmailServices);
        }

        private void PreProcessEmailServiceItemModelState(EmailServiceItemModel emailServiceItemModel)
        {
            ModelState.Remove("RecipientEmail");
            if (emailServiceItemModel.SendCondition == 1)
            {
                if (string.IsNullOrEmpty(emailServiceItemModel.SendTime))
                {
                    ModelState.AddModelError("SendTime", string.Format(Resources.M_FieldRequired, Resources.SendTime));
                }
                if (emailServiceItemModel.DataDate == null)
                {
                    ModelState.AddModelError("DataDate", string.Format(Resources.M_FieldRequired, Resources.DataDate));
                }
            }
        }

        [HttpPost]
        public virtual ActionResult SendTestEmail(string server, string username, string password, int port, bool isSSL, string recipientEmailAddress)
        {
            if (EmailServiceHelper.SendTestEmail(server, username, password, port, isSSL, recipientEmailAddress))
            {
                return Json(new { messsage = Resources.M_EmailSentSuccessfully });
            }
            else
            {
                return Json(new { messsage = Resources.M_InvalidEmailCredentials });
            }
        }
        #endregion

        [HttpGet]
        public ActionResult ShowVariables()
        {
            return View();
        }

  }
}