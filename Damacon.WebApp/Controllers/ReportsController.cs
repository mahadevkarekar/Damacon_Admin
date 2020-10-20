using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.Operations.Contracts;
using Damacon.WebApp.Helpers;
using Damacon.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Damacon.DAL.i18n;
using Damacon.WebApp.Export.PDF;
using Damacon.WebApp.Export.Excel;
using Kendo.Mvc.UI;
using System.Globalization;
using Kendo.Mvc.Extensions;
using Damacon.SharedWeb.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace Damacon.WebApp.Controllers
{
    public class ReportsController : BaseController
    {
        //#region BasicSalesReport
        //// GET: Report
        //public ActionResult BasicSalesReport()
        //{
        //    BasicSalesReportModel basicSalesReportModel = new BasicSalesReportModel();
        //    InitBasicSalesReport(basicSalesReportModel);
        //    return View(basicSalesReportModel);
        //}

        //[HttpPost]
        //public ActionResult BasicSalesReport(BasicSalesReportModel basicSalesReportModel)
        //{
        //    InitBasicSalesReport(basicSalesReportModel);
        //    var exportData = DALFactory.GetDALObject<IReportsDAL>().GetDailyIncomeReportData(basicSalesReportModel.SelectedDate,basicSalesReportModel.CompanyID, basicSalesReportModel.StoreID);

        //    var filters = SetUpBasicSalesReportFilters(basicSalesReportModel);

        //    if (exportData.Result != null && exportData.Result.Count > 0)
        //    {
        //        if (basicSalesReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //        {
        //            var pdfResult = new PDFExportSalesReport().Export(exportData.Result, Resources.HM_DailyIncomeReport, filters, basicSalesReportModel.ReportLayout);
        //            if (pdfResult != null)
        //            {
        //                basicSalesReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //            }
        //        }
        //        else if (basicSalesReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //        {
        //            var pdfResult = new PDFExportSalesReport().Export(exportData.Result, Resources.HM_DailyIncomeReport, filters, basicSalesReportModel.ReportLayout);
        //            return pdfResult;
        //        }
        //        else if (basicSalesReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //        {
        //            var excelResult = new ExcelExportSalesReport().Export(exportData.Result, Resources.HM_DailyIncomeReport, filters, basicSalesReportModel.ReportLayout);
        //            return excelResult;
        //        }
        //    }
        //    return View(basicSalesReportModel);
        //}

        //private void InitBasicSalesReport(BasicSalesReportModel basicSalesReportModel)
        //{
        //    PopulateCompanies(false);
        //    PopulateStores(false);
        //    PopulateReportLayoutValues();
        //    if (basicSalesReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == basicSalesReportModel.CompanyID).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpBasicSalesReportFilters(BasicSalesReportModel basicSalesReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (basicSalesReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == basicSalesReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (basicSalesReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == basicSalesReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>("", basicSalesReportModel.SelectedDate.ToString("dddd dd MMMM yyyy")));
        //    return filters;
        //}
        //#endregion

        //#region MonthlyIncomeReport
        //[HttpGet]
        //public ActionResult MonthlyIncomeReport()
        //{
        //    var model = new MonthlyIncomeReportModel();
        //    model.Year = DateTime.Now.Year;
        //    model.Month = DateTime.Now.Month == 1 ? DateTime.Now.Month : DateTime.Now.Month - 1;
        //    InitMonthlyIncomeReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult MonthlyIncomeReport(MonthlyIncomeReportModel monthlyIncomeReportModel)
        //{
        //    monthlyIncomeReportModel.Error = string.Empty;
        //    InitMonthlyIncomeReport(monthlyIncomeReportModel);

        //    if (monthlyIncomeReportModel.StoreID < 1)
        //    {
        //        monthlyIncomeReportModel.Error = string.Format(Resources.M_FieldRequired, Resources.HM_Store);
        //    }

        //    if (string.IsNullOrEmpty(monthlyIncomeReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetMonthlyIncomeReportData(monthlyIncomeReportModel.Year, monthlyIncomeReportModel.Month, monthlyIncomeReportModel.StoreID);

        //        var filters = SetUpMonthlyIncomeReportFilters(monthlyIncomeReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (monthlyIncomeReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportMonthlyIncomeReport(monthlyIncomeReportModel.ReportChoice).Export(exportData.Result, Resources.HM_MonthlyIncomeReport, filters, monthlyIncomeReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    monthlyIncomeReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (monthlyIncomeReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportMonthlyIncomeReport(monthlyIncomeReportModel.ReportChoice).Export(exportData.Result, Resources.HM_MonthlyIncomeReport, filters, monthlyIncomeReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (monthlyIncomeReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {

        //                var pdfResult = new ExcelExportMonthlyIncomeReport(monthlyIncomeReportModel.ReportChoice).Export(exportData.Result, Resources.HM_MonthlyIncomeReport, filters, monthlyIncomeReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(monthlyIncomeReportModel);
        //}

        //private void InitMonthlyIncomeReport(MonthlyIncomeReportModel monthlyIncomeReportModel)
        //{
        //    PopulateMonths();
        //    PopulateYearValues();

        //    var result = DALFactory.GetDALObject<IStoreDAL>().GetForSalesByUserID(UserManager.GetLoggedInUser().ID);
        //    if (result.IsSuccess)
        //    {
        //        var stores = result.Result.OrderBy(x => x.StoreName).ToList();
        //        ViewData["Stores"] = stores;
        //        ViewData["Companies"] = stores.Select(x => x.Company).Distinct().OrderBy(x => x.CompanyName).ToList();
        //    }

        //    PopulateMonthlyIncomeReportChoices();
        //    PopulateReportLayoutValues();
        //    if (monthlyIncomeReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == monthlyIncomeReportModel.CompanyID).ToList();
        //    }

            
        //}

        //private IList<KeyValuePair<string, string>> SetUpMonthlyIncomeReportFilters(MonthlyIncomeReportModel monthlyIncomeReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (monthlyIncomeReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == monthlyIncomeReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (monthlyIncomeReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == monthlyIncomeReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>("", (new DateTime(monthlyIncomeReportModel.Year, monthlyIncomeReportModel.Month, 1)).ToString("MMMM yyyy")));

        //    return filters;
        //}

        //private void PopulateMonthlyIncomeReportChoices()
        //{
        //    IList<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem() { Text = Resources.Report_ReportMonthlyIncome, Value = Resources.Report_ReportMonthlyIncome });
        //    list.Add(new SelectListItem() { Text = Resources.Report_MonthlyIncomeWithBuyer, Value = Resources.Report_MonthlyIncomeWithBuyer });
        //    list.Add(new SelectListItem() { Text = Resources.Report_MonthlyIncomeDetailed, Value = Resources.Report_MonthlyIncomeDetailed });
        //    list.Add(new SelectListItem() { Text = Resources.Report_MonthlyIncomeDetailedWithBuyer, Value = Resources.Report_MonthlyIncomeDetailedWithBuyer });
        //    ViewData["ReportChoices"] = list;
        //}
        //#endregion

        //#region EmployeesReport
        //[HttpGet]
        //public ActionResult EmployeesWorkedHoursReport()
        //{
        //    var model = new EmployeesWorkedHoursReportModel();
        //    DateTime previousMonth = DateTime.Now.AddMonths(-1);
        //    model.From = new DateTime(previousMonth.Year, previousMonth.Month, 1);
        //    model.To = new DateTime(previousMonth.Year, previousMonth.Month, DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month));
        //    InitEmployeesWorkedHoursReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult EmployeesWorkedHoursReport(EmployeesWorkedHoursReportModel employeesReportModel)
        //{
        //    employeesReportModel.Error = string.Empty;
        //    InitEmployeesWorkedHoursReport(employeesReportModel);


        //    if (employeesReportModel.From == null || employeesReportModel.From == DateTime.MinValue)
        //    {
        //        employeesReportModel.Error = string.Format(Resources.M_FieldRequired, Resources.From);
        //    }

        //    if (employeesReportModel.To == null || employeesReportModel.To == DateTime.MinValue)
        //    {
        //        employeesReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.To);
        //    }

        //    if (employeesReportModel.From > employeesReportModel.To)
        //    {
        //        employeesReportModel.Error += "\\n" + Resources.M_FromToDateError;
        //    }

        //    if (string.IsNullOrEmpty(employeesReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetEmployeesWorkedHoursReportData(
        //            employeesReportModel.From, employeesReportModel.To, employeesReportModel.CompanyID, employeesReportModel.EmployeeID);

        //        PopulateWorkerTimeTypes();
        //        var filters = SetUpEmployeesWorkedHoursReportFilters(employeesReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (employeesReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportEmployeesWorkedHoursReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]), 
        //                    ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]))
        //                    .Export(ModelMapper.Instance.Mapper.Map<IList<EmployeesWorkedHoursModel>>(exportData.Result), Resources.HM_EmployeesWorkedHoursReport, filters, employeesReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    employeesReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (employeesReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportEmployeesWorkedHoursReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]),
        //                   ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]))
        //                   .Export(ModelMapper.Instance.Mapper.Map<IList<EmployeesWorkedHoursModel>>(exportData.Result), Resources.HM_EmployeesWorkedHoursReport, filters, employeesReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (employeesReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {

        //                var pdfResult = new ExcelExportEmployeesWorkedHoursReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]),
        //                   ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]))
        //                   .Export(ModelMapper.Instance.Mapper.Map<IList<EmployeesWorkedHoursModel>>(exportData.Result), Resources.HM_EmployeesWorkedHoursReport, filters, employeesReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(employeesReportModel);
        //}

        //private void InitEmployeesWorkedHoursReport(EmployeesWorkedHoursReportModel employeesReportModel)
        //{
        //    PopulateCompanies(false);
        //    PopulateStores(false);
        //    PopulateEmployees(false);
        //    PopulateReportLayoutValues();
        //    if (employeesReportModel.CompanyID > 0)
        //    {
        //        ViewData["Employees"] = (ViewData["Employees"] as List<EmployeeModel>).Where(x => x.EmployeeCompanies.Any(y => y.ID == employeesReportModel.CompanyID)).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpEmployeesWorkedHoursReportFilters(EmployeesWorkedHoursReportModel employeesReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (employeesReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == employeesReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>(Resources.From, employeesReportModel.From.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, employeesReportModel.To.ToString("dd/MM/yyyy")));
        //    return filters;
        //}

        //#endregion

        //#region MonthlyWorkedHoursByStoreReport
        //[HttpGet]
        //public ActionResult MonthlyWorkedHoursByStoreReport()
        //{
        //    var model = new MonthlyWorkedHoursByStoreReportModel();
        //    model.Year = DateTime.Now.Year;
        //    model.Month = DateTime.Now.Month == 1 ? DateTime.Now.Month : DateTime.Now.Month - 1;
        //    InitMonthlyWorkedHoursByStoreReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult MonthlyWorkedHoursByStoreReport(MonthlyWorkedHoursByStoreReportModel monthlyWorkedHoursByStoreReportModel)
        //{
        //    monthlyWorkedHoursByStoreReportModel.Error = string.Empty;
        //    InitMonthlyWorkedHoursByStoreReport(monthlyWorkedHoursByStoreReportModel);

        //    if (string.IsNullOrEmpty(monthlyWorkedHoursByStoreReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetEmployeesMonthlyWorkedHoursByStoreReportData(monthlyWorkedHoursByStoreReportModel.Year, monthlyWorkedHoursByStoreReportModel.Month, monthlyWorkedHoursByStoreReportModel.CompanyID, monthlyWorkedHoursByStoreReportModel.StoreID);
        //        PopulateWorkerTimeTypes();
        //        var filters = SetUpInitMonthlyWorkedHoursByStoreReportFilters(monthlyWorkedHoursByStoreReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (monthlyWorkedHoursByStoreReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportEmployeesMonthlyWorkedHoursByStoreReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]),
        //                    ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]),
        //                     ModelMapper.Instance.Mapper.Map<IList<EmployeeModel>>(DALFactory.GetDALObject<IEmployeeDAL>().GetAll(false).Result), 
        //                     monthlyWorkedHoursByStoreReportModel.Year, monthlyWorkedHoursByStoreReportModel.Month)
        //                    .Export(ModelMapper.Instance.Mapper.Map<IList<WorkedHourModel>>(exportData.Result), Resources.HM_MonthlyWorkedHoursByStoreReport, filters, monthlyWorkedHoursByStoreReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    monthlyWorkedHoursByStoreReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (monthlyWorkedHoursByStoreReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportEmployeesMonthlyWorkedHoursByStoreReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]),
        //                    ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]),
        //                     ModelMapper.Instance.Mapper.Map<IList<EmployeeModel>>(DALFactory.GetDALObject<IEmployeeDAL>().GetAll(false).Result),
        //                     monthlyWorkedHoursByStoreReportModel.Year, monthlyWorkedHoursByStoreReportModel.Month)
        //                    .Export(ModelMapper.Instance.Mapper.Map<IList<WorkedHourModel>>(exportData.Result), Resources.HM_MonthlyWorkedHoursByStoreReport, filters, monthlyWorkedHoursByStoreReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (monthlyWorkedHoursByStoreReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {

        //                var pdfResult = new ExcelExportEmployeesMonthlyWorkedHoursByStoreReport(ModelMapper.Instance.Mapper.Map<IList<StoreModel>>(ViewData["Stores"]),
        //                    ModelMapper.Instance.Mapper.Map<IList<WorkerTimeTypeModel>>(ViewData["WorkerTimeTypes"]),
        //                     ModelMapper.Instance.Mapper.Map<IList<EmployeeModel>>(DALFactory.GetDALObject<IEmployeeDAL>().GetAll(false).Result),
        //                     monthlyWorkedHoursByStoreReportModel.Year, monthlyWorkedHoursByStoreReportModel.Month)
        //                    .Export(ModelMapper.Instance.Mapper.Map<IList<WorkedHourModel>>(exportData.Result), Resources.HM_MonthlyWorkedHoursByStoreReport, filters, monthlyWorkedHoursByStoreReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(monthlyWorkedHoursByStoreReportModel);
        //}

        //private void InitMonthlyWorkedHoursByStoreReport(MonthlyWorkedHoursByStoreReportModel monthlyWorkedHoursByStoreReportModel)
        //{
        //    PopulateMonths();
        //    PopulateYearValues();
        //    PopulateEmployees(false);
        //    PopulateReportLayoutValues();

        //    var result = DALFactory.GetDALObject<IStoreDAL>().GetForSalesByUserID(UserManager.GetLoggedInUser().ID);
        //    if (result.IsSuccess)
        //    {
        //        var stores = result.Result.OrderBy(x => x.StoreName).ToList();
        //        ViewData["Stores"] = stores;
        //        ViewData["Companies"] = stores.Select(x => x.Company).Distinct().OrderBy(x => x.CompanyName).ToList();
        //    }

        //    if (monthlyWorkedHoursByStoreReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == monthlyWorkedHoursByStoreReportModel.CompanyID).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpInitMonthlyWorkedHoursByStoreReportFilters(MonthlyWorkedHoursByStoreReportModel monthlyWorkedHoursByStoreReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (monthlyWorkedHoursByStoreReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == monthlyWorkedHoursByStoreReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (monthlyWorkedHoursByStoreReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == monthlyWorkedHoursByStoreReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>("", (new DateTime(monthlyWorkedHoursByStoreReportModel.Year, monthlyWorkedHoursByStoreReportModel.Month, 1)).ToString("MMMM yyyy")));

        //    return filters;
        //}
        //#endregion

        //#region ShoppingCenterReport
        //[HttpGet]
        //public ActionResult ShoppingCenterReport()
        //{
        //    var model = new ShoppingCenterReportModel();
        //    DateTime previousMonth = DateTime.Now.AddMonths(-1);
        //    model.From = new DateTime(previousMonth.Year, previousMonth.Month, 1);
        //    model.To = new DateTime(previousMonth.Year, previousMonth.Month, DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month));
        //    InitShoppingCenterReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult ShoppingCenterReport(ShoppingCenterReportModel shoppingCenterReportModel)
        //{
        //    shoppingCenterReportModel.Error = string.Empty;
        //    InitShoppingCenterReport(shoppingCenterReportModel);

        //    if (shoppingCenterReportModel.From == null || shoppingCenterReportModel.From == DateTime.MinValue)
        //    {
        //        shoppingCenterReportModel.Error = string.Format(Resources.M_FieldRequired, Resources.From);
        //    }

        //    if (shoppingCenterReportModel.To == null || shoppingCenterReportModel.To == DateTime.MinValue)
        //    {
        //        shoppingCenterReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.To);
        //    }

        //    if (shoppingCenterReportModel.From > shoppingCenterReportModel.To)
        //    {
        //        shoppingCenterReportModel.Error += "\\n" + Resources.M_FromToDateError;
        //    }

        //    if (string.IsNullOrEmpty(shoppingCenterReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetShoppingCenterReportData(
        //            shoppingCenterReportModel.From, shoppingCenterReportModel.To, shoppingCenterReportModel.CompanyID, shoppingCenterReportModel.StoreID);

        //        var filters = SetUpShoppingCenterReportFilters(shoppingCenterReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (shoppingCenterReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportShoppingCenterReport().Export(exportData.Result , Resources.HM_ShoppingCenterReport, filters, shoppingCenterReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    shoppingCenterReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (shoppingCenterReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportShoppingCenterReport().Export(exportData.Result, Resources.HM_ShoppingCenterReport, filters, shoppingCenterReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (shoppingCenterReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {

        //                var pdfResult = new ExcelExportShoppingCenterReport().Export(exportData.Result, Resources.HM_ShoppingCenterReport, filters, shoppingCenterReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(shoppingCenterReportModel);
        //}

        //private void InitShoppingCenterReport(ShoppingCenterReportModel shoppingCenterReportModel)
        //{
        //    PopulateCompanies(false);
        //    PopulateStores(false);
        //    PopulateReportLayoutValues();
        //    if (shoppingCenterReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == shoppingCenterReportModel.CompanyID).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpShoppingCenterReportFilters(ShoppingCenterReportModel shoppingCenterReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (shoppingCenterReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == shoppingCenterReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>(Resources.From, shoppingCenterReportModel.From.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, shoppingCenterReportModel.To.ToString("dd/MM/yyyy")));
        //    return filters;
        //}

        //#endregion

        //#region ClassificaPeriodComparison
        //// GET: Report
        //public ActionResult ClassificaPeriodComparison()
        //{
        //    ClassificaPeriodComparisonReportModel classificaPeriodComparisonReportModel = new ClassificaPeriodComparisonReportModel();
        //    InitClassificaPeriodComparison(classificaPeriodComparisonReportModel);
        //    classificaPeriodComparisonReportModel.YearCurrent = DateTime.Now.Year;
        //    classificaPeriodComparisonReportModel.YearPrevious = DateTime.Now.Year - 1;
        //    classificaPeriodComparisonReportModel.WeekCurrent = UIExtensions.GetWeekOfYear(DateTime.Now) - 1;
        //    classificaPeriodComparisonReportModel.WeekPrevious = classificaPeriodComparisonReportModel.WeekCurrent;
        //    return View(classificaPeriodComparisonReportModel);
        //}

        //[HttpPost]
        //public ActionResult ClassificaPeriodComparison(ClassificaPeriodComparisonReportModel classificaPeriodComparisonReportModel)
        //{
        //    classificaPeriodComparisonReportModel.Error = string.Empty;
        //    InitClassificaPeriodComparison(classificaPeriodComparisonReportModel);

        //    if (classificaPeriodComparisonReportModel.WeekCurrent == 0)
        //    {
        //        classificaPeriodComparisonReportModel.Error = string.Format(Resources.M_FieldRequired, Resources.Current + " " + Resources.WeekNumber);
        //    }
        //    if (classificaPeriodComparisonReportModel.YearCurrent == 0)
        //    {
        //        classificaPeriodComparisonReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.Current + " " + Resources.Year);
        //    }

        //    if (classificaPeriodComparisonReportModel.WeekPrevious == 0)
        //    {
        //        classificaPeriodComparisonReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.Previous + " " + Resources.WeekNumber);
        //    }
        //    if (classificaPeriodComparisonReportModel.YearPrevious == 0)
        //    {
        //        classificaPeriodComparisonReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.Previous + " " + Resources.Year);
        //    }

        //    if (classificaPeriodComparisonReportModel.YearPrevious == classificaPeriodComparisonReportModel.YearCurrent &&
        //        classificaPeriodComparisonReportModel.WeekPrevious == classificaPeriodComparisonReportModel.WeekCurrent)
        //    {
        //        classificaPeriodComparisonReportModel.Error += "\\n" + Resources.M_CurrentPreviousShouldNotBeSame;
        //    }

        //    if (string.IsNullOrEmpty(classificaPeriodComparisonReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetIncomeComparisonReportData(
        //        classificaPeriodComparisonReportModel.YearCurrent, classificaPeriodComparisonReportModel.WeekCurrent,
        //        classificaPeriodComparisonReportModel.YearPrevious, classificaPeriodComparisonReportModel.WeekPrevious,
        //        classificaPeriodComparisonReportModel.CompanyID, classificaPeriodComparisonReportModel.StoreID);

        //        var filters = SetUpClassificaPeriodComparisonFilters(classificaPeriodComparisonReportModel);

        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (classificaPeriodComparisonReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportClassificaPeriodComparisonReport(classificaPeriodComparisonReportModel.YearCurrent, classificaPeriodComparisonReportModel.WeekCurrent,
        //                    classificaPeriodComparisonReportModel.YearPrevious, classificaPeriodComparisonReportModel.WeekPrevious)
        //                    .Export(exportData.Result, Resources.HM_ClassificaPeriodComparison, filters, classificaPeriodComparisonReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    classificaPeriodComparisonReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (classificaPeriodComparisonReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportClassificaPeriodComparisonReport(classificaPeriodComparisonReportModel.YearCurrent, classificaPeriodComparisonReportModel.WeekCurrent,
        //                   classificaPeriodComparisonReportModel.YearPrevious, classificaPeriodComparisonReportModel.WeekPrevious)
        //                   .Export(exportData.Result, Resources.HM_ClassificaPeriodComparison, filters, classificaPeriodComparisonReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (classificaPeriodComparisonReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {

        //                var pdfResult = new ExcelExportClassificaPeriodComparisonReport(classificaPeriodComparisonReportModel.YearCurrent, classificaPeriodComparisonReportModel.WeekCurrent,
        //                   classificaPeriodComparisonReportModel.YearPrevious, classificaPeriodComparisonReportModel.WeekPrevious)
        //                   .Export(exportData.Result, Resources.HM_ClassificaPeriodComparison, filters, classificaPeriodComparisonReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(classificaPeriodComparisonReportModel);
        //}

        //private void InitClassificaPeriodComparison(ClassificaPeriodComparisonReportModel classificaPeriodComparisonReportModel)
        //{
        //    PopulateCompanies(false);
        //    PopulateStores(false);
        //    PopulateYearValues();
        //    PopulateWeakNumbersValues();
        //    PopulateReportLayoutValues();
        //    if (classificaPeriodComparisonReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == classificaPeriodComparisonReportModel.CompanyID).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpClassificaPeriodComparisonFilters(ClassificaPeriodComparisonReportModel classificaPeriodComparisonReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (classificaPeriodComparisonReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == classificaPeriodComparisonReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (classificaPeriodComparisonReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == classificaPeriodComparisonReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    return filters;
        //}
        //#endregion

        //#region ComparisonResport
        //// GET: Report
        //public ActionResult ComparisonResport()
        //{
        //    ComparisonResportModel comparisonReportModel = new ComparisonResportModel();
        //    InitComparisonResport(comparisonReportModel);
        //    comparisonReportModel.CurrentPeriodStartDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    comparisonReportModel.CurrentPeriodEndDay = DateTime.Now.Day == 1  ? comparisonReportModel.CurrentPeriodStartDay : comparisonReportModel.CurrentPeriodStartDay.AddDays(DateTime.Now.Day - 2);
        //    comparisonReportModel.PreviousPeriodStartDay = comparisonReportModel.CurrentPeriodStartDay.AddYears(-1);
        //    comparisonReportModel.PreviousPeriodEndDay = comparisonReportModel.CurrentPeriodEndDay.AddYears(-1);
        //    comparisonReportModel.PreviousPeriodEndDay = comparisonReportModel.PreviousPeriodEndDay.AddDays(DateTime.DaysInMonth(comparisonReportModel.PreviousPeriodEndDay.Year, comparisonReportModel.PreviousPeriodEndDay.Month) - comparisonReportModel.PreviousPeriodEndDay.Day);
        //    return View(comparisonReportModel);
        //}

        //[HttpPost]
        //public ActionResult ComparisonResport(ComparisonResportModel comparisonReportModel)
        //{
        //    comparisonReportModel.Error = string.Empty;
        //    InitComparisonResport(comparisonReportModel);

        //    if (comparisonReportModel.CurrentPeriodStartDay == null || comparisonReportModel.CurrentPeriodEndDay == null)
        //    {
        //        comparisonReportModel.Error = string.Format(Resources.M_FieldRequired, Resources.Current + " " + Resources.Date);
        //    }

        //    if (comparisonReportModel.PreviousPeriodStartDay == null || comparisonReportModel.PreviousPeriodEndDay == null)
        //    {
        //        comparisonReportModel.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.Previous + " " + Resources.Date);
        //    }

        //    if (comparisonReportModel.CurrentPeriodStartDay == comparisonReportModel.PreviousPeriodStartDay &&
        //        comparisonReportModel.CurrentPeriodEndDay == comparisonReportModel.PreviousPeriodEndDay)
        //    {
        //        comparisonReportModel.Error += "\\n" + Resources.M_CurrentPreviousShouldNotBeSame;
        //    }

        //    if (comparisonReportModel.CurrentPeriodStartDay > comparisonReportModel.CurrentPeriodEndDay)
        //    {
        //        comparisonReportModel.Error += "\\n" + Resources.Current + " " + Resources.M_FromToDateError;
        //    }

        //    if (comparisonReportModel.PreviousPeriodStartDay > comparisonReportModel.PreviousPeriodEndDay)
        //    {
        //        comparisonReportModel.Error += "\\n" + Resources.Previous + " " + Resources.M_FromToDateError;
        //    }

        //    if (string.IsNullOrEmpty(comparisonReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetComparisonReportData(
        //        comparisonReportModel.CurrentPeriodStartDay, comparisonReportModel.CurrentPeriodEndDay,
        //        comparisonReportModel.PreviousPeriodStartDay, comparisonReportModel.PreviousPeriodEndDay,
        //        comparisonReportModel.CompanyID, comparisonReportModel.StoreID);

        //        var filters = SetUpComparisonResportFilters(comparisonReportModel);

        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (comparisonReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportComparisonResport((IList<Company>)ViewData["Companies"], comparisonReportModel.ReportChoice)
        //                    .Export(exportData.Result, comparisonReportModel.ReportChoice, filters, comparisonReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    comparisonReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (comparisonReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {

        //                var pdfResult = new PDFExportComparisonResport((IList<Company>)ViewData["Companies"], comparisonReportModel.ReportChoice)
        //                     .Export(exportData.Result, comparisonReportModel.ReportChoice, filters, comparisonReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (comparisonReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {
        //                var pdfResult = new ExcelExportComparisonResport((IList<Company>)ViewData["Companies"], comparisonReportModel.ReportChoice)
        //                     .Export(exportData.Result, comparisonReportModel.ReportChoice, filters, comparisonReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(comparisonReportModel);
        //}

        //private void InitComparisonResport(ComparisonResportModel comparisonReportModel)
        //{
        //    PopulateCompanies(false);
        //    PopulateStores(false);
        //    PopulateComparisonResportChoices();
        //    PopulateReportLayoutValues();
        //    if (comparisonReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == comparisonReportModel.CompanyID).ToList();
        //    }
        //}

        //private IList<KeyValuePair<string, string>> SetUpComparisonResportFilters(ComparisonResportModel comparisonReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    filters.Add(new KeyValuePair<string, string>(Resources.From, comparisonReportModel.PreviousPeriodStartDay.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, comparisonReportModel.PreviousPeriodEndDay.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.From, comparisonReportModel.CurrentPeriodStartDay.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, comparisonReportModel.CurrentPeriodEndDay.ToString("dd/MM/yyyy")));
        //    if (comparisonReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == comparisonReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (comparisonReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == comparisonReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    return filters;
        //}

        //private void PopulateComparisonResportChoices()
        //{
        //    IList<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem() { Text = Resources.Report_ReportChoice_IncomeComparison, Value = Resources.Report_ReportChoice_IncomeComparison });
        //    list.Add(new SelectListItem() { Text = Resources.Report_ReportChoice_IncomeComparisonByCompany, Value = Resources.Report_ReportChoice_IncomeComparisonByCompany });
        //    ViewData["ReportChoices"] = list;
        //}
        //#endregion

        //#region ClassificaReport
        //[HttpGet]
        //public ActionResult ClassificaReport()
        //{
        //    var model = new ClassificaReportModel();
        //    model.Year = DateTime.Now.Year;
        //    model.Month = DateTime.Now.Month == 1 ? DateTime.Now.Month : DateTime.Now.Month - 1;
        //    InitClassificaReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult ClassificaReport(ClassificaReportModel classificaReportModel)
        //{
        //    classificaReportModel.Error = string.Empty;
        //    InitClassificaReport(classificaReportModel);

        //    if (string.IsNullOrEmpty(classificaReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetClassificaReportData(classificaReportModel.Year, classificaReportModel.Month);

        //        var filters = SetUpClassificaReportFilters(classificaReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (classificaReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportClassificaReport().Export(exportData.Result, Resources.HM_Classifica, filters, classificaReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    classificaReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (classificaReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {
        //                var pdfResult = new PDFExportClassificaReport().Export(exportData.Result, Resources.HM_Classifica, filters, classificaReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (classificaReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {
        //                var pdfResult = new ExcelExportClassificaReport().Export(exportData.Result, Resources.HM_Classifica, filters, classificaReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(classificaReportModel);
        //}

        //private void InitClassificaReport(ClassificaReportModel classificaReportModel)
        //{
        //    PopulateMonths();
        //    PopulateYearValues();
        //    PopulateReportLayoutValues();
        //}

        //private IList<KeyValuePair<string, string>> SetUpClassificaReportFilters(ClassificaReportModel classificaReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    filters.Add(new KeyValuePair<string, string>("", (new DateTime(classificaReportModel.Year, classificaReportModel.Month, 1)).ToString("MMMM yyyy")));
        //    return filters;
        //}
        //#endregion

        //#region SentEmails
        //[HttpGet]
        //public ActionResult SentEmails()
        //{
        //    PopulateEmailConditions();
        //    PopulateStores(false);
        //    PopulateEmailSendResultTypes();
        //    return View();
        //}

        //public ActionResult ManageSentEmailsEditingPopup_Read([DataSourceRequest] DataSourceRequest request, string fromDate, string toDate)
        //{
        //    DateTime fromDateParsed;
        //    DateTime toDateParsed;
        //    if (!DateTime.TryParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateParsed))
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.From));
        //    }
        //    if (!DateTime.TryParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateParsed))
        //    {
        //        ModelState.AddModelError("", string.Format(Resources.M_FieldRequired, Resources.To));
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var getAllEmailTaskResultItemsResult = DALFactory.GetDALObject<IEmailServiceDAL>().GetEmailTaskResultItems();
        //        if (getAllEmailTaskResultItemsResult.IsSuccess)
        //        {

        //            getAllEmailTaskResultItemsResult.Result = getAllEmailTaskResultItemsResult.Result.Where(x => x.SentDateTime.Date >= fromDateParsed.Date && x.SentDateTime.Date <= toDateParsed.Date).ToList();
        //            SetUpFilters<EmailServiceItemModel>(request);
        //            var emailTaskResultItemModel = ModelMapper.Instance.Mapper.Map<IList<EmailTaskResultItemModel>>(getAllEmailTaskResultItemsResult.Result);
        //            return Json(emailTaskResultItemModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", getAllEmailTaskResultItemsResult.ErrorMessage);
        //        }
        //    }
        //    request.Filters.Clear();
        //    return Json((new List<DocumentRegistrationModel>()).ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        //private void PopulateEmailSendResultTypes()
        //{
        //    IList<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem() { Text = Resources.M_EmailSentSuccessfully, Value = Resources.M_EmailSentSuccessfully });
        //    list.Add(new SelectListItem() { Text = Resources.M_InvalidEmailCredentials, Value = Resources.M_InvalidEmailCredentials });
        //    list.Add(new SelectListItem() { Text = Resources.M_EmailNotConfiguredInStore, Value = Resources.M_EmailNotConfiguredInStore });
        //    ViewData["EmailSendResultTypes"] = list;
        //}
        //#endregion

        //#region ComparisonByYearReport
        //[HttpGet]
        //public ActionResult ComparisonByYearReport()
        //{
        //    var model = new ComparisonByYearReportModel();
        //    model.NumberOfYears = 5;
        //    model.LastYear = DateTime.Now.Year;
        //    InitComparisonByYearReport(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult ComparisonByYearReport(ComparisonByYearReportModel comparisonByYearReportModel)
        //{
        //    comparisonByYearReportModel.Error = string.Empty;
        //    InitComparisonByYearReport(comparisonByYearReportModel);

        //    if (string.IsNullOrEmpty(comparisonByYearReportModel.Error))
        //    {
        //        var exportData = DALFactory.GetDALObject<IReportsDAL>().GetComparisonByYearReportData(
        //            comparisonByYearReportModel.CompanyID, comparisonByYearReportModel.BrandID, comparisonByYearReportModel.StoreID,
        //            comparisonByYearReportModel.NumberOfYears, comparisonByYearReportModel.LastYear);

        //        var filters = SetUpComparisonByYearReportFilters(comparisonByYearReportModel);
        //        if (exportData.Result != null && exportData.Result.Count > 0)
        //        {
        //            if (comparisonByYearReportModel.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                var pdfResult = new PDFExportComparisonByYearReport(comparisonByYearReportModel).Export(exportData.Result, Resources.HM_ComparisonByYearReport, filters, comparisonByYearReportModel.ReportLayout);
        //                if (pdfResult != null)
        //                {
        //                    comparisonByYearReportModel.ReportPDFData = UIExtensions.GetBase64FromStream(pdfResult.FileStream);
        //                }
        //            }
        //            else if (comparisonByYearReportModel.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {
        //                var pdfResult = new PDFExportComparisonByYearReport(comparisonByYearReportModel).Export(exportData.Result, Resources.HM_ComparisonByYearReport, filters, comparisonByYearReportModel.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (comparisonByYearReportModel.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {
        //                var excelResult = new ExcelExportComparisonByYearReport(comparisonByYearReportModel).Export(exportData.Result, Resources.HM_ComparisonByYearReport, filters, comparisonByYearReportModel.ReportLayout);
        //                return excelResult;
        //            }
        //        }
        //    }
        //    return View(comparisonByYearReportModel);
        //}

        //private void InitComparisonByYearReport(ComparisonByYearReportModel comparisonByYearReportModel)
        //{
        //    PopulateYearValues();
        //    PopulateNumbers(1, 10, "LastYearNumbers");
        //    PopulateCompanies(false);
        //    PopulateBrands(false);
        //    PopulateStores(false);
        //    PopulateReportLayoutValues();
        //    if (comparisonByYearReportModel.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == comparisonByYearReportModel.CompanyID).ToList();
        //    }
        //    if (comparisonByYearReportModel.BrandID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.BrandID == comparisonByYearReportModel.BrandID).ToList();
        //    }

        //    IList<SelectListItem> generalSortValues = new List<SelectListItem> { new SelectListItem() { Text = Resources.HM_Brand, Value = Resources.HM_Brand } };
        //    ViewData["GeneralSortValues"] = generalSortValues;

        //    IList<SelectListItem> sortDiectionValues = new List<SelectListItem>
        //    { (new SelectListItem() { Text = Resources.Ascending, Value = Resources.Ascending }),
        //      (new SelectListItem() { Text = Resources.Descending, Value = Resources.Descending }) };
        //    ViewData["SortDiectionValues"] = sortDiectionValues;

        //    IList<SelectListItem> brandSortValues = new List<SelectListItem>
        //    { (new SelectListItem() { Text = Resources.StoreName, Value = Resources.StoreName }),
        //      (new SelectListItem() { Text = Resources.Income, Value = Resources.Income }),
        //      (new SelectListItem() { Text = Resources.Pieces, Value = Resources.Pieces }),
        //      (new SelectListItem() { Text = Resources.DifferentialPercent, Value = Resources.DifferentialPercent })};
        //    ViewData["BrandSortValues"] = brandSortValues;
        //}

        //private IList<KeyValuePair<string, string>> SetUpComparisonByYearReportFilters(ComparisonByYearReportModel comparisonByYearReportModel)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (comparisonByYearReportModel.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == comparisonByYearReportModel.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }
        //    if (comparisonByYearReportModel.BrandID > 0)
        //    {
        //        var brand = (ViewData["Brands"] as List<BrandModel>).FirstOrDefault(x => x.ID == comparisonByYearReportModel.BrandID);
        //        if (brand != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Brand, brand.Name));
        //        }
        //    }
        //    if (comparisonByYearReportModel.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == comparisonByYearReportModel.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }
        //    filters.Add(new KeyValuePair<string, string>(Resources.NumberOfYearsInReport, comparisonByYearReportModel.NumberOfYears.ToString()));
        //    filters.Add(new KeyValuePair<string, string>(Resources.LastYear, comparisonByYearReportModel.LastYear.ToString()));

        //    return filters;
        //}

        //#endregion

        //#region Report14
        //[HttpGet]
        //public ActionResult Report14()
        //{
        //    var model = new Report14Model();
        //    DateTime previousMonth = DateTime.Now.AddMonths(-1);
        //    model.From = new DateTime(previousMonth.Year, previousMonth.Month, 1);
        //    model.To = new DateTime(previousMonth.Year, previousMonth.Month, DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month));
        //    InitReport14(model);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Report14(Report14Model report14Model)
        //{
        //    report14Model.Error = string.Empty;
        //    InitReport14(report14Model);

        //    if (report14Model.From == null || report14Model.From == DateTime.MinValue)
        //    {
        //        report14Model.Error = string.Format(Resources.M_FieldRequired, Resources.From);
        //    }

        //    if (report14Model.To == null || report14Model.To == DateTime.MinValue)
        //    {
        //        report14Model.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.To);
        //    }

        //    if (report14Model.From > report14Model.To)
        //    {
        //        report14Model.Error += "\\n" + Resources.M_FromToDateError;
        //    }

        //    if (report14Model.StoreID <= 0)
        //    {
        //        report14Model.Error += "\\n" + string.Format(Resources.M_FieldRequired, Resources.HM_Store);
        //    }

        //    if (string.IsNullOrEmpty(report14Model.Error))
        //    {
        //        var exportWorkedHourData = DALFactory.GetDALObject<IReportsDAL>().GetReport14WorkedHourData(
        //            report14Model.StoreID, report14Model.From, report14Model.To);

        //        var exportSalesData = DALFactory.GetDALObject<IReportsDAL>().GetReport14SalesData(
        //            report14Model.StoreID, report14Model.From, report14Model.To);

        //        var filters = SetUpReport14Filters(report14Model);
        //        if (exportWorkedHourData.Result != null && exportWorkedHourData.Result.Count > 0)
        //        {
        //            var reportItems = exportWorkedHourData.Result.ConvertToReport14ParentDataItem(exportSalesData.Result);
        //            if (report14Model.ReportFormat == UIExtensions.ExportFormat_Inline)
        //            {
        //                report14Model.ReportItems = reportItems;
        //            }
        //            else if (report14Model.ReportFormat == UIExtensions.ExportFormat_PDF)
        //            {
        //                var pdfResult = new PDFExportReport14(report14Model.ChartData).Export(reportItems, Resources.HM_Report14, filters, report14Model.ReportLayout);
        //                return pdfResult;
        //            }
        //            else if (report14Model.ReportFormat == UIExtensions.ExportFormat_Excel)
        //            {
        //                var pdfResult = new ExcelExportReport14(report14Model.ChartData).Export(reportItems, Resources.HM_Report14, filters, report14Model.ReportLayout);
        //                return pdfResult;
        //            }
        //        }
        //    }
        //    return View(report14Model);
        //}

        //private void InitReport14(Report14Model report14Model)
        //{
        //    var result = DALFactory.GetDALObject<IStoreDAL>().GetForSalesByUserID(UserManager.GetLoggedInUser().ID);
        //    if (result.IsSuccess)
        //    {
        //        var stores = result.Result.OrderBy(x => x.StoreName).ToList();
        //        ViewData["Stores"] = stores;
        //        ViewData["Companies"] = stores.Select(x => x.Company).Distinct().OrderBy(x => x.CompanyName).ToList();
        //    }
        //    PopulateReportLayoutValues();
        //    if (report14Model.CompanyID > 0)
        //    {
        //        ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Where(x => x.CompanyID == report14Model.CompanyID).ToList();
        //    }
        //    report14Model.ReportItems = new List<Report14DataItem>();
        //}

        //private IList<KeyValuePair<string, string>> SetUpReport14Filters(Report14Model report14Model)
        //{
        //    IList<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();
        //    if (report14Model.CompanyID > 0)
        //    {
        //        var company = (ViewData["Companies"] as List<Company>).FirstOrDefault(x => x.ID == report14Model.CompanyID);
        //        if (company != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.Company, company.CompanyName));
        //        }
        //    }

        //    if (report14Model.StoreID > 0)
        //    {
        //        var store = (ViewData["Stores"] as List<Store>).FirstOrDefault(x => x.ID == report14Model.StoreID);
        //        if (store != null)
        //        {
        //            filters.Add(new KeyValuePair<string, string>(Resources.HM_Store, store.StoreName));
        //        }
        //    }

        //    filters.Add(new KeyValuePair<string, string>(Resources.From, report14Model.From.ToString("dd/MM/yyyy")));
        //    filters.Add(new KeyValuePair<string, string>(Resources.To, report14Model.To.ToString("dd/MM/yyyy")));
        //    return filters;
        //}

        //#endregion

        //#region Dashboard
        //[HttpGet]
        //public ActionResult Dashboard()
        //{
        //    DashboardModel dashboardModel = new DashboardModel();
        //    var getConfigResult = DALFactory.GetDALObject<IConfigDAL>().GetByKey("StoreDDashBoard");
        //    if (getConfigResult.IsSuccess && !string.IsNullOrWhiteSpace(getConfigResult.GetSingleResult().ConfigValue))
        //    {
        //        DashboardConfigModel dashboardConfigModel = JsonConvert.DeserializeObject<DashboardConfigModel>(getConfigResult.GetSingleResult().ConfigValue);
        //        dashboardModel = ModelMapper.Instance.Mapper.Map<DashboardModel>(dashboardConfigModel);
        //        dashboardModel.DailyGlobalIncomePeriod = dashboardConfigModel.ConfigDailyGlobalIncomePeriod;
        //        dashboardModel.DailyGlobalIncomePeriodTo = DateTime.Now.AddDays(-1);
        //        dashboardModel.DailyGlobalIncomeYear = dashboardConfigModel.ConfigDailyGlobalIncomeYear;
        //        dashboardModel.GlobalIncomeGroupBy = dashboardConfigModel.ConfigGlobalIncomeGroupBy;
        //        dashboardModel.GlobalIncomePeriodFrom = ConvertDashBoardPeriodToDate(DateTime.Now.AddDays(-1), dashboardConfigModel.ConfigGlobalIncomePeriod);
        //        dashboardModel.GlobalIncomePeriodTo = DateTime.Now.AddDays(-1);
        //        dashboardModel.YearIncomeGroupBy = dashboardConfigModel.ConfigYearIncomeGroupBy;
        //        dashboardModel.YearIncomeGroupId = dashboardConfigModel.ConfigYearIncomeGroupId ?? "0";
        //        dashboardModel.YearIncomeYear = dashboardConfigModel.ConfigYearIncomeYear;
        //    }
        //    else
        //    {
        //        dashboardModel.DailyGlobalIncomePeriod = "1Week";
        //        dashboardModel.DailyGlobalIncomePeriodTo = DateTime.Now;
        //        dashboardModel.DailyGlobalIncomeYear = 3;
        //        dashboardModel.GlobalIncomeGroupBy = "Brand";
        //        dashboardModel.GlobalIncomePeriodFrom = DateTime.Now.AddDays(-7);
        //        dashboardModel.GlobalIncomePeriodTo = DateTime.Now;
        //        dashboardModel.YearIncomeGroupBy = "Brand";
        //        dashboardModel.YearIncomeGroupId = "1";
        //        dashboardModel.YearIncomeYear = 3;
        //    }
        //    PopulateStores(false);
        //    PopulateCompanies(false);
        //    PopulateBrands(false);

        //    ViewData["Stores"] = (ViewData["Stores"] as List<Store>).Select(s => new { ID = s.ID, Name = s.StoreName });
        //    ViewData["Companies"] = (ViewData["Companies"] as List<Company>).Select(c => new { ID = c.ID, Name = c.CompanyName });
        //    ViewData["Brands"] = (ViewData["Brands"] as List<BrandModel>).Select(b => new { ID = b.ID, Name = b.Name });
        //    return View(dashboardModel);
        //}

        //public ActionResult GetDailyGlobalIncomeData(string period, string to, int years)
        //{
        //    DateTime periodTo = DateTime.MinValue;
        //    DateTime.TryParse(to, CultureInfo.CurrentUICulture, DateTimeStyles.None, out periodTo);
        //    DateTime periodFrom = ConvertDashBoardPeriodToDate(periodTo, period);
        //    var getDailyGlobalIncomeDataResult = DALFactory.GetDALObject<IReportsDAL>().GetDashBoardDailyGlobalIncome(periodFrom, periodTo, years);
        //    if (getDailyGlobalIncomeDataResult.IsSuccess)
        //    {
        //        DashBoardChartDataModel graphData = ModelMapper.Instance.Mapper.Map<DashBoardChartDataModel>(getDailyGlobalIncomeDataResult.GetSingleResult());
        //        return Json(graphData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { ErrorMessage = "" });
        //    }
        //}

        //public ActionResult GetGlobalIncomeData(string from, string to, string groupBy)
        //{
        //    DateTime periodFrom, periodTo = DateTime.MinValue;
        //    DateTime.TryParse(from, CultureInfo.CurrentUICulture, DateTimeStyles.None, out periodFrom);
        //    DateTime.TryParse(to, CultureInfo.CurrentUICulture, DateTimeStyles.None, out periodTo);
        //    var getDailyGlobalIncomeDataResult = DALFactory.GetDALObject<IReportsDAL>().GetDashBoardGlobalIncome(periodFrom, periodTo, groupBy);
        //    if (getDailyGlobalIncomeDataResult.IsSuccess)
        //    {
        //        DashBoardChartSeriesDataModel graphData = ModelMapper.Instance.Mapper.Map<DashBoardChartSeriesDataModel>(getDailyGlobalIncomeDataResult.GetSingleResult().DashBoardChartSeriesList.First());
        //        return Json(graphData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { ErrorMessage = "" });
        //    }
        //}

        //public ActionResult GetYearIncomeData(string groupBy, long? groupId, int years)
        //{
        //    var getDailyGlobalIncomeDataResult = DALFactory.GetDALObject<IReportsDAL>().GetDashBoardYearIncome(groupBy, groupId, years);
        //    if (getDailyGlobalIncomeDataResult.IsSuccess)
        //    {
        //        DashBoardChartDataModel graphData = ModelMapper.Instance.Mapper.Map<DashBoardChartDataModel>(getDailyGlobalIncomeDataResult.GetSingleResult());
        //        return Json(graphData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { ErrorMessage = "" });
        //    }
        //}

        //public ActionResult SaveDashBoardConfig(DashboardConfigModel dashboardConfigModel)
        //{
        //    string configValue = JsonConvert.SerializeObject(dashboardConfigModel);
        //    var saveConfigResult = DALFactory.GetDALObject<IConfigDAL>().AddOrUpdate("StoreDDashBoard", configValue, UserManager.GetLoggedInUser(), GetIP());
        //    if (saveConfigResult.IsSuccess)
        //    {
        //        return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { IsSuccess = false, ErrorMessage = Resources.M_InternalServerError });
        //    }
        //}

        //public ActionResult ExportDashboard(string chartImageData)
        //{
        //    try
        //    {
        //        string fileName = Resources.HM_Dashboard + "_" + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss_fff") + ".pdf";
        //        var pdfResult = new PDFExportDashboard(chartImageData)
        //            .Export(null, Resources.HM_Dashboard, new List<KeyValuePair<string, string>>(), Resources.Landscape);

        //        using (var fileStream = System.IO.File.Create(Server.MapPath("~\\TempPdf\\" + fileName)))
        //        {
        //            pdfResult.FileStream.Seek(0, SeekOrigin.Begin);
        //            pdfResult.FileStream.CopyTo(fileStream);
        //        }
        //        return Json(new { IsSuccess = true, FileName = fileName }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex) { }
        //    return Json(new { IsSuccess = false, ErrorMessage = Resources.M_InternalServerError }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult Download(string fileName)
        //{
        //    return DownloadDocument("TempPdf", fileName, "download", System.Net.Mime.MediaTypeNames.Application.Pdf, true);
            
        //}
        //#endregion
    }
}