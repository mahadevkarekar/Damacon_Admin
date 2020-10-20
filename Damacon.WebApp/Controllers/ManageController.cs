using Damacon.DAL;
using Damacon.DAL.Operations.Contracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Damacon.WebApp.Models;
using Damacon.WebApp.Helpers;
using System.Collections.Generic;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using System;
using System.Linq;
using Damacon.SharedWeb.Helpers;

namespace Damacon.WebApp.Controllers
{
    public class ManageController : BaseController
    {
        #region Users
        [HttpGet]
        public ActionResult ManageUsers()
        {
            PopulateUserTypes();
            PopulateCountries();
       //    PopulateStores();
            PopulateEnabledDisabledValues();
            return View();
        }

        public ActionResult ManageUsersEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        {
            var getAllUsersResult = DALFactory.GetDALObject<IUserDAL>().GetAll(showDeleted.GetValueOrDefault());
            if (getAllUsersResult.IsSuccess)
            {
                SetUpFilters<UserModel>(request);
                var userModels = ModelMapper.Instance.Mapper.Map<IList<UserModel>>(getAllUsersResult.Result);
                return Json(userModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("", getAllUsersResult.ErrorMessage);
                return Json(new UserModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageUsersEditingPopup_Create([DataSourceRequest] DataSourceRequest request, UserModel userModel)
        {
            PreProcessUserModelState(userModel);
            if (userModel != null && ModelState.IsValid)
            {
                if (userModel.UserTypeID != 3)
                {
                    userModel.StoreID = null;
                }
                var adduserResult = DALFactory.GetDALObject<IUserDAL>().AddNew(ModelMapper.Instance.Mapper.Map<User>(userModel), UserManager.GetLoggedInUser(), GetIP());
                if (adduserResult.IsSuccess)
                {
                    userModel = ModelMapper.Instance.Mapper.Map<UserModel>(adduserResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", adduserResult.ErrorMessage);
                }
            }
            return Json(new[] { userModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageUsersEditingPopup_Update([DataSourceRequest] DataSourceRequest request, UserModel userModel)
        {
            PreProcessUserModelState(userModel);
            if (userModel != null && ModelState.IsValid)
            {
                if (userModel.UserTypeID != 3)
                {
                    userModel.StoreID = null;
                }
                var updateUsersResult = DALFactory.GetDALObject<IUserDAL>().Update(ModelMapper.Instance.Mapper.Map<User>(userModel), UserManager.GetLoggedInUser(), GetIP());
                if (updateUsersResult.IsSuccess)
                {
                    userModel = ModelMapper.Instance.Mapper.Map<UserModel>(updateUsersResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", updateUsersResult.ErrorMessage);
                }
            }

            return Json(new[] { userModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageUsersEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, UserModel userModel)
        {
            PreProcessUserModelState(userModel);
            if (userModel != null)
            {
                var updateUsersResult = DALFactory.GetDALObject<IUserDAL>().DeleteRecord(userModel.ID, UserManager.GetLoggedInUser(), GetIP());
                if (!updateUsersResult.IsSuccess)
                {
                    ModelState.AddModelError("", updateUsersResult.ErrorMessage);
                }
            }

            return Json(new[] { userModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public FileStreamResult ExportUsersGrid(string columnSettings, string selectedOptions)
        {
            var exportData = ModelMapper.Instance.Mapper.Map<IList<UserModel>>(DALFactory.GetDALObject<IUserDAL>().GetAll(false).Result);
            return ExportGrid<UserModel>(exportData, columnSettings, selectedOptions, Resources.HM_Users);
        }

        [HttpPost]
        public ActionResult GetLoginPages(int userTypeID)
        {
            return Json(DALFactory.GetDALObject<IUserDAL>().GetAllUserTypeLoginPages(userTypeID).Result, JsonRequestBehavior.AllowGet);
        }

        private void PreProcessUserModelState(UserModel userModel)
        {
            if (userModel.UserTypeID != 3)
            {
                this.ModelState.Remove("StoreID");
            }

            if (userModel.UserTypeID == 5 && UserManager.GetLoggedInUser().UserType.ID != 5)
            {
                this.ModelState.AddModelError("", Resources.M_UnableToCreateDeveloperUser);
            }
            if ((userModel.UserTypeID == 1)
                && (UserManager.GetLoggedInUser().UserType.ID != 1 && UserManager.GetLoggedInUser().UserType.ID != 5))
            {
                this.ModelState.AddModelError("", Resources.M_UnableToCreateDeveloperUser);
            }
        }
        #endregion

       

      

        //#region DocumentTypes
        //[HttpGet]
        //public ActionResult ManageDocumentTypes()
        //{
        //    PopulateEnabledDisabledValues();
        //    return View();
        //}

        //public ActionResult ManageDocumentTypesEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        //{
        //    var getAllDocumentTypesResult = DALFactory.GetDALObject<IDocumentTypeDAL>().GetAll(showDeleted.GetValueOrDefault());
        //    if (getAllDocumentTypesResult.IsSuccess)
        //    {
        //        SetUpFilters<DocumentTypeModel>(request);
        //        var documentTypeModels = ModelMapper.Instance.Mapper.Map<IList<DocumentTypeModel>>(getAllDocumentTypesResult.Result);
        //        return Json(documentTypeModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", getAllDocumentTypesResult.ErrorMessage);
        //        return Json(new DocumentTypeModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentTypesEditingPopup_Create([DataSourceRequest] DataSourceRequest request, DocumentTypeModel documentTypeModel)
        //{
        //    if (documentTypeModel != null && ModelState.IsValid)
        //    {
        //        var addDocumentTypeResult = DALFactory.GetDALObject<IDocumentTypeDAL>().AddNew(ModelMapper.Instance.Mapper.Map<DocumentType>(documentTypeModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addDocumentTypeResult.IsSuccess)
        //        {
        //            documentTypeModel = ModelMapper.Instance.Mapper.Map<DocumentTypeModel>(addDocumentTypeResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addDocumentTypeResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { documentTypeModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentTypesEditingPopup_Update([DataSourceRequest] DataSourceRequest request, DocumentTypeModel documentTypeModel)
        //{
        //    if (documentTypeModel != null && ModelState.IsValid)
        //    {
        //        var updateDocumentTypesResult = DALFactory.GetDALObject<IDocumentTypeDAL>().Update(ModelMapper.Instance.Mapper.Map<DocumentType>(documentTypeModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateDocumentTypesResult.IsSuccess)
        //        {
        //            documentTypeModel = ModelMapper.Instance.Mapper.Map<DocumentTypeModel>(updateDocumentTypesResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateDocumentTypesResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentTypeModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentTypesEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, DocumentTypeModel documentTypeModel)
        //{
        //    if (documentTypeModel != null)
        //    {
        //        documentTypeModel.Deleted = true;
        //        var updateDocumentTypesResult = DALFactory.GetDALObject<IDocumentTypeDAL>().DeleteRecord(documentTypeModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateDocumentTypesResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateDocumentTypesResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentTypeModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportDocumentTypesGrid(string columnSettings, string selectedOptions)
        //{
        //    var exportData = ModelMapper.Instance.Mapper.Map<IList<DocumentTypeModel>>(DALFactory.GetDALObject<IDocumentTypeDAL>().GetAll(false).Result);
        //    return ExportGrid<DocumentTypeModel>(exportData, columnSettings, selectedOptions, Resources.HM_Documents);
        //}

        //#endregion

        
    }
}