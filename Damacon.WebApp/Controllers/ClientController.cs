using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.DAL.Utils;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Export.Excel;
using Damacon.WebApp.Export.PDF;
using Damacon.WebApp.Helpers;
using Damacon.WebApp.Models;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Damacon.WebApp.Controllers
{
    public class ClientController : BaseController
    {
        #region Clients
        [HttpGet]
        public ActionResult ManageClients()
        {
            PopulateCountries();
            PopulateEnabledDisabledValues();
            PopulateYesNoValues(); 
            return View();
        }

        public ActionResult ManageClientsEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        {
            DateTime? birthdayFromDate = null, birthdayToDate = null;
            if (request.Filters.Count > 0)
            {
                FilterDescriptor allFilter = ((FilterDescriptor)request.Filters[0]);
                string[] splitValues = allFilter.Value.ToString().Split(new string[] { "####" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitValues.Length > 1)
                {
                    var filterData = JsonConvert.DeserializeObject<FilterData>(HttpUtility.UrlDecode(splitValues[0]));
                    birthdayFromDate = GetDateFromFilter(filterData, "BirthdayFromDate", "dd MMMM yyyy", true);
                    birthdayToDate = GetDateFromFilter(filterData, "BirthdayToDate", "dd MMMM yyyy", true);
                }
            }
            var filtersNew = SetUpFilters<ClientModel>(request, new List<string> { "RegisteredByStoreName", "UserName" });
            var getAllClientsResultNew = DALFactory.GetDALObject<IClientDAL>().GetAll(request, birthdayFromDate, birthdayToDate, showDeleted.GetValueOrDefault());
            if (getAllClientsResultNew.IsSuccess)
            {
                DataSourceResult dataSourceResult = getAllClientsResultNew.GetSingleResult();
                dataSourceResult.Data = ModelMapper.Instance.Mapper.Map<IList<ClientModel>>(dataSourceResult.Data);
                return Json(dataSourceResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("", getAllClientsResultNew.ErrorMessage);
                return Json(new ClientModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageClientsEditingPopup_Create([DataSourceRequest] DataSourceRequest request, ClientModel clientModel)
        {
            if (clientModel != null && ModelState.IsValid)
            {
                var addclientResult = DALFactory.GetDALObject<IClientDAL>().AddNew(ModelMapper.Instance.Mapper.Map<Client>(clientModel), UserManager.GetLoggedInUser(), GetIP());
                if (addclientResult.IsSuccess)
                {
                    clientModel = ModelMapper.Instance.Mapper.Map<ClientModel>(addclientResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", addclientResult.ErrorMessage);
                }
            }
            return Json(new[] { clientModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageClientsEditingPopup_Update([DataSourceRequest] DataSourceRequest request, ClientModel clientModel)
        {
            if (clientModel != null && ModelState.IsValid)
            {
                var updateClientsResult = DALFactory.GetDALObject<IClientDAL>().Update(ModelMapper.Instance.Mapper.Map<Client>(clientModel), UserManager.GetLoggedInUser(), GetIP());
                if (updateClientsResult.IsSuccess)
                {
                    clientModel = ModelMapper.Instance.Mapper.Map<ClientModel>(updateClientsResult.GetSingleResult());
                }
                else
                {
                    ModelState.AddModelError("", updateClientsResult.ErrorMessage);
                }
            }

            return Json(new[] { clientModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageClientsEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, ClientModel clientModel)
        {
            if (clientModel != null)
            {
                clientModel.Deleted = true;
                var updateClientsResult = DALFactory.GetDALObject<IClientDAL>().DeleteRecord(clientModel.ID, UserManager.GetLoggedInUser(), GetIP());
                if (!updateClientsResult.IsSuccess)
                {
                    ModelState.AddModelError("", updateClientsResult.ErrorMessage);
                }
            }

            return Json(new[] { clientModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public FileStreamResult ExportClientsGrid(string columnSettings, string selectedOptions)
        {
            var exportData = DALFactory.GetDALObject<IClientDAL>().GetAll(false).Result;

            IList<KeyValuePair<string, string>> exportFilters = new List<KeyValuePair<string, string>>();
            dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
            var filterData = JsonConvert.DeserializeObject<FilterData>(options.exportFilters.ToString());
            exportData = ProcessClientGridDateFilters(exportData, filterData, exportFilters);

            return ExportGrid<ClientModel>(ModelMapper.Instance.Mapper.Map<IList<ClientModel>>(exportData), columnSettings, selectedOptions, Resources.HM_Clients, exportFilters);
        }

        private List<Client> ProcessClientGridDateFilters(List<Client> data, FilterData filters, IList<KeyValuePair<string, string>> exportFilters = null)
        {
            NameValue birthdayFromDate = filters.ColumnFilters.FirstOrDefault(x => x.Value == "BirthdayFromDate" && !string.IsNullOrEmpty(x.Text));
            if (birthdayFromDate != null)
            {
                var birthdayFromDateParsed = DateTime.ParseExact(birthdayFromDate.Text + " 2016", "dd MMMM yyyy", CultureInfo.CurrentCulture);
                data = data.Where(x => x.DateBirth == null || x.DateBirth.Value.Month > birthdayFromDateParsed.Month ||
                (x.DateBirth.Value.Month == birthdayFromDateParsed.Month && x.DateBirth.Value.Day >= birthdayFromDateParsed.Day)).ToList();

                if (exportFilters != null)
                {
                    exportFilters.Add(new KeyValuePair<string, string>(Resources.BirthdayFrom, birthdayFromDate.Text));
                }
            }

            NameValue birthdayToDate = filters.ColumnFilters.FirstOrDefault(x => x.Value == "BirthdayToDate" && !string.IsNullOrEmpty(x.Text));
            if (birthdayToDate != null)
            {
                var birthdayToDateParsed = DateTime.ParseExact(birthdayToDate.Text + " 2016", "dd MMMM yyyy", CultureInfo.CurrentCulture);
                data = data.Where(x => x.DateBirth == null || x.DateBirth.Value.Month < birthdayToDateParsed.Month ||
               (x.DateBirth.Value.Month == birthdayToDateParsed.Month && x.DateBirth.Value.Day <= birthdayToDateParsed.Day)).ToList();
                if (exportFilters != null)
                {
                    exportFilters.Add(new KeyValuePair<string, string>(Resources.BirthdayTo, birthdayToDate.Text));
                }
            }

            return data;
        }

        //[HttpPost]
        //public ActionResult PrintAndEmailClientLoginDetail(long clientID, bool isEmail, bool isPrint)
        //{
        //    var result = ClientPrintAndEmail.ProcessClientLoginPrintAndEmail(clientID, isEmail, isPrint, SaveDocument);
        //    return ConvertToJsonResult(result);
        //}

        //[HttpPost]
        //public ActionResult PrintAndEmailClientBalanceDetail(long clientID, bool isEmail, bool isPrint)
        //{
        //    var result = ClientPrintAndEmail.ProcessClientBalancePrintAndEmail(clientID, isEmail, isPrint, SaveDocument);
        //    return ConvertToJsonResult(result);
        //}

        #endregion

        //#region Client Shopping

        //[HttpGet]
        //public ActionResult GetClientShoppingByClient(long clientID)
        //{
        //    PopulateStoresForSalesData();
        //    var clientData = DALFactory.GetDALObject<IClientDAL>().GetById(clientID);
        //    string clientName = "";
        //    if (clientData.IsSuccess)
        //    {
        //        clientName = ModelMapper.Instance.Mapper.Map<ClientModel>(clientData.GetSingleResult()).FullName;
        //    }
        //    return PartialView("EditorTemplates/AddEditClientShopping", new ClientShoppingModel() { ClientID = clientID, ClientName = clientName, ReceiptDate = DateTime.Now });
        //}

        //[HttpGet]
        //public ActionResult GetClientShoppingByID(long id)
        //{
        //    PopulateStoresForSalesData();
        //    var data = DALFactory.GetDALObject<IClientShoppingDAL>().GetById(id);
        //    if (data.IsSuccess)
        //    {
        //        var model = ModelMapper.Instance.Mapper.Map<ClientShoppingModel>(data.GetSingleResult());
        //        model.ToPay = (model.ReceiptAmount ?? 0) - (model.BonusUsed ?? 0);
        //        return PartialView("EditorTemplates/AddEditClientShopping", model);
        //    }

        //    return new HttpStatusCodeResult(400);
        //}

        //[HttpGet]
        //public ActionResult GetClientShoppingByClientAndStore(long clientID, long storeID, long clientShoppingID)
        //{
        //    var metaData = DALFactory.GetDALObject<IClientShoppingDAL>().GetMetaDataByClientAndStore(clientID, storeID, clientShoppingID);
        //    if (metaData != null)
        //    {
        //        return Json(new { IsSuccess = true, Result = metaData }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { IsSuccess = false, ErrorMessage = Resources.M_InternalServerError }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult CreateUpdateClientShopping(ClientShoppingModel clientShoppingModel)
        //{
        //    if (clientShoppingModel.ClientID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.Client));
        //    }
        //    if (clientShoppingModel.StoreID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Store));
        //    }
        //    if (clientShoppingModel.BrandID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Brand));
        //    }
        //    if ((clientShoppingModel.ReceiptAmount == null || clientShoppingModel.ReceiptAmount.Value == 0) &&
        //        (clientShoppingModel.ExtraBonus == null || clientShoppingModel.ExtraBonus.Value == 0))
        //    {
        //        ModelState.AddModelError("", Resources.M_EitherReceiptOrExtraBonusIsRequired);
        //    }

        //    if (clientShoppingModel.ReceiptAmount.HasValue && clientShoppingModel.BonusUsed.HasValue &&
        //        clientShoppingModel.ReceiptAmount < clientShoppingModel.BonusUsed.Value)
        //    {
        //        ModelState.AddModelError("", Resources.M_BonusUsedMoreThanRecieptAmountError);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (clientShoppingModel.ID == 0)
        //        {
        //            var addClientShoppingResult = DALFactory.GetDALObject<IClientShoppingDAL>().AddNew(ModelMapper.Instance.Mapper.Map<ClientShopping>(clientShoppingModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (addClientShoppingResult.IsSuccess)
        //            {
        //                var newClientShoppingModel = ModelMapper.Instance.Mapper.Map<ClientShoppingModel>(addClientShoppingResult.GetSingleResult());
        //                return Json(new { IsSuccess = true, Result = newClientShoppingModel }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", addClientShoppingResult.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            var updateClientShoppingResult = DALFactory.GetDALObject<IClientShoppingDAL>().Update(ModelMapper.Instance.Mapper.Map<ClientShopping>(clientShoppingModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (updateClientShoppingResult.IsSuccess)
        //            {
        //                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateClientShoppingResult.ErrorMessage);
        //            }
        //        }
        //    }
        //    return Json(new { IsSuccess = false, ErrorMessage = string.Join(Environment.NewLine, ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)) }, JsonRequestBehavior.AllowGet);
        //}


        //[HttpGet]
        //public ActionResult ManageBonus()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateBonusRecordTypes();
        //    return View();
        //}

        //public ActionResult ManageClientShoppingsEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        //{
        //    if (request.Filters.Count > 0)
        //    {
        //        var getAllClientShoppingsResult = DALFactory.GetDALObject<IClientShoppingDAL>().GetAll(showDeleted.GetValueOrDefault());
        //        if (getAllClientShoppingsResult.IsSuccess)
        //        {
        //            if (request.Filters.Count > 0)
        //            {
        //                FilterDescriptor allFilter = ((FilterDescriptor)request.Filters[0]);
        //                string[] splitValues = allFilter.Value.ToString().Split(new string[] { "####" }, StringSplitOptions.RemoveEmptyEntries);
        //                if (splitValues.Length > 1)
        //                {
        //                    var filterData = JsonConvert.DeserializeObject<FilterData>(HttpUtility.UrlDecode(splitValues[0]));
        //                    getAllClientShoppingsResult.Result = ProcessBonusGridDateFilters(getAllClientShoppingsResult.Result, filterData);
        //                }
        //            }

        //            var filters = SetUpFilters<ClientShoppingModel>(request);
        //            var clientModels = ModelMapper.Instance.Mapper.Map<IList<ClientShoppingModel>>(getAllClientShoppingsResult.Result.OrderByDescending(x => x.ID));
        //            return Json(clientModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", getAllClientShoppingsResult.ErrorMessage);
        //            return Json(new ClientShoppingModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new ClientShoppingModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageClientShoppingsEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, ClientShoppingModel clientShoppingModel)
        //{
        //    if (clientShoppingModel != null)
        //    {
        //        clientShoppingModel.Deleted = true;
        //        var updateClientsResult = DALFactory.GetDALObject<IClientShoppingDAL>().DeleteRecord(clientShoppingModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateClientsResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateClientsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { clientShoppingModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportClientShoppingsGrid(string columnSettings, string selectedOptions)
        //{
        //    var exportData = DALFactory.GetDALObject<IClientShoppingDAL>().GetAll(false).Result;

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    var filterData = JsonConvert.DeserializeObject<FilterData>(options.exportFilters.ToString());
        //    exportData = ProcessBonusGridDateFilters(exportData, filterData, filters);

        //    var exportDataModels = ModelMapper.Instance.Mapper.Map<IList<ClientShoppingModel>>(exportData);
        //    DataSourceRequest request = new DataSourceRequest();
        //    request.Filters = new List<IFilterDescriptor>();
        //    request.Filters.Add(new FilterDescriptor("All", FilterOperator.Contains, options.exportFilters + "####" + columnSettings));
        //    var exportFilters = SetUpFilters<ClientShopping>(request);

        //    if (filters != null)
        //    {
        //        exportFilters.AddRange(filters);
        //    }

        //    exportDataModels = exportDataModels.ToDataSourceResult(request).Data.Cast<ClientShoppingModel>().ToList();
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new Export.Excel.ExcelExportBonusManagement().Export(exportDataModels, Resources.HM_BonusManagement, exportFilters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new Export.PDF.PDFExportBonusManagement().Export(exportDataModels, Resources.HM_BonusManagement, exportFilters, Resources.Portrait);
        //    }
        //}

        //private List<ClientShopping> ProcessBonusGridDateFilters(List<ClientShopping> data, FilterData filters, IList<KeyValuePair<string, string>> exportFilters = null)
        //{
        //    NameValue receiptFromDateFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptFromDate" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptFromDateFilter != null)
        //    {
        //        DateTime receiptFromDateFilterParsed = DateTime.MinValue;
        //        if (DateTime.TryParseExact(receiptFromDateFilter.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out receiptFromDateFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptDate.Date >= receiptFromDateFilterParsed.Date).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptFromDate, receiptFromDateFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue receiptToDateFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptToDate" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptToDateFilter != null)
        //    {
        //        DateTime receiptToDateFilterFilterParsed = DateTime.MinValue;
        //        if (DateTime.TryParseExact(receiptToDateFilter.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out receiptToDateFilterFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptDate.Date <= receiptToDateFilterFilterParsed.Date).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptFromDate, receiptToDateFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue receiptAmountFromFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptAmountFrom" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptAmountFromFilter != null)
        //    {
        //        decimal receiptAmountFromFilterParsed = 0;
        //        if (decimal.TryParse(receiptAmountFromFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out receiptAmountFromFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptAmount.HasValue && x.ReceiptAmount.Value >= receiptAmountFromFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptAmountFrom, receiptAmountFromFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue receiptAmountToFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptAmountTo" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptAmountToFilter != null)
        //    {
        //        decimal receiptAmountToFilterParsed = 0;
        //        if (decimal.TryParse(receiptAmountToFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out receiptAmountToFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptAmount.HasValue && x.ReceiptAmount.Value <= receiptAmountToFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptAmountTo, receiptAmountToFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue currentBonusBalanceFromFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "CurrentBonusBalanceFrom" && !string.IsNullOrEmpty(x.Text));
        //    if (currentBonusBalanceFromFilter != null)
        //    {
        //        decimal currentBonusBalanceFromFilterParsed = 0;
        //        if (decimal.TryParse(currentBonusBalanceFromFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out currentBonusBalanceFromFilterParsed))
        //        {
        //            data = data.Where(x => x.BonusBalance.HasValue && x.BonusBalance.Value >= currentBonusBalanceFromFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.CurrentBonusBalanceFrom, currentBonusBalanceFromFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue currentBonusBalanceToFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "CurrentBonusBalanceTo" && !string.IsNullOrEmpty(x.Text));
        //    if (currentBonusBalanceToFilter != null)
        //    {
        //        decimal currentBonusBalanceToFilterParsed = 0;
        //        if (decimal.TryParse(currentBonusBalanceToFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out currentBonusBalanceToFilterParsed))
        //        {
        //            data = data.Where(x => x.BonusBalance.HasValue && x.BonusBalance.Value <= currentBonusBalanceToFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.CurrentBonusBalanceTo, currentBonusBalanceToFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue bonusRecordTypeFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "BonusRecordTypeID" && !string.IsNullOrEmpty(x.Text));
        //    if (bonusRecordTypeFilter != null)
        //    {
        //        if (bonusRecordTypeFilter.Text == Resources.BonusAdded)
        //        {
        //            data = data.Where(x => x.BonusAdded.HasValue).ToList();
        //        }
        //        else if (bonusRecordTypeFilter.Text == Resources.BonusUsed)
        //        {
        //            data = data.Where(x => x.BonusUsed.HasValue).ToList();
        //        }
        //        else if (bonusRecordTypeFilter.Text == Resources.ExtraBonus)
        //        {
        //            data = data.Where(x => x.ExtraBonus.HasValue).ToList();
        //        }
        //        else if (bonusRecordTypeFilter.Text == Resources.LastBalance)
        //        {
        //            data = data.Where(x => x.IsLastRecord).ToList();
        //        }

        //        if (exportFilters != null)
        //        {
        //            exportFilters.Add(new KeyValuePair<string, string>(Resources.RecordType, bonusRecordTypeFilter.Text));
        //        }
        //    }

        //    return data;
        //}

        //#endregion

        //#region Client Shopping New

        //[HttpGet]
        //public ActionResult AddShoppingDetailByClient(long clientID)
        //{
        //    PopulateStoresForSalesData();
        //    PopulateShoppingReturnReimbursementTypes();
        //    var clientData = DALFactory.GetDALObject<IClientDAL>().GetById(clientID);
        //    string clientName = "";
        //    int? clientCardCode = null;
        //    if (clientData.IsSuccess)
        //    {
        //        clientName = ModelMapper.Instance.Mapper.Map<ClientModel>(clientData.GetSingleResult()).FullName;
        //        clientCardCode = clientData.GetSingleResult().CardCode;
        //    }
        //    return PartialView("EditorTemplates/AddEditShoppingDetail", new ShoppingEditModel() { ClientID = clientID, ClientName = clientName, ReceiptDate = DateTime.Now, ClientCardCode = clientCardCode });
        //}

        //public ActionResult EditShoppingDetailByID(string id)
        //{
        //    long shoppingID = Convert.ToInt64(id.Replace("S_", ""));
        //    PopulateStoresForSalesData();
        //    PopulateShoppingReturnReimbursementTypes();
        //    var data = DALFactory.GetDALObject<IShoppingDetailDAL>().GetById(shoppingID);
        //    if (data.IsSuccess)
        //    {
        //        var model = ModelMapper.Instance.Mapper.Map<ShoppingEditModel>(data.GetSingleResult());
        //        model.IsLastRecord = data.GetSingleResult().IsLastClientRecord;
        //        return PartialView("EditorTemplates/AddEditShoppingDetail", model);
        //    }

        //    return new HttpStatusCodeResult(400);
        //}

        //public ActionResult EditShoppingReturnDetailByID(string id)
        //{
        //    long shoppingID = Convert.ToInt64(id.Replace("S_", ""));
        //    PopulateStoresForSalesData();
        //    PopulateShoppingReturnReimbursementTypes();
        //    var data = DALFactory.GetDALObject<IShoppingDetailDAL>().GetById(shoppingID);
        //    if (data.IsSuccess)
        //    {
        //        var model = ModelMapper.Instance.Mapper.Map<ShoppingEditModel>(data.GetSingleResult());
        //        model.IsReturnRecord = true;
        //        model.ReturnDate = DateTime.Now;
        //        model.ReturnType = "ReturnTypeBonus";
        //        model.ReturnAmount = model.ReceiptAmount;
        //        return PartialView("EditorTemplates/AddEditShoppingDetail", model);
        //    }

        //    return new HttpStatusCodeResult(400);
        //}

        //[HttpGet]
        //public ActionResult GetClientAvailableReceiptValueForBonus(long clientID, DateTime reciptDate)
        //{
        //    var metaData = DALFactory.GetDALObject<IShoppingDetailDAL>().GetClientAvailableReceiptValueForBonus(reciptDate, clientID);
        //    if (metaData != null)
        //    {
        //        return Json(new { IsSuccess = true, Result = metaData }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { IsSuccess = false, ErrorMessage = Resources.M_InternalServerError }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult CreateShoppingDetail(ShoppingEditModel shoppingEditModel)
        //{
        //    if (shoppingEditModel.ClientID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.Client));
        //    }
        //    if (shoppingEditModel.StoreID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Store));
        //    }
        //    if (shoppingEditModel.BrandID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Brand));
        //    }
        //    if ((shoppingEditModel.ReceiptAmount == null || shoppingEditModel.ReceiptAmount.Value == 0))
        //    {
        //        ModelState.AddModelError("", Resources.M_EitherReceiptOrExtraBonusIsRequired);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (shoppingEditModel.ID == 0)
        //        {
        //            var addShoppingDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().AddNew(ModelMapper.Instance.Mapper.Map<ShoppingDetail>(shoppingEditModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (addShoppingDetailResult.IsSuccess)
        //            {
        //                var newShoppingDetailModel = ModelMapper.Instance.Mapper.Map<ShoppingEditModel>(addShoppingDetailResult.GetSingleResult());

        //                if (shoppingEditModel.IsPrint || shoppingEditModel.IsSendEmail)
        //                {
        //                    var result = ClientPrintAndEmail.ProcessShoppingPrintAndEmail(shoppingEditModel.IsPrint, shoppingEditModel.IsSendEmail, newShoppingDetailModel.ID, SaveDocument);
        //                    return Json(new { IsSuccess = true, PrintData = result.PrintData, PrintError = result.PrintError, EmailError = result.EmailError }, JsonRequestBehavior.AllowGet);
        //                }

        //                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", addShoppingDetailResult.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            var updateClientShoppingResult = DALFactory.GetDALObject<IShoppingDetailDAL>().Update(ModelMapper.Instance.Mapper.Map<ShoppingDetail>(shoppingEditModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (updateClientShoppingResult.IsSuccess)
        //            {
        //                if (shoppingEditModel.IsPrint || shoppingEditModel.IsSendEmail)
        //                {
        //                    var result = ClientPrintAndEmail.ProcessShoppingPrintAndEmail(shoppingEditModel.IsPrint, shoppingEditModel.IsSendEmail, updateClientShoppingResult.GetSingleResult().ID, SaveDocument);
        //                    return Json(new { IsSuccess = true, PrintData = result.PrintData, PrintError = result.PrintError, EmailError = result.EmailError }, JsonRequestBehavior.AllowGet);
        //                }

        //                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateClientShoppingResult.ErrorMessage);
        //            }
        //        }
        //    }
        //    return Json(new { IsSuccess = false, ErrorMessage = string.Join(Environment.NewLine, ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)) }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult CreateVoucherDetail(VoucherEditModel voucherEditModel)
        //{
        //    if (voucherEditModel.ClientID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.Client));
        //    }
        //    if (voucherEditModel.StoreID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Store));
        //    }
        //    if (voucherEditModel.BrandID <= 0)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.HM_Brand));
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var addVoucherDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().AddBonusVoucher(ModelMapper.Instance.Mapper.Map<VoucherDetail>(voucherEditModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addVoucherDetailResult.IsSuccess)
        //        {
        //            if (voucherEditModel.IsPrint || voucherEditModel.IsSendEmail)
        //            {
        //                var result = ClientPrintAndEmail.ProcessVoucherPrintAndEmail(voucherEditModel.IsPrint, voucherEditModel.IsSendEmail, addVoucherDetailResult.GetSingleResult().ID, SaveDocument);
        //                return Json(new { IsSuccess = true, PrintData = result.PrintData, PrintError = result.PrintError, EmailError = result.EmailError }, JsonRequestBehavior.AllowGet);
        //            }

        //            return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addVoucherDetailResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new { IsSuccess = false, ErrorMessage = string.Join(Environment.NewLine, ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)) }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddEditShoppingDetail_GetVoucher(long clientID, DateTime receiptDate, string shoppingDetailID)
        //{
        //    long shoppingID = Convert.ToInt64(shoppingDetailID.Replace("S_", ""));
        //    var vouchers = DALFactory.GetDALObject<IShoppingDetailDAL>().GetClientAvailableVouchers(receiptDate, clientID, shoppingID);
        //    return Json(ModelMapper.Instance.Mapper.Map<List<VoucherEditModel>>(vouchers.Result), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult ManageBonusNew()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateShoppingRecordTypes();
        //    PopulateYesNoValues();
        //    return View();
        //}

        //public ActionResult ManageBonusNew_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
        //{
        //    DateTime? receiptFromDate = null;
        //    DateTime? receiptToDate = null;
        //    FilterData filterData = null;
        //    if (request.Filters.Count > 0)
        //    {
        //        FilterDescriptor allFilter = ((FilterDescriptor)request.Filters[0]);
        //        string[] splitValues = allFilter.Value.ToString().Split(new string[] { "####" }, StringSplitOptions.RemoveEmptyEntries);
        //        if (splitValues.Length > 1)
        //        {
        //            filterData = JsonConvert.DeserializeObject<FilterData>(HttpUtility.UrlDecode(splitValues[0]));
        //            receiptFromDate = GetDateFromFilter(filterData, "ReceiptFromDate", "dd/MM/yyyy", false);
        //            receiptToDate = GetDateFromFilter(filterData, "ReceiptToDate", "dd/MM/yyyy", false);
        //        }
        //        var getAllClientShoppingsResult = DALFactory.GetDALObject<IShoppingDetailDAL>().GetShoppingDetailView(showDeleted.GetValueOrDefault(), receiptFromDate ?? DateTime.MinValue, receiptToDate ?? DateTime.MaxValue);
        //        if (getAllClientShoppingsResult.IsSuccess)
        //        {
        //            if (filterData != null)
        //            {
        //                getAllClientShoppingsResult.Result = ProcessManageBonusNewGridDateFilters(getAllClientShoppingsResult.Result, filterData);
        //            }

        //            var filters = SetUpFilters<ShoppingDetailViewModel>(request);
        //            var clientModels = ModelMapper.Instance.Mapper.Map<IList<ShoppingDetailViewModel>>(getAllClientShoppingsResult.Result);
        //            return Json(clientModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", getAllClientShoppingsResult.ErrorMessage);
        //            return Json(new ShoppingDetailViewModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new ShoppingDetailViewModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //    }
        //}


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageBonusNew_Destroy([DataSourceRequest] DataSourceRequest request, ShoppingDetailViewModel shoppingDetailViewModel)
        //{
        //    if (shoppingDetailViewModel != null)
        //    {
        //        if (shoppingDetailViewModel.ID.StartsWith("S_"))
        //        {
        //            long shoppingDetailID = Convert.ToInt64(shoppingDetailViewModel.ID.Replace("S_", ""));
        //            GenericActionResult deleteShoppingDetailResult = null;
        //            if(shoppingDetailViewModel.IsReturnRecord)
        //            {
        //                deleteShoppingDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().DeleteReturnRecord(shoppingDetailID);
        //            }
        //            else
        //            {
        //                deleteShoppingDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().DeleteRecord(shoppingDetailID, UserManager.GetLoggedInUser(), GetIP());
        //            }
                    
        //            if (!deleteShoppingDetailResult.IsSuccess)
        //            {
        //                ModelState.AddModelError("", deleteShoppingDetailResult.ErrorMessage);
        //            }
        //            else
        //            {
        //                shoppingDetailViewModel.Deleted = true;
        //            }
        //        }
        //        else
        //        {
        //            long voucherID = Convert.ToInt64(shoppingDetailViewModel.ID.Replace("V_", ""));
        //            var deleteVoucherDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().DeleteBonusVoucher(voucherID);
        //            if (!deleteVoucherDetailResult.IsSuccess)
        //            {
        //                ModelState.AddModelError("", deleteVoucherDetailResult.ErrorMessage);
        //            }
        //            else
        //            {
        //                shoppingDetailViewModel.Deleted = true;
        //            }
        //        }
        //    }

        //    return Json(new[] { shoppingDetailViewModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportShoppingDetailViewGrid(string columnSettings, string selectedOptions)
        //{
        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    var filterData = JsonConvert.DeserializeObject<FilterData>(options.exportFilters.ToString());

        //    DateTime? receiptFromDate = GetDateFromFilter(filterData, "ReceiptFromDate", "dd/MM/yyyy", false);
        //    DateTime? receiptToDate = GetDateFromFilter(filterData, "ReceiptToDate", "dd/MM/yyyy", false);

        //    var exportData = DALFactory.GetDALObject<IShoppingDetailDAL>().GetShoppingDetailView(false, receiptFromDate ?? DateTime.MinValue, receiptToDate ?? DateTime.MaxValue).Result;

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    exportData = ProcessManageBonusNewGridDateFilters(exportData, filterData, filters);

        //    var exportDataModels = ModelMapper.Instance.Mapper.Map<IList<ShoppingDetailViewModel>>(exportData);
        //    DataSourceRequest request = new DataSourceRequest();
        //    request.Filters = new List<IFilterDescriptor>();
        //    request.Filters.Add(new FilterDescriptor("All", FilterOperator.Contains, options.exportFilters + "####" + columnSettings));
        //    var sortDataList = JsonConvert.DeserializeObject<List<SortData>>(options.sort.ToString());
        //    request.Sorts = ConvertToSortDescriptor(sortDataList);
        //    var exportFilters = SetUpFilters<ShoppingDetailViewModel>(request);

        //    if (receiptFromDate.HasValue)
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptFromDate, receiptFromDate.Value.ToString(UIExtensions.DateFormat)));
        //    }
        //    if (receiptToDate.HasValue)
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptToDate, receiptToDate.Value.ToString(UIExtensions.DateFormat)));
        //    }

        //    if (filters != null)
        //    {
        //        exportFilters.AddRange(filters);
        //    }

        //    exportDataModels = exportDataModels.ToDataSourceResult(request).Data.Cast<ShoppingDetailViewModel>().ToList();
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new ExcelExportBonusManagementNew().Export(exportDataModels, Resources.HM_BonusManagementNew, exportFilters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new PDFExportBonusManagementNew().Export(exportDataModels, Resources.HM_BonusManagementNew, exportFilters, Resources.Landscape);
        //    }
        //}

        //private List<ShoppingDetailView> ProcessManageBonusNewGridDateFilters(List<ShoppingDetailView> data, FilterData filters, IList<KeyValuePair<string, string>> exportFilters = null)
        //{
        //    NameValue expireDateFromFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ExpireDateFrom" && !string.IsNullOrEmpty(x.Text));
        //    if (expireDateFromFilter != null)
        //    {
        //        DateTime expireDateFromFilterParsed = DateTime.MinValue;
        //        if (DateTime.TryParseExact(expireDateFromFilter.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out expireDateFromFilterParsed))
        //        {
        //            data = data.Where(x => x.ExpiryDate.Date >= expireDateFromFilterParsed.Date).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ExpireDateFrom, expireDateFromFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue expireDateToFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ExpireDateTo" && !string.IsNullOrEmpty(x.Text));
        //    if (expireDateToFilter != null)
        //    {
        //        DateTime expireDateToFilterParsed = DateTime.MinValue;
        //        if (DateTime.TryParseExact(expireDateToFilter.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out expireDateToFilterParsed))
        //        {
        //            data = data.Where(x => x.ExpiryDate.Date <= expireDateToFilterParsed.Date).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ExpireDateTo, expireDateToFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue receiptAmountFromFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptAmountFrom" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptAmountFromFilter != null)
        //    {
        //        decimal receiptAmountFromFilterParsed = 0;
        //        if (decimal.TryParse(receiptAmountFromFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out receiptAmountFromFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptAmount >= receiptAmountFromFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptAmountFrom, receiptAmountFromFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue receiptAmountToFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ReceiptAmountTo" && !string.IsNullOrEmpty(x.Text));
        //    if (receiptAmountToFilter != null)
        //    {
        //        decimal receiptAmountToFilterParsed = 0;
        //        if (decimal.TryParse(receiptAmountToFilter.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out receiptAmountToFilterParsed))
        //        {
        //            data = data.Where(x => x.ReceiptAmount <= receiptAmountToFilterParsed).ToList();

        //            if (exportFilters != null)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.ReceiptAmountTo, receiptAmountToFilter.Text));
        //            }
        //        }
        //    }

        //    NameValue shoppingRecordTypeFilter = filters.ColumnFilters.FirstOrDefault(x => x.Value == "ShoppingRecordTypeID" && !string.IsNullOrEmpty(x.Text));
        //    if (shoppingRecordTypeFilter != null)
        //    {
        //        var shoppingRecordTypes = shoppingRecordTypeFilter.Text.Split(',');
        //        data = data.Where(x => shoppingRecordTypes.Any(srt => srt == x.RecordType)).ToList();
        //        if (exportFilters != null)
        //        {
        //            if (shoppingRecordTypes.Count() == 4)
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.RecordType, "All"));
        //            }
        //            else
        //            {
        //                exportFilters.Add(new KeyValuePair<string, string>(Resources.RecordType, shoppingRecordTypeFilter.Text));
        //            }
        //        }
        //    }

        //    return data;
        //}

        //public ActionResult EditExpiryDate(string id, DateTime currentDate)
        //{
        //    return PartialView("EditorTemplates/EditExpiryDate", new EditExpiryDateModel() { ID = id, ExpiryDate = currentDate });
        //}

        //[HttpPost]
        //public ActionResult UpdateExpiryDate(EditExpiryDateModel editExpiryDateModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if(editExpiryDateModel.ID.StartsWith("S", StringComparison.OrdinalIgnoreCase))
        //        {
        //            long shoppingID = Convert.ToInt64(editExpiryDateModel.ID.Substring(2));
        //            var updateShoppingDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().UpdateShoppingExpiryDate(shoppingID, editExpiryDateModel.ExpiryDate, UserManager.GetLoggedInUser(), GetIP());
        //            if (updateShoppingDetailResult.IsSuccess)
        //            {
        //                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateShoppingDetailResult.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            long voucherID = Convert.ToInt64(editExpiryDateModel.ID.Substring(2));
        //            var updateVoucherDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().UpdateVoucherExpiryDate(voucherID, editExpiryDateModel.ExpiryDate, UserManager.GetLoggedInUser(), GetIP());
        //            if (updateVoucherDetailResult.IsSuccess)
        //            {
        //                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateVoucherDetailResult.ErrorMessage);
        //            }
        //        }
        //    }
        //    return Json(new { IsSuccess = false, ErrorMessage = string.Join(Environment.NewLine, ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)) }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult CreateShoppingReturnDetail(ShoppingEditModel shoppingEditModel)
        //{
        //    if ((shoppingEditModel.ReturnAmount == null || shoppingEditModel.ReturnAmount.Value == 0))
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.ReturnAmount));
        //    }
        //    if (string.IsNullOrWhiteSpace(shoppingEditModel.ReturnType))
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.ReturnType));
        //    }
        //    if (shoppingEditModel.ReceiptAmount.GetValueOrDefault() < shoppingEditModel.ReturnAmount.GetValueOrDefault())
        //    {
        //        ModelState.AddModelError("", Resources.ReturnAmountValidation);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        shoppingEditModel.ReceiptAmount = shoppingEditModel.ReturnAmount;
        //        var addShoppingReturnDetailResult = DALFactory.GetDALObject<IShoppingDetailDAL>().AddReturnRecord(ModelMapper.Instance.Mapper.Map<ShoppingDetail>(shoppingEditModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addShoppingReturnDetailResult.IsSuccess)
        //        {
        //            if (shoppingEditModel.IsPrint || shoppingEditModel.IsSendEmail)
        //            {
        //                var result = ClientPrintAndEmail.ProcessShoppingReturnPrintAndEmail(shoppingEditModel.IsPrint, shoppingEditModel.IsSendEmail, addShoppingReturnDetailResult.GetSingleResult(), SaveDocument);
        //                return Json(new { IsSuccess = true, PrintData = result.PrintData, PrintError = result.PrintError, EmailError = result.EmailError }, JsonRequestBehavior.AllowGet);
        //            }

        //            return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addShoppingReturnDetailResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new { IsSuccess = false, ErrorMessage = string.Join(Environment.NewLine, ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)) }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        public void SaveDocument(Stream stream, string fileName, string clientName, PrintAndEmailResult printAndEmailResult)
        {
            string fileNameNew = string.Format("{0}_{1}_{2}_{3}", clientName, GetIP(), DateTime.Now.ToString(UIExtensions.DateTimeFormat), fileName);
            fileNameNew = string.Join("_", fileNameNew.Split(Path.GetInvalidFileNameChars()));
            SaveDocument("TempPdf", fileNameNew, stream);
            printAndEmailResult.PrintData = fileNameNew;
        }

        [HttpGet]
        public virtual ActionResult DownloadDocument(string filePath, string mode)
        {
            return DownloadDocument("TempPdf", filePath, mode, System.Net.Mime.MediaTypeNames.Application.Pdf, mode != null && mode.ToLower() == "download");
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