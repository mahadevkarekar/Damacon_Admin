using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using Damacon.WebApp.Models;
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
    public class StoreController : BaseController
    {
        //#region Sales
        //[HttpGet]
        //public ActionResult ManageSales()
        //{
        //    var stores = PopulateStoresForSalesData();
        //    return View();
        //}

        //public ActionResult ManageSales_Read([DataSourceRequest] DataSourceRequest request, string selectedDate, string selectedStoreId)
        //{
        //    var salesData = new List<SaleModel>();
        //    if (!string.IsNullOrWhiteSpace(selectedDate) && !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        long storeID = Convert.ToInt64(selectedStoreId);
        //        var salesResult = DALFactory.GetDALObject<ISalesDAL>().GetByStoreIdNDate(storeID, salesDate);
        //        if (salesResult.IsSuccess)
        //        {
        //            if (salesResult.Result.Count > 0)
        //            {
        //                salesData.Add(ModelMapper.Instance.Mapper.Map<SaleModel>(salesResult.Result.First()));
        //            }
        //            else
        //            {
        //                salesData.Add(new SaleModel() { ID = -1,
        //                    StoreID = storeID,
        //                    SalesDate = salesDate,
        //                    IsIncludeInClassifica = true
        //                });
        //            }
        //        }
        //    }

        //    return Json(salesData.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageSalesEditingPopup_Update([DataSourceRequest] DataSourceRequest request, SaleModel salesModel)
        //{
        //    PreProcessSalesModelState(salesModel);
        //    if (salesModel != null && ModelState.IsValid)
        //    {
        //        var updateSalesResult = DALFactory.GetDALObject<ISalesDAL>().SaveSaleRecord(ModelMapper.Instance.Mapper.Map<Sale>(salesModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateSalesResult.IsSuccess)
        //        {
        //            salesModel = ModelMapper.Instance.Mapper.Map<SaleModel>(updateSalesResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateSalesResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { salesModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageSalesEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, SaleModel salesModel)
        //{
        //    PreProcessSalesModelState(salesModel);
        //    if (salesModel != null && salesModel.ID > 0)
        //    {
        //        salesModel.AutumnWinterIncome = 0;
        //        salesModel.AutumnWinterPieces = 0;
        //        salesModel.BuyerIncome = 0;
        //        salesModel.BuyerPieces = 0;
        //        salesModel.SpringSummerIncome = 0;
        //        salesModel.SpringSummerPieces = 0;
        //        salesModel.InternalInvoice_Quantity = null;
        //        salesModel.Notes = null;
        //        salesModel.Receipt_Quantity = null;
        //        salesModel.FiscalIncome = null;
        //        salesModel.IsIncludeInClassifica = false;
        //        salesModel.Lock = false;
        //        salesModel.NumberOfShoppers = null;
        //        var updateSalesResult = DALFactory.GetDALObject<ISalesDAL>().SaveSaleRecord(ModelMapper.Instance.Mapper.Map<Sale>(salesModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateSalesResult.IsSuccess)
        //        {
        //            salesModel = ModelMapper.Instance.Mapper.Map<SaleModel>(updateSalesResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateSalesResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { salesModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportSalesGrid(string columnSettings, string selectedOptions, string selectedDateExport, string selectedStoreIdExport, string selectedStoreTextExport)
        //{
        //    var salesData = new List<SaleModel>();
        //    if (!string.IsNullOrWhiteSpace(selectedDateExport) && !string.IsNullOrWhiteSpace(selectedStoreIdExport))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        long storeID = Convert.ToInt64(selectedStoreIdExport);
        //        var salesResult = DALFactory.GetDALObject<ISalesDAL>().GetByStoreIdNDate(storeID, salesDate);
        //        if (salesResult.IsSuccess)
        //        {
        //            if (salesResult.Result.Count > 0)
        //            {
        //                salesData.Add(ModelMapper.Instance.Mapper.Map<SaleModel>(salesResult.Result.First()));
        //            }
        //            else
        //            {
        //                salesData.Add(new SaleModel()
        //                {
        //                    ID = -1,
        //                    StoreID = storeID,
        //                    SalesDate = salesDate,
        //                    IsIncludeInClassifica = true
        //                });
        //            }
        //        }
        //    }

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    filters.Add(new KeyValuePair<string, string>(Resources.Date, selectedDateExport));
        //    filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, selectedStoreTextExport));

        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new Export.Excel.ExcelExportSale().Export(salesData, Resources.HM_Sales, filters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new Export.PDF.PDFExportSale().Export(salesData, Resources.HM_Sales, filters, Resources.Portrait);
        //    }
        //}

        //public ActionResult LockUnlockSaleData(long saleID, bool isLock)
        //{
        //    GenericActionResult<Sale> updateSalesResult = null;
        //    if (isLock)
        //    {
        //        updateSalesResult = DALFactory.GetDALObject<ISalesDAL>().LockRecord(saleID, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    else
        //    {
        //        updateSalesResult = DALFactory.GetDALObject<ISalesDAL>().UnLockRecord(saleID, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    return Json(new { IsSuccess = updateSalesResult.IsSuccess, ErrorMessage = updateSalesResult.ErrorMessage });
        //}

        //public ActionResult IncludeExcludeClassifica(long saleID, bool isIncluded)
        //{
        //    GenericActionResult<Sale> updateSalesResult = null;
        //    updateSalesResult = DALFactory.GetDALObject<ISalesDAL>().SetIncludeInClassificaForRecord(saleID, isIncluded, UserManager.GetLoggedInUser(), GetIP());
        //    return Json(new { IsSuccess = updateSalesResult.IsSuccess, ErrorMessage = updateSalesResult.ErrorMessage }); ;
        //}

        //private void PreProcessSalesModelState(SaleModel saleModel)
        //{
        //    if (saleModel.StoreID <= 0 || saleModel.SalesDate == null || saleModel.Lock)
        //    {
        //        ModelState.AddModelError("", Resources.InvalidData);
        //    }
        //}
        //#endregion

        //#region WorkersTime
        //[HttpGet]
        //public ActionResult ManageWorkersTime()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateEmployees();
        //    PopulateWorkerTimeTypes(false);
        //    return View();
        //}

        //public ActionResult ManageWorkedHoursEditingPopup_Read([DataSourceRequest] DataSourceRequest request, string selectedDate, string selectedStoreId)
        //{
        //    var workedHoursData = new List<WorkedHourModel>();
        //    if (!string.IsNullOrWhiteSpace(selectedDate) && !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        long storeID = Convert.ToInt64(selectedStoreId);
        //        var workedHoursResult = DALFactory.GetDALObject<IWorkedHourDAL>().GetByStoreIdNDate(storeID, salesDate);
        //        if (workedHoursResult.IsSuccess)
        //        {
        //            if (workedHoursResult.Result.Count > 0)
        //            {
        //                workedHoursData.AddRange(ModelMapper.Instance.Mapper.Map<List<WorkedHourModel>>(workedHoursResult.Result));
        //            }
        //        }
        //    }

        //    return Json(workedHoursData.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageWorkedHoursEditingPopup_Create([DataSourceRequest] DataSourceRequest request, WorkedHourModel workedHourModel)
        //{
        //    PreProcessWorkedHourModelState(workedHourModel);
        //    if (workedHourModel != null && ModelState.IsValid)
        //    {
        //        var addWorkedHourResult = DALFactory.GetDALObject<IWorkedHourDAL>().AddNew(ModelMapper.Instance.Mapper.Map<WorkedHour>(workedHourModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addWorkedHourResult.IsSuccess)
        //        {
        //            workedHourModel = ModelMapper.Instance.Mapper.Map<WorkedHourModel>(addWorkedHourResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addWorkedHourResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { workedHourModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageWorkedHoursEditingPopup_Update([DataSourceRequest] DataSourceRequest request, WorkedHourModel workedHourModel)
        //{
        //    PreProcessWorkedHourModelState(workedHourModel);
        //    if (workedHourModel != null && ModelState.IsValid)
        //    {
        //        if (workedHourModel.ID == -10)
        //        {
        //            workedHourModel.ID = 0;
        //            var addWorkedHourResult = DALFactory.GetDALObject<IWorkedHourDAL>().AddNew(ModelMapper.Instance.Mapper.Map<WorkedHour>(workedHourModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (addWorkedHourResult.IsSuccess)
        //            {
        //                workedHourModel = ModelMapper.Instance.Mapper.Map<WorkedHourModel>(addWorkedHourResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                workedHourModel.ID = -10;
        //                ModelState.AddModelError("", addWorkedHourResult.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            var updateWorkedHoursResult = DALFactory.GetDALObject<IWorkedHourDAL>().Update(ModelMapper.Instance.Mapper.Map<WorkedHour>(workedHourModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (updateWorkedHoursResult.IsSuccess)
        //            {
        //                workedHourModel = ModelMapper.Instance.Mapper.Map<WorkedHourModel>(updateWorkedHoursResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateWorkedHoursResult.ErrorMessage);
        //            }
        //        }
        //    }

        //    return Json(new[] { workedHourModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageWorkedHoursEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, WorkedHourModel workedHourModel)
        //{
        //    var deleteWorkedHoursResult = DALFactory.GetDALObject<IWorkedHourDAL>().DeleteRecord(workedHourModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //    if (!deleteWorkedHoursResult.IsSuccess)
        //    {
        //        ModelState.AddModelError("", deleteWorkedHoursResult.ErrorMessage);
        //    }

        //    return Json(new[] { workedHourModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportWorkedHoursGrid(string columnSettings, string selectedOptions, string selectedDateExport, string selectedStoreIdExport, string selectedStoreTextExport)
        //{
        //    var workedHoursData = new List<WorkedHourModel>();
        //    if (!string.IsNullOrWhiteSpace(selectedDateExport) && !string.IsNullOrWhiteSpace(selectedStoreIdExport))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        long storeID = Convert.ToInt64(selectedStoreIdExport);
        //        var workedHoursResult = DALFactory.GetDALObject<IWorkedHourDAL>().GetByStoreIdNDate(storeID, salesDate);
        //        if (workedHoursResult.IsSuccess)
        //        {
        //            if (workedHoursResult.Result.Count > 0)
        //            {
        //                workedHoursData.AddRange(ModelMapper.Instance.Mapper.Map<List<WorkedHourModel>>(workedHoursResult.Result));
        //            }

        //            for (int counter = 0; counter < workedHoursData.Count; counter++)
        //            {
        //                workedHoursData[counter].EmployeeName = workedHoursResult.Result.ElementAt(counter).Employee.FullName;
        //            }
        //        }
        //    }

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    filters.Add(new KeyValuePair<string, string>(Resources.Date, selectedDateExport));
        //    filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, selectedStoreTextExport));

        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new Export.Excel.ExcelExportWorkersTime().Export(workedHoursData, Resources.HM_WorkersTime, filters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new Export.PDF.PDFExportWorkersTime().Export(workedHoursData, Resources.HM_WorkersTime, filters, Resources.Portrait);
        //    }
        //}

        //public ActionResult LockUnlockWorkedHoursData(string selectedDate, string selectedStoreId, bool isLock)
        //{
        //    GenericActionResult lockUnlockWorkedHourResult = null;
        //    DateTime salesDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    long storeID = Convert.ToInt64(selectedStoreId);
        //    if (isLock)
        //    {
        //        lockUnlockWorkedHourResult = DALFactory.GetDALObject<IWorkedHourDAL>().LockRecord(storeID, salesDate, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    else
        //    {
        //        lockUnlockWorkedHourResult = DALFactory.GetDALObject<IWorkedHourDAL>().UnLockRecord(storeID, salesDate, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    return Json(new { IsSuccess = lockUnlockWorkedHourResult.IsSuccess, ErrorMessage = lockUnlockWorkedHourResult.ErrorMessage }); ;
        //}

        //public ActionResult CloneWorkedHoursData(long seletedCloneWorkerHourID, string cloneToDate, bool allEmployees)
        //{
        //    GenericActionResult cloneWorkedHoursDataResult = new GenericActionResult();
        //    DateTime cloneDate = DateTime.ParseExact(cloneToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    cloneWorkedHoursDataResult = DALFactory.GetDALObject<IWorkedHourDAL>().CloneWorkedHoursData(seletedCloneWorkerHourID, cloneDate, UserManager.GetLoggedInUser(), GetIP(), allEmployees);
        //    return Json(new { IsSuccess = cloneWorkedHoursDataResult.IsSuccess, ErrorMessage = cloneWorkedHoursDataResult.ErrorMessage }); ;
        //}

        //private void PreProcessWorkedHourModelState(WorkedHourModel workedHourModel)
        //{
        //    //if (string.IsNullOrWhiteSpace(workedHourModel.FirstName) && string.IsNullOrWhiteSpace(workedHourModel.CompanyName))
        //    //{
        //    //    this.ModelState.AddModelError("", Resources.M_EitherFirstNameOrCompanyIsRequired);
        //    //}
        //}

        //public ActionResult GetWorkerTimeTypes(long storeID, DateTime workedDate)
        //{
        //    IList<WorkerTimeType> workerTimeTypeList;
        //    var workerTimeTypeResult = DALFactory.GetDALObject<IWorkerTimeTypeDAL>().GetAll(false);
        //    var isHolidayResult = DALFactory.GetDALObject<IStoreDAL>().IsHolidayInStore(storeID, workedDate);
        //    if (workerTimeTypeResult.IsSuccess && isHolidayResult.IsSuccess)
        //    {
        //        if (isHolidayResult.GetSingleResult() == true)
        //        {
        //            workerTimeTypeList = workerTimeTypeResult.Result.Where(x => x.IsAvailableInHoliday).ToList();
        //        }
        //        else
        //        {
        //            workerTimeTypeList = workerTimeTypeResult.Result;
        //        }

        //        return Json(new
        //        {
        //            WorkerTimeTypes = ModelMapper.Instance.Mapper.Map<List<WorkerTimeTypeModel>>(workerTimeTypeList).Where(x => !x.Disable).OrderBy(x => x.TypeText).ToList(),
        //            IsSuccess = true,
        //            ErrorMessage = workerTimeTypeResult.ErrorMessage
        //        },
        //        JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { IsSuccess = false, ErrorMessage = workerTimeTypeResult.ErrorMessage + isHolidayResult.ErrorMessage }, JsonRequestBehavior.AllowGet); ;
        //}

        //#endregion

        //#region Summary
        //[HttpGet]
        //public ActionResult Summary()
        //{
        //    PopulateStores(false);
        //    PopulateYesNoValues();
        //    PopulateUsers();
        //    return View();
        //}

        //public ActionResult Summary_Read([DataSourceRequest] DataSourceRequest request, string selectedDate)
        //{
        //    var salesData = new List<SaleNWorkedHourModel>();
        //    if (!string.IsNullOrWhiteSpace(selectedDate))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        var salesResult = DALFactory.GetDALObject<ISalesDAL>().GetSaleSummary(salesDate);
        //        if (salesResult.IsSuccess)
        //        {
        //            salesData.AddRange(ModelMapper.Instance.Mapper.Map<List<SaleNWorkedHourModel>>(salesResult.Result));
        //        }
        //    }

        //    return Json(salesData.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region DocumentRegistrations
        //[HttpGet]
        //public ActionResult ManageDocumentRegistrations()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateGoodsTypes(false);
        //    PopulateDocumentTypes(false);
        //    PopulateInOutValues();
        //    PopulateEmployees(false);
        //    PopulateSuppliers();
        //    return View();
        //}

        //public ActionResult ManageDocumentRegistrationsEditingPopup_Read([DataSourceRequest] DataSourceRequest request,
        //    string fromDate, string toDate, string goodsInOutFromDate, string goodsInOutToDate, string registrationFromDate, string registrationToDate)
        //{
        //    DateTime fromDateParsed, toDateParsed, goodsInOutFromDateParsed, goodsInOutToDateParsed, registrationFromDateParsed, registrationToDateParsed;

        //    DateTime.TryParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateParsed);
        //    DateTime.TryParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateParsed);
        //    DateTime.TryParseExact(goodsInOutFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out goodsInOutFromDateParsed);
        //    DateTime.TryParseExact(goodsInOutToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out goodsInOutToDateParsed);
        //    DateTime.TryParseExact(registrationFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationFromDateParsed);
        //    DateTime.TryParseExact(registrationToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationToDateParsed);

        //    if (fromDateParsed == DateTime.MinValue &&
        //        goodsInOutFromDateParsed == DateTime.MinValue &&
        //        registrationFromDateParsed == DateTime.MinValue)
        //    {
        //        ModelState.AddModelError("", Resources.M_EitherDocumentDateOrGoodsInOutIsRequired);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        UserLite user = UserManager.GetLoggedInUser();
        //        if (user != null && user.UserType.ID == 3 && request.Filters.Count == 0)
        //        {
        //        }
        //        else
        //        {
        //            var getAllDocumentRegistrationsResult = DALFactory.GetDALObject<IDocumentRegistrationDAL>().GetAllByDate(false,
        //                fromDateParsed, toDateParsed, goodsInOutFromDateParsed, goodsInOutToDateParsed, registrationFromDateParsed, registrationToDateParsed);
        //            if (getAllDocumentRegistrationsResult.IsSuccess)
        //            {
        //                SetUpFilters<DocumentRegistrationModel>(request);
        //                var documentRegistrationModels = ModelMapper.Instance.Mapper.Map<IList<DocumentRegistrationModel>>(getAllDocumentRegistrationsResult.Result);
        //                return Json(documentRegistrationModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", getAllDocumentRegistrationsResult.ErrorMessage);
        //            }
        //        }
        //    }
        //    request.Filters.Clear();
        //    return Json((new List<DocumentRegistrationModel>()).ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentRegistrationsEditingPopup_Create([DataSourceRequest] DataSourceRequest request, DocumentRegistrationModel documentRegistrationModel)
        //{
        //    PreProcessDocumentRegistrationModelState(documentRegistrationModel);
        //    if (documentRegistrationModel != null && ModelState.IsValid)
        //    {
        //        var addDocumentRegistrationResult = DALFactory.GetDALObject<IDocumentRegistrationDAL>().AddNew(ModelMapper.Instance.Mapper.Map<DocumentRegistration>(documentRegistrationModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addDocumentRegistrationResult.IsSuccess)
        //        {
        //            documentRegistrationModel = ModelMapper.Instance.Mapper.Map<DocumentRegistrationModel>(addDocumentRegistrationResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addDocumentRegistrationResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentRegistrationsEditingPopup_Update([DataSourceRequest] DataSourceRequest request, DocumentRegistrationModel documentRegistrationModel)
        //{
        //    PreProcessDocumentRegistrationModelState(documentRegistrationModel);
        //    if (documentRegistrationModel.IsLocked)
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_InternalServerError));
        //    }
        //    if (documentRegistrationModel != null && ModelState.IsValid)
        //    {
        //        var updateDocumentRegistrationsResult = DALFactory.GetDALObject<IDocumentRegistrationDAL>().Update(ModelMapper.Instance.Mapper.Map<DocumentRegistration>(documentRegistrationModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateDocumentRegistrationsResult.IsSuccess)
        //        {
        //            documentRegistrationModel = ModelMapper.Instance.Mapper.Map<DocumentRegistrationModel>(updateDocumentRegistrationsResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateDocumentRegistrationsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentRegistrationsEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, DocumentRegistrationModel documentRegistrationModel)
        //{
        //    if (documentRegistrationModel != null)
        //    {
        //        documentRegistrationModel.Deleted = true;
        //        var updateDocumentRegistrationsResult = DALFactory.GetDALObject<IDocumentRegistrationDAL>().DeleteRecord(documentRegistrationModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateDocumentRegistrationsResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateDocumentRegistrationsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportDocumentRegistrationsGrid(string columnSettings, string selectedOptions, string fromDateExport, string toDateExport,
        //    string goodsInOutFromDateExport, string goodsInOutToDateExport, string registrationFromDateExport, string registrationToDateExport)
        //{
        //    IList<KeyValuePair<string, string>> exportFilters = new List<KeyValuePair<string, string>>();
        //    DateTime fromDateParsed, toDateParsed, goodsInOutFromDateParsed, goodsInOutToDateParsed, registrationFromDateParsed, registrationToDateParsed;

        //    if (DateTime.TryParseExact(fromDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.From, fromDateExport));
        //    }
        //    if (DateTime.TryParseExact(toDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.To, toDateExport));
        //    }

        //    if (DateTime.TryParseExact(goodsInOutFromDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out goodsInOutFromDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.GoodsInOutDate + " " + Resources.From, goodsInOutFromDateExport));
        //    }

        //    if (DateTime.TryParseExact(goodsInOutToDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out goodsInOutToDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.GoodsInOutDate + " " + Resources.To, goodsInOutToDateExport));
        //    }

        //    if (DateTime.TryParseExact(registrationFromDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationFromDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.RegistrationDate + " " + Resources.From, registrationFromDateExport));
        //    }

        //    if (DateTime.TryParseExact(registrationToDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationToDateParsed))
        //    {
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.RegistrationDate + " " + Resources.To, registrationToDateExport));
        //    }

        //    var exportData = ModelMapper.Instance.Mapper.Map<IEnumerable<DocumentRegistrationModel>>(
        //        DALFactory.GetDALObject<IDocumentRegistrationDAL>().GetAllByDate(false, fromDateParsed, toDateParsed, goodsInOutFromDateParsed, goodsInOutToDateParsed, registrationFromDateParsed, registrationToDateParsed).Result);

        //    return ExportGrid<DocumentRegistrationModel>(exportData.ToList(), columnSettings, selectedOptions, Resources.HM_DocumentsRegistration, exportFilters);
        //}

        //private void PreProcessDocumentRegistrationModelState(DocumentRegistrationModel documentRegistrationModel)
        //{
        //    if (documentRegistrationModel.SupplierID == null && string.IsNullOrWhiteSpace(documentRegistrationModel.SupplierOtherName))
        //    {
        //        ModelState.AddModelError("", Resources.M_SupplierNotEntered);
        //    }

        //    if (documentRegistrationModel.TypeOfGoodID == null && string.IsNullOrWhiteSpace(documentRegistrationModel.GoodsDescription))
        //    {
        //        ModelState.AddModelError("", Resources.M_TypeOfGoodsNotEntered);
        //    }

        //    if (documentRegistrationModel.VerifiedByEmployeeID == null && string.IsNullOrWhiteSpace(documentRegistrationModel.VerifiedByOtherName))
        //    {
        //        ModelState.AddModelError("", Resources.M_VerifiedByNotEntered);
        //    }
        //}

        //public ActionResult SaveDocument(HttpPostedFileBase documentFile)
        //{
        //    var fileName = Path.GetFileName(documentFile.FileName);
        //    fileName = Guid.NewGuid() + "_" + fileName;
        //    SaveDocument("UploadedDocuments", fileName, documentFile.InputStream);
        //    return Json(new { ImageUrl = fileName }, "text/plain");
        //}

        //[HttpGet]
        //public virtual ActionResult DownloadDocument(string filePath, string mode)
        //{
        //    return DownloadDocument("UploadedDocuments", filePath, mode, System.Net.Mime.MediaTypeNames.Application.Octet);
        //}
        //#endregion

        //#region WorkersTimetable
        //[HttpGet]
        //public ActionResult ManageWorkersTimetable()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateEmployees(false);
        //    PopulateWorkerTimeTypes(false);
        //    PopulateYearValues();
        //    PopulateWeakNumbersValues();
        //    return View();
        //}

        //public ActionResult ManageWorkersTimetableEditingPopup_Read([DataSourceRequest] DataSourceRequest request, string selectedStoreId, int year, int weekNumber, int dayNumber)
        //{
        //    var workedHoursData = new List<WorkersTimeTableEmployeeDataItemModel>();
        //    if (year > 0 && weekNumber > 0 && dayNumber > 0 && !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        DateTime date = FirstDateOfWeekISO8601(year, weekNumber).AddDays(dayNumber - 1);
        //        long storeID = Convert.ToInt64(selectedStoreId);
        //        var workersTimeTableEmployeeDataItemResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().GetByStoreIdNDate(storeID, date);
        //        if (workersTimeTableEmployeeDataItemResult.IsSuccess)
        //        {
        //            if (workersTimeTableEmployeeDataItemResult.Result.Count > 0)
        //            {
        //                workedHoursData.AddRange(ModelMapper.Instance.Mapper.Map<List<WorkersTimeTableEmployeeDataItemModel>>(workersTimeTableEmployeeDataItemResult.Result));
        //            }
        //        }
        //    }
        //    return Json(workedHoursData.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageWorkersTimetableEditingPopup_Update([DataSourceRequest] DataSourceRequest request, WorkersTimeTableEmployeeDataItemModel workersTimeTableEmployeeDataItemModel)
        //{
        //    PreProcessWorkersTimetableModelState(workersTimeTableEmployeeDataItemModel);
        //    if (workersTimeTableEmployeeDataItemModel != null && ModelState.IsValid)
        //    {
        //        if (workersTimeTableEmployeeDataItemModel.ID < 0)
        //        {
        //            long previousID = workersTimeTableEmployeeDataItemModel.ID;
        //            workersTimeTableEmployeeDataItemModel.ID = 0;
        //            var addWorkedHourResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().AddNew(ModelMapper.Instance.Mapper.Map<WorkersTimeTableEmployeeDataItem>(workersTimeTableEmployeeDataItemModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (addWorkedHourResult.IsSuccess)
        //            {
        //                workersTimeTableEmployeeDataItemModel = ModelMapper.Instance.Mapper.Map<WorkersTimeTableEmployeeDataItemModel>(addWorkedHourResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                workersTimeTableEmployeeDataItemModel.ID = previousID;
        //                ModelState.AddModelError("", addWorkedHourResult.ErrorMessage);
        //            }
        //        }
        //        else
        //        {
        //            var updateWorkedHoursResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().Update(ModelMapper.Instance.Mapper.Map<WorkersTimeTableEmployeeDataItem>(workersTimeTableEmployeeDataItemModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (updateWorkedHoursResult.IsSuccess)
        //            {
        //                workersTimeTableEmployeeDataItemModel = ModelMapper.Instance.Mapper.Map<WorkersTimeTableEmployeeDataItemModel>(updateWorkedHoursResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", updateWorkedHoursResult.ErrorMessage);
        //            }
        //        }
        //    }

        //    return Json(new[] { workersTimeTableEmployeeDataItemModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageWorkersTimetableEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, WorkersTimeTableEmployeeDataItemModel workersTimeTableEmployeeDataItemModel)
        //{
        //    var deleteWorkedHoursResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().DeleteRecord(workersTimeTableEmployeeDataItemModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //    if (!deleteWorkedHoursResult.IsSuccess)
        //    {
        //        ModelState.AddModelError("", deleteWorkedHoursResult.ErrorMessage);
        //    }

        //    return Json(new[] { workersTimeTableEmployeeDataItemModel }.ToDataSourceResult(request, ModelState));
        //}

        //private void PreProcessWorkersTimetableModelState(WorkersTimeTableEmployeeDataItemModel workersTimeTableEmployeeDataItemModel)
        //{
        //    //if (string.IsNullOrWhiteSpace(workedHourModel.FirstName) && string.IsNullOrWhiteSpace(workedHourModel.CompanyName))
        //    //{
        //    //    this.ModelState.AddModelError("", Resources.M_EitherFirstNameOrCompanyIsRequired);
        //    //}
        //}

        //[HttpGet]
        //public ActionResult ManageWorkersTimeTableDayData_Read(string selectedStoreId, int year, int weekNumber)
        //{
        //    var workersTimeTableDayDataItems = new List<WorkersTimeTableDayDataItemModel>();
        //    if (year > 0 && weekNumber > 0 && !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        DateTime date = FirstDateOfWeekISO8601(year, weekNumber);
        //        long storeID = Convert.ToInt64(selectedStoreId);
        //        var workersTimeTableDaysDataResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().GetDaysDataByStoreIdNDate(storeID, date, 7);
        //        if (workersTimeTableDaysDataResult.IsSuccess)
        //        {
        //            if (workersTimeTableDaysDataResult.Result.Count > 0)
        //            {
        //                workersTimeTableDayDataItems.AddRange(ModelMapper.Instance.Mapper.Map<List<WorkersTimeTableDayDataItemModel>>(workersTimeTableDaysDataResult.Result));
        //            }
        //        }
        //    }
        //    return Json(workersTimeTableDayDataItems, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult ManageWorkersTimeTableDayData_Save(WorkersTimeTableDayDataItemModel workersTimeTableDayDataItemModel)
        //{
        //    string errorMessage = string.Empty;
        //    if (workersTimeTableDayDataItemModel != null && ModelState.IsValid)
        //    {
        //        if (workersTimeTableDayDataItemModel.ID < 0)
        //        {
        //            long previousID = workersTimeTableDayDataItemModel.ID;
        //            workersTimeTableDayDataItemModel.ID = 0;
        //            var addWorkedHourResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().AddNewDayData(ModelMapper.Instance.Mapper.Map<WorkersTimeTableDayDataItem>(workersTimeTableDayDataItemModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (addWorkedHourResult.IsSuccess)
        //            {
        //                workersTimeTableDayDataItemModel = ModelMapper.Instance.Mapper.Map<WorkersTimeTableDayDataItemModel>(addWorkedHourResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                workersTimeTableDayDataItemModel.ID = previousID;
        //                errorMessage = addWorkedHourResult.ErrorMessage;
        //            }
        //        }
        //        else
        //        {
        //            var updateWorkedHoursResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().UpdateDayData(ModelMapper.Instance.Mapper.Map<WorkersTimeTableDayDataItem>(workersTimeTableDayDataItemModel), UserManager.GetLoggedInUser(), GetIP());
        //            if (updateWorkedHoursResult.IsSuccess)
        //            {
        //                workersTimeTableDayDataItemModel = ModelMapper.Instance.Mapper.Map<WorkersTimeTableDayDataItemModel>(updateWorkedHoursResult.GetSingleResult());
        //            }
        //            else
        //            {
        //                errorMessage = updateWorkedHoursResult.ErrorMessage;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var errorList = ModelState.Values.SelectMany(x => x.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(x => x.ErrorMessage).ToList();
        //        errorMessage = string.Join(Environment.NewLine, errorList);
        //    }
        //    return Json(new { Model = workersTimeTableDayDataItemModel, ErrorMessage = errorMessage });
        //}

        //[HttpPost]
        //public ActionResult LockUnlockWorkersTimeTableData(int weekNumber, int year, string selectedStoreId, bool isLock)
        //{
        //    GenericActionResult lockUnlockWorkedHourResult = null;
        //    long storeID = Convert.ToInt64(selectedStoreId);
        //    if (isLock)
        //    {
        //        lockUnlockWorkedHourResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().LockRecord(storeID, weekNumber, year, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    else
        //    {
        //        lockUnlockWorkedHourResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().UnLockRecord(storeID, weekNumber, year, UserManager.GetLoggedInUser(), GetIP());
        //    }
        //    return Json(new { IsSuccess = lockUnlockWorkedHourResult.IsSuccess, ErrorMessage = lockUnlockWorkedHourResult.ErrorMessage }); ;
        //}

        //[HttpPost]
        //public FileStreamResult ExportWorkersTimeTableGrid(string columnSettings, string selectedOptions, string storeIDExport, string storeNameExport, int weekNumberExport, int yearExport)
        //{
        //    IList<WorkersTimeTableReportModel> reportData = new List<WorkersTimeTableReportModel>();
        //    DateTime startDate = FirstDateOfWeekISO8601(yearExport, weekNumberExport);
        //    long storeID = Convert.ToInt64(storeIDExport);
        //    var workersTimeTableDaysDataResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().GetDaysDataByStoreIdNDate(storeID, startDate, 7);
        //    for (int dayCounter = 0; dayCounter < 7; dayCounter++)
        //    {
        //        DateTime currentDate = startDate.AddDays(dayCounter);
        //        var workersTimeTableEmployeeDataItemResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().GetByStoreIdNDate(storeID, currentDate);

        //        WorkersTimeTableReportModel workersTimeTableReportModel = new WorkersTimeTableReportModel();
        //        workersTimeTableReportModel.DataDate = currentDate;
        //        var dayData = workersTimeTableDaysDataResult.Result.FirstOrDefault(x => x.WorkedDate.Date == currentDate.Date);
        //        if (dayData != null)
        //        {
        //            workersTimeTableReportModel.WorkersTimeTableDayDataItemModel = ModelMapper.Instance.Mapper.Map<WorkersTimeTableDayDataItemModel>(dayData);
        //            workersTimeTableReportModel.WorkersTimeTableEmployeeDataItems = ModelMapper.Instance.Mapper.Map<List<WorkersTimeTableEmployeeDataItemModel>>(workersTimeTableEmployeeDataItemResult.Result);
        //        }
        //        reportData.Add(workersTimeTableReportModel);
        //    }

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, storeNameExport));
        //    filters.Add(new KeyValuePair<string, string>(Resources.WeekNumber, Resources.WeekNumber + " " + weekNumberExport.ToString()));
        //    filters.Add(new KeyValuePair<string, string>(Resources.From, startDate.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, startDate.AddDays(6).ToString("dd/MM/yyyy")));

        //    dynamic options = Newtonsoft.Json.JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new Export.Excel.ExcelExportWorkersTimeTableReport(DALFactory.GetDALObject<IWorkerTimeTypeDAL>().GetAll(false).Result).Export(reportData, Resources.HM_WorkersTimetable, filters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new Export.PDF.PDFExportWorkersTimeTableReport(DALFactory.GetDALObject<IWorkerTimeTypeDAL>().GetAll(false).Result).Export(reportData, Resources.HM_WorkersTimetable, filters, Resources.Portrait);
        //    }

        //}

        //[HttpPost]
        //public ActionResult CloneWorkesTimeTableData(string storeID, string sourceEmployeeID, int sourceYear, int sourceWeekNumber
        //                                            , string destinationEmployeeID, int destinationYear, int destinationWeekNumber)
        //{
        //    string errorMessage = null;
        //    long storeIDParsed;
        //    long sourceEmployeeIDParsed;
        //    long destinationEmployeeIDParsed;
        //    if (!long.TryParse(storeID, out storeIDParsed))
        //    {
        //        errorMessage = string.Format(Resources.M_FieldRequired, Resources.HM_Store) + Environment.NewLine;
        //    }
        //    if (!long.TryParse(sourceEmployeeID, out sourceEmployeeIDParsed))
        //    {
        //        errorMessage = string.Format(Resources.M_FieldRequired, Resources.Employee) + Environment.NewLine;
        //    }
        //    if (!long.TryParse(destinationEmployeeID, out destinationEmployeeIDParsed))
        //    {
        //        errorMessage = string.Format(Resources.M_FieldRequired, Resources.Employee) + Environment.NewLine;
        //    }

        //    if (string.IsNullOrWhiteSpace(errorMessage))
        //    {
        //        GenericActionResult cloneWorkesTimeTableDataResult = new GenericActionResult();
        //        cloneWorkesTimeTableDataResult = DALFactory.GetDALObject<IWorkersTimeTableDAL>().CloneWorkersTimeTableEmployeeData(storeIDParsed, sourceEmployeeIDParsed,
        //            sourceYear, sourceWeekNumber, destinationEmployeeIDParsed, destinationYear, destinationWeekNumber, UserManager.GetLoggedInUser(), GetIP());
        //        return Json(new { IsSuccess = cloneWorkesTimeTableDataResult.IsSuccess, ErrorMessage = cloneWorkesTimeTableDataResult.ErrorMessage }); ;
        //    }
        //    else
        //    {
        //        return Json(new { IsSuccess = false, ErrorMessage = errorMessage });
        //    }
        //}
        //#endregion

        //#region Documentations
        //[HttpGet]
        //public ActionResult ManageDocumentations()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateDocumentationTypes(false);
        //    PopulateEnabledDisabledValues();
        //    PopulateStoreSelectionTypes();
        //    return View();
        //}

        //public ActionResult ManageDocumentationsEditingPopup_Read([DataSourceRequest] DataSourceRequest request,
        //    string fromDate, string toDate, bool? showDeleted)
        //{
        //    DateTime fromDateParsed, toDateParsed;

        //    DateTime.TryParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateParsed);
        //    DateTime.TryParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateParsed);

        //    if (ModelState.IsValid)
        //    {
        //        var getAllDocumentationsResult = DALFactory.GetDALObject<IDocumentationDAL>().GetAll(showDeleted.GetValueOrDefault());
        //        if (getAllDocumentationsResult.IsSuccess)
        //        {
        //            if (fromDateParsed > DateTime.MinValue)
        //            {
        //                getAllDocumentationsResult.Result = getAllDocumentationsResult.Result.Where(x => x.LastModifyDateTime.Date >= fromDateParsed.Date).ToList();
        //            }
        //            if (toDateParsed > DateTime.MinValue)
        //            {
        //                getAllDocumentationsResult.Result = getAllDocumentationsResult.Result.Where(x => x.LastModifyDateTime.Date <= toDateParsed.Date).ToList();
        //            }

        //            UserLite user = UserManager.GetLoggedInUser();
        //            if (user != null && user.UserType.ID == 3)
        //            {
        //                getAllDocumentationsResult.Result = getAllDocumentationsResult.Result.Where(x => x.DocumentationStores.Count == 0 || x.DocumentationStores.Any(ds => ds.StoreID == user.StoreID)).ToList();
        //            }

        //            SetUpFilters<DocumentationModel>(request);
        //            var documentRegistrationModels = ModelMapper.Instance.Mapper.Map<List<DocumentationModel>>(getAllDocumentationsResult.Result);
        //            if (user != null && user.UserType.ID == 3)
        //            {
        //                documentRegistrationModels.ForEach(x => x.IsLocked = true);
        //            }
        //            return Json(documentRegistrationModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", getAllDocumentationsResult.ErrorMessage);
        //        }
        //    }
        //    request.Filters.Clear();
        //    return Json((new List<DocumentationModel>()).ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentationsEditingPopup_Create([DataSourceRequest] DataSourceRequest request, DocumentationModel documentRegistrationModel)
        //{
        //    if (UIExtensions.HasUserEditAccess(3))
        //    {
        //        ModelState.AddModelError("", Resources.M_InternalServerError);
        //    }
        //    if (documentRegistrationModel != null && ModelState.IsValid)
        //    {
        //        var addDocumentationResult = DALFactory.GetDALObject<IDocumentationDAL>().AddNew(ModelMapper.Instance.Mapper.Map<Documentation>(documentRegistrationModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (addDocumentationResult.IsSuccess)
        //        {
        //            documentRegistrationModel = ModelMapper.Instance.Mapper.Map<DocumentationModel>(addDocumentationResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", addDocumentationResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentationsEditingPopup_Update([DataSourceRequest] DataSourceRequest request, DocumentationModel documentRegistrationModel)
        //{
        //    if (documentRegistrationModel.IsLocked || UIExtensions.HasUserEditAccess(3))
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_InternalServerError));
        //    }
        //    if (documentRegistrationModel != null && ModelState.IsValid)
        //    {
        //        var updateDocumentationsResult = DALFactory.GetDALObject<IDocumentationDAL>().Update(ModelMapper.Instance.Mapper.Map<Documentation>(documentRegistrationModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateDocumentationsResult.IsSuccess)
        //        {
        //            documentRegistrationModel = ModelMapper.Instance.Mapper.Map<DocumentationModel>(updateDocumentationsResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateDocumentationsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageDocumentationsEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, DocumentationModel documentRegistrationModel)
        //{
        //    if (documentRegistrationModel != null)
        //    {
        //        documentRegistrationModel.Deleted = true;
        //        var updateDocumentationsResult = DALFactory.GetDALObject<IDocumentationDAL>().DeleteRecord(documentRegistrationModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateDocumentationsResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateDocumentationsResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { documentRegistrationModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportDocumentationsGrid(string columnSettings, string selectedOptions, string fromDateExport, string toDateExport, bool? showDeleted)
        //{
        //    var exportData = ModelMapper.Instance.Mapper.Map<IEnumerable<DocumentationModel>>(DALFactory.GetDALObject<IDocumentationDAL>().GetAll(showDeleted.GetValueOrDefault()).Result);
        //    IList<KeyValuePair<string, string>> exportFilters = new List<KeyValuePair<string, string>>();
        //    DateTime fromDateParsed, toDateParsed;

        //    if (DateTime.TryParseExact(fromDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateParsed))
        //    {
        //        exportData = exportData.Where(x => x.UploadDate.Date >= fromDateParsed.Date);
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.From, fromDateExport));
        //    }
        //    if (DateTime.TryParseExact(toDateExport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateParsed))
        //    {
        //        exportData = exportData.Where(x => x.UploadDate.Date <= toDateParsed.Date);
        //        exportFilters.Add(new KeyValuePair<string, string>(Resources.To, toDateExport));
        //    }

        //    return ExportGrid<DocumentationModel>(exportData.ToList(), columnSettings, selectedOptions, Resources.HM_Documentation, exportFilters);
        //}

        //#endregion

        //#region BuyerManagement
        //[HttpGet]
        //public ActionResult BuyerManagement()
        //{
        //    PopulateStoresForSalesData();
        //    PopulateBuyers(false);
        //    PopulateBuyers(true, "BuyersAll");
        //    return View();
        //}

        //public ActionResult BuyerManagement_Read([DataSourceRequest] DataSourceRequest request, string selectedDate, string selectedStoreId,
        //    long? brandID, long? buyerID, string fromDate, string toDate)
        //{
        //    List<SalesBuyerModel> salesData = ReadBuyerData(selectedDate, selectedStoreId, brandID, buyerID, fromDate, toDate);
        //    return Json(salesData.OrderByDescending(x => x.SalesDate).ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult BuyerManagementEditingPopup_Create([DataSourceRequest] DataSourceRequest request, SalesBuyerModel salesBuyerModel)
        //{
        //    if (salesBuyerModel != null && ModelState.IsValid)
        //    {
        //        var createSalesBuyerResult = DALFactory.GetDALObject<IBuyerManagementDAL>().AddNew(ModelMapper.Instance.Mapper.Map<SalesBuyer>(salesBuyerModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (createSalesBuyerResult.IsSuccess)
        //        {
        //            salesBuyerModel = ModelMapper.Instance.Mapper.Map<SalesBuyerModel>(createSalesBuyerResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", createSalesBuyerResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { salesBuyerModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult BuyerManagementEditingPopup_Update([DataSourceRequest] DataSourceRequest request, SalesBuyerModel salesBuyerModel)
        //{
        //    if (salesBuyerModel != null && ModelState.IsValid)
        //    {
        //        var updateSalesResult = DALFactory.GetDALObject<IBuyerManagementDAL>().Update(ModelMapper.Instance.Mapper.Map<SalesBuyer>(salesBuyerModel), UserManager.GetLoggedInUser(), GetIP());
        //        if (updateSalesResult.IsSuccess)
        //        {
        //            salesBuyerModel = ModelMapper.Instance.Mapper.Map<SalesBuyerModel>(updateSalesResult.GetSingleResult());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", updateSalesResult.ErrorMessage);
        //        }
        //    }

        //    return Json(new[] { salesBuyerModel }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult BuyerManagementEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, SalesBuyerModel salesBuyerModel)
        //{
        //    if (salesBuyerModel != null && ModelState.IsValid && salesBuyerModel.ID > 0)
        //    {
        //        var updateSalesResult = DALFactory.GetDALObject<IBuyerManagementDAL>().DeleteRecord(salesBuyerModel.ID, UserManager.GetLoggedInUser(), GetIP());
        //        if (!updateSalesResult.IsSuccess)
        //        {
        //            ModelState.AddModelError("", updateSalesResult.ErrorMessage);
        //        }
        //    }
        //    return Json(new[] { salesBuyerModel }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public FileStreamResult ExportBuyerManagementGrid(string columnSettings, string selectedOptions, string selectedDateExport, 
        //    string selectedStoreIdExport, string selectedStoreTextExport,
        //    long? selectedBrandIdExport, string selectedBrandTextExport,
        //    long? selectedBuyerIdExport, string selectedBuyerTextExport,
        //    string selectedFromDateExport, string selectedToDateExport)
        //{
        //    var exportData = ReadBuyerData(selectedDateExport, selectedStoreIdExport, selectedBrandIdExport, selectedBuyerIdExport, selectedFromDateExport, selectedToDateExport);
        //    exportData = exportData.OrderByDescending(x => x.SalesDate).ToList();

        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (!string.IsNullOrWhiteSpace(selectedDateExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.Date, selectedDateExport));
        //    }
        //    if (!string.IsNullOrWhiteSpace(selectedStoreTextExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, selectedStoreTextExport));
        //    }
        //    if (!string.IsNullOrWhiteSpace(selectedBrandTextExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.Brand, selectedBrandTextExport));
        //    }
        //    if (!string.IsNullOrWhiteSpace(selectedBuyerTextExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.Buyer, selectedBuyerTextExport));
        //    }
        //    if (!string.IsNullOrWhiteSpace(selectedFromDateExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.From, selectedFromDateExport));
        //    }
        //    if (!string.IsNullOrWhiteSpace(selectedToDateExport))
        //    {
        //        filters.Add(new KeyValuePair<string, string>(Resources.To, selectedToDateExport));
        //    }

        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
        //    if (options.format.ToString() == "xlsx")
        //    {
        //        return new Export.Excel.ExcelExportBuyerManagement().Export(exportData, Resources.HM_BuyerManagement, filters, Resources.Portrait);
        //    }
        //    else
        //    {
        //        return new Export.PDF.PdfExportBuyerManagement().Export(exportData, Resources.HM_BuyerManagement, filters, Resources.Portrait);
        //    }
        //}

        //[HttpGet]
        //public ActionResult GetSalesStatus(string selectedDate, string selectedStoreId)
        //{
        //    bool isLocked = false;
        //    if (!string.IsNullOrWhiteSpace(selectedDate) && !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        DateTime salesDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        long storeID = Convert.ToInt64(selectedStoreId);
        //        var salesResult = DALFactory.GetDALObject<ISalesDAL>().GetByStoreIdNDate(storeID, salesDate);
        //        if (salesResult.IsSuccess && salesResult.Result.Count > 0)
        //        {
        //            isLocked = salesResult.GetSingleResult().Lock;
        //        }
        //    }

        //    return Json(new { IsLocked = isLocked }, JsonRequestBehavior.AllowGet);
        //}

        //private List<SalesBuyerModel> ReadBuyerData(string selectedDate, string selectedStoreId, long? brandID, long? buyerID, string fromDate, string toDate)
        //{
        //    var salesData = new List<SalesBuyerModel>();
        //    DateTime fromDateParsed = DateTime.MinValue;
        //    DateTime toDateParsed = DateTime.MaxValue;

        //    if (!string.IsNullOrWhiteSpace(selectedDate))
        //    {
        //        fromDateParsed = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        toDateParsed = fromDateParsed;
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrWhiteSpace(fromDate))
        //        {
        //            fromDateParsed = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        }
        //        if (!string.IsNullOrWhiteSpace(toDate))
        //        {
        //            toDateParsed = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        }
        //    }

        //    if (!UIExtensions.HasUserEditAccess(3) || !string.IsNullOrWhiteSpace(selectedStoreId))
        //    {
        //        long storeID = string.IsNullOrWhiteSpace(selectedStoreId) ? 0 : Convert.ToInt64(selectedStoreId);
        //        var salesResult = DALFactory.GetDALObject<IBuyerManagementDAL>().GetByStoreIdNDate(storeID, fromDateParsed, toDateParsed);
        //        if (salesResult.IsSuccess && salesResult.Result.Count > 0)
        //        {
        //            if (brandID.HasValue && brandID.Value > 0)
        //            {
        //                salesResult.Result = salesResult.Result.Where(x => x.Store.BrandDetail.ID == brandID.Value).ToList();
        //            }
        //            if (buyerID.HasValue && buyerID.Value > 0)
        //            {
        //                salesResult.Result = salesResult.Result.Where(x => x.BuyerID == buyerID.Value).ToList();
        //            }
        //            salesData.AddRange(ModelMapper.Instance.Mapper.Map<List<SalesBuyerModel>>(salesResult.Result));
        //        }
        //    }

        //    return salesData;
        //}
        //#endregion
    }
}