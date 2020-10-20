using Damacon.DAL;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.WebApp.Helpers;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using Damacon.WebApp.Models;
using System.Globalization;
using Damacon.WebApp.Export.PDF;
using Damacon.WebApp.Export.Excel;
using Damacon.DAL.Database.EF;
using Kendo.Mvc.Extensions;
using Damacon.SharedWeb.Helpers;
using System.ComponentModel;
using Kendo.Mvc.Export;

namespace Damacon.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;

            // Attempt to read the culture cookie from Request
            if (cultureName == null)
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            if (RouteData.Values["culture"] as string != cultureName)
            {

                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            if (CacheManager.GetFromCache(UIExtensions.LanguageMetaCacheKey) == null)
            {
                var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllLanguageMetas();
                if (result.IsSuccess)
                {
                    CacheManager.AddToCache(UIExtensions.LanguageMetaCacheKey, result.Result);
                }
            }

            // Modify current thread's cultures   
            var currentCulture = (CultureInfo)(new CultureInfo(cultureName).Clone());
            if (cultureName != "it-IT")
            {
                var italianCultureInfo = new CultureInfo("it-IT");
                currentCulture.DateTimeFormat.DateSeparator = italianCultureInfo.DateTimeFormat.DateSeparator;
                currentCulture.DateTimeFormat.FullDateTimePattern = italianCultureInfo.DateTimeFormat.FullDateTimePattern;
                currentCulture.DateTimeFormat.LongDatePattern = italianCultureInfo.DateTimeFormat.LongDatePattern;
                currentCulture.DateTimeFormat.LongTimePattern = italianCultureInfo.DateTimeFormat.LongTimePattern;
                currentCulture.DateTimeFormat.MonthDayPattern = italianCultureInfo.DateTimeFormat.MonthDayPattern;
                currentCulture.DateTimeFormat.ShortDatePattern = italianCultureInfo.DateTimeFormat.ShortDatePattern;
                currentCulture.DateTimeFormat.ShortTimePattern = italianCultureInfo.DateTimeFormat.ShortTimePattern;
                currentCulture.DateTimeFormat.TimeSeparator = italianCultureInfo.DateTimeFormat.TimeSeparator;
                currentCulture.DateTimeFormat.YearMonthPattern = italianCultureInfo.DateTimeFormat.YearMonthPattern;
                currentCulture.NumberFormat = italianCultureInfo.NumberFormat;
            }

            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult SetCulture(string languageSelection)
        {
            // Validate input
            languageSelection = CultureHelper.GetImplementedCulture(languageSelection);
            RouteData.Values["culture"] = languageSelection;  // set culture
            return RedirectToLocal(null, UserManager.GetLoggedInUser().DefaultLoginPage.LinkController, UserManager.GetLoggedInUser().DefaultLoginPage.LinkAction);
        }

        public ActionResult SaveImage(HttpPostedFileBase profileImageFile)
        {
            var fileName = Path.GetFileName(profileImageFile.FileName);
            fileName = Guid.NewGuid() + "_" + fileName;
            var physicalPath = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

            profileImageFile.SaveAs(physicalPath);

            return Json(new { ImageUrl = fileName }, "text/plain");
        }

        protected string GetIP()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        protected void PopulateUserTypes()
        {
            var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllUserTypes();
            if (result.IsSuccess)
            {
                ViewData["UserTypes"] = result.Result.OrderBy(x => x.UserTypeEn).ToList();
            }
        }

        protected void PopulateCountries()
        {
            var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllCountries();
            if (result.IsSuccess)
            {
                ViewData["Countries"] = result.Result.OrderBy(x => x.CountryEN).ToList();
            }
        }

       

        protected void PopulateIncludedExcludedValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.Included, Value = true.ToString().ToLower() });
            list.Add(new SelectListItem() { Text = Resources.Excluded, Value = false.ToString().ToLower() });
            ViewData["IncludedExcludedValues"] = list;
        }

        protected void PopulateYesNoValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.Yes, Value = true.ToString().ToLower() });
            list.Add(new SelectListItem() { Text = Resources.No, Value = false.ToString().ToLower() });
            ViewData["YesNoValues"] = list;
        }

        protected void PopulateEnabledDisabledValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.Enabled, Value = false.ToString().ToLower() });
            list.Add(new SelectListItem() { Text = Resources.Disabled, Value = true.ToString().ToLower() });
            ViewData["EnabledDisabledValues"] = list;
        }

        protected void PopulateActiveExpiredValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.Active, Value = true.ToString().ToLower() });
            list.Add(new SelectListItem() { Text = Resources.Expired, Value = false.ToString().ToLower() });
            ViewData["ActiveExpiredValues"] = list;
        }

        protected void PopulateYearValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            int endYear = DateTime.Now.Year + 1;
            for (int counter = endYear; counter >= 2000; counter--)
            {
                list.Add(new SelectListItem() { Text = counter.ToString(), Value = counter.ToString() });
            }
            ViewData["YearValues"] = list;
        }

        protected void PopulateNumbers(int startNumber, int endNumber, string viewDataKeyName)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            for (int counter = startNumber; counter <= endNumber; counter++)
            {
                list.Add(new SelectListItem() { Text = counter.ToString(), Value = counter.ToString() });
            }
            ViewData[viewDataKeyName] = list;
        }

        protected void PopulateWeakNumbersValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            for (int counter = 1; counter <= 53; counter++)
            {
                list.Add(new SelectListItem() { Text = counter.ToString(), Value = counter.ToString() });
            }
            ViewData["WeakValues"] = list;
        }

       

        protected void PopulateUsers(bool getDisabled = true)
        {
            var result = DALFactory.GetDALObject<IUserDAL>().GetAllUsersWithPassword();
            if (result.IsSuccess)
            {
                ViewData["Users"] = ModelMapper.Instance.Mapper.Map<IList<UserModel>>(result.Result.Where(x => getDisabled || !x.Disable)).OrderBy(x => x.FullName).ToList(); ;
            }
        }

        protected void PopulateMonths()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            int index = 1;
            foreach (string monthName in DateTimeFormatInfo.CurrentInfo.MonthNames)
            {
                if (!string.IsNullOrEmpty(monthName))
                {
                    list.Add(new SelectListItem() { Text = monthName, Value = index.ToString() });
                    index++;
                }
            }
            ViewData["Months"] = list;
        }

        protected void PopulateYears(int startYear, int SelectedYear = 0, int iLastYear = 0)
        {
            if (SelectedYear == 0) SelectedYear = startYear;
            if (iLastYear == 0) iLastYear = DateTime.Now.Year;

            IList<SelectListItem> list = new List<SelectListItem>();
            for (int i = startYear; i <= iLastYear; i++)
            {
                list.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = (SelectedYear == i) });
            }
            ViewData["Years"] = list;
        }

        protected void PopulateReportLayoutValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.Portrait, Value = Resources.Portrait });
            list.Add(new SelectListItem() { Text = Resources.Landscape, Value = Resources.Landscape });
            ViewData["ReportLayoutValues"] = list;
        }

        protected void PopulateInOutValues()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.In, Value = "In" });
            list.Add(new SelectListItem() { Text = Resources.Out, Value = "Out" });
            ViewData["InOutValues"] = list;
        }

       

      

       

        //protected void PopulateEmailConditions()
        //{
        //    var emailConditionList = Enum.GetValues(typeof(EmailSendConditionType)).OfType<EmailSendConditionType>().Select(x => new { Text = Resources.GetResource(x.ToString()), Value = (int)x }).ToList();
        //    ViewData["EmailConditions"] = emailConditionList;
        //}

        protected void PopulateTimeIntervales()
        {
            IList<DateTime> timeIntervals = new List<DateTime>();
            for (int counter = 0; counter < 48; counter++)
            {
                timeIntervals.Add(new DateTime(1, 1, 1).AddMinutes(counter * 30));
            }
            ViewData["TimeIntervals"] = timeIntervals.Select(x => new { Text = x.ToString("HH:mm"), Value = x.ToString("HH:mm") }).ToList();
        }

        //protected void PopulateEmailDataDates()
        //{
        //    var emailConditionList = Enum.GetValues(typeof(EmailDataDateType)).OfType<EmailDataDateType>().Select(x => new { Text = Resources.GetResource(x.ToString()), Value = (int)x }).ToList();
        //    ViewData["EmailDataDates"] = emailConditionList;
        //}

        protected void PopulateDocumentationTypes(bool getDisabled = true)
        {
            var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllDocumentationTypes();
            if (result.IsSuccess)
            {
                ViewData["DocumentationTypes"] = result.Result.OrderBy(x => x.TypeText).ToList();
            }
        }

        protected void PopulateStoreSelectionTypes()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = Resources.AllStores });
            list.Add(new SelectListItem { Value = "2", Text = Resources.SomeStores });
            ViewData["StoreSelectionTypes"] = list;
        }

       

        protected void PopulateBonusRecordTypes()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.BonusAdded, Value = Resources.BonusAdded });
            list.Add(new SelectListItem() { Text = Resources.BonusUsed, Value = Resources.BonusUsed });
            list.Add(new SelectListItem() { Text = Resources.ExtraBonus, Value = Resources.ExtraBonus });
            list.Add(new SelectListItem() { Text = Resources.LastBalance, Value = Resources.LastBalance });
            ViewData["BonusRecordTypes"] = list;
        }

        protected void PopulateShoppingRecordTypes()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "Shopping", Text = Resources.Shopping });
            list.Add(new SelectListItem() { Value = "VoucherCreation", Text = Resources.VoucherCreation });
            list.Add(new SelectListItem() { Value = "ShoppingWithVoucher", Text = Resources.ShoppingWithVoucher });
            list.Add(new SelectListItem() { Value = "VirtualShopping", Text = Resources.VirtualShopping });
            list.Add(new SelectListItem() { Value = "ShoppingReturn", Text = Resources.Return });
            ViewData["ShoppingRecordTypes"] = list;
        }

      

        protected void PopulateWeekDays()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = DayOfWeek.Monday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Monday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Tuesday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Tuesday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Wednesday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Wednesday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Thursday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Thursday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Friday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Friday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Saturday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Saturday) });
            list.Add(new SelectListItem() { Value = DayOfWeek.Sunday.ToString(), Text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Sunday) });
            ViewData["WeekDays"] = list;
        }

       
        protected void PopulateLeaveTypes(bool getDisabled = true)
        {
            var result = DALFactory.GetDALObject<IStaticDataDAL>().GetAllLeaveTypes();
            if (result.IsSuccess)
            {
                ViewData["LeaveTypes"] = result.Result.ToList();
            }
        }

        protected void PopulateShoppingReturnReimbursementTypes()
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = Resources.ReturnTypeBonus, Value = "ReturnTypeBonus" });
            list.Add(new SelectListItem() { Text = Resources.ReturnTypeCash, Value = "ReturnTypeCash" });
            ViewData["ShoppingReturnReimbursementTypes"] = list;
        }

        protected DateTime? GetDateFromFilter(FilterData filters, string key, string format, bool isAppendYear)
        {
            DateTime? dateResult = null;
            NameValue date = filters.ColumnFilters.FirstOrDefault(x => x.Value == key && !string.IsNullOrEmpty(x.Text));
            if (date != null)
            {
                dateResult = DateTime.ParseExact(date.Text + (isAppendYear ? " 2016" : ""), format, CultureInfo.CurrentCulture);
            }
            return dateResult;
        }

        
        protected List<KeyValuePair<string, string>> SetUpFilters<T>(DataSourceRequest request, IList<string> ignoreColumns = null)
        {
            List<KeyValuePair<string, string>> exportFilters = new List<KeyValuePair<string, string>>();
            if (request.Filters.Count > 0 && ((FilterDescriptor)request.Filters[0]).Member == "All")
            {
                FilterDescriptor allFilter = ((FilterDescriptor)request.Filters[0]);
                string[] splitValues = allFilter.Value.ToString().Split(new string[] { "####" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitValues.Length > 1)
                {
                    var filterData = JsonConvert.DeserializeObject<FilterData>(HttpUtility.UrlDecode(splitValues[0]));

                    var jsonResolver = new IgnorableSerializerContractResolver();
                    // ignore single property
                    jsonResolver.Ignore(typeof(ExportColumnSettings), "Width");
                    var jsonSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = jsonResolver };
                    var columnsData = JsonConvert.DeserializeObject<IList<ExportColumnSettingsEx>>(HttpUtility.UrlDecode(splitValues[1]), jsonSettings);
                    request.Filters.RemoveAt(0);

                    CompositeFilterDescriptor textFilter = new CompositeFilterDescriptor();
                    textFilter.LogicalOperator = FilterCompositionLogicalOperator.Or;

                    columnsData.RemoveAt(columnsData.Count - 1);

                    if (!string.IsNullOrWhiteSpace(filterData.GenericSearchText))
                    {
                        exportFilters.Add(new KeyValuePair<string, string>(Resources.GenericSearch, filterData.GenericSearchText));
                    }

                    foreach (var column in columnsData)
                    {
                        if (ignoreColumns != null && ignoreColumns.Any(x => x.ToLower() == column.Field.ToLower()))
                        {
                            continue;
                        }
                        NameValue filterColumn = null;
                        if (filterData.ColumnFilters != null)
                        {
                            filterColumn = filterData.ColumnFilters.FirstOrDefault(x => x.Value.Equals(column.Field, StringComparison.InvariantCultureIgnoreCase));
                        }

                        if (filterColumn != null)
                        {
                            if (!string.IsNullOrWhiteSpace(filterColumn.DisplayText))
                            {
                                exportFilters.Add(new KeyValuePair<string, string>(column.Title, filterColumn.DisplayText));
                            }

                            if (!string.IsNullOrWhiteSpace(filterColumn.Text))
                            {
                                FilterDescriptor newFilter = null;
                                if (filterColumn.IsMultiColumn)
                                {
                                    newFilter = new FilterDescriptor(column.Field, FilterOperator.Contains, "#" + filterColumn.Text + "#");
                                }
                                else
                                {
                                    var filterOperator = FilterOperator.IsEqualTo;
                                    if (filterColumn.Operator != null)
                                    {
                                        if (filterColumn.Operator == ">")
                                        {
                                            filterOperator = FilterOperator.IsGreaterThanOrEqualTo;
                                        }
                                        else if (filterColumn.Operator == "<")
                                        {
                                            filterOperator = FilterOperator.IsLessThanOrEqualTo;
                                        }
                                    }
                                    newFilter = new FilterDescriptor(column.Field, filterOperator, filterColumn.Text);
                                }
                                request.Filters.Add(newFilter);
                            }
                        }

                        if ((filterColumn == null || filterColumn.IsIncludeInGenericSearch) && !string.IsNullOrWhiteSpace(filterData.GenericSearchText))
                        {
                            if (column.Values != null && column.Values.Count > 0)
                            {
                                foreach (NameValue nameValue in column.Values.Where(x => x.Text.IndexOf(filterData.GenericSearchText, StringComparison.InvariantCultureIgnoreCase) >= 0))
                                {
                                    textFilter.FilterDescriptors.Add(new FilterDescriptor(column.Field, FilterOperator.IsEqualTo, nameValue.Value));
                                }
                            }
                            else
                            {
                                var item = typeof(T).GetProperties().FirstOrDefault(x => x.Name == column.Field);
                                if (item != null)
                                {
                                    if (item.PropertyType == typeof(string))
                                    {
                                        textFilter.FilterDescriptors.Add(new FilterDescriptor(column.Field, allFilter.Operator, filterData.GenericSearchText));
                                    }
                                }
                            }
                        }
                    }

                    if (textFilter.FilterDescriptors.Count > 0)
                    {
                        request.Filters.Add(textFilter);
                    }
                }
                else
                {
                    request.Filters.RemoveAt(0);
                }
            }
            return exportFilters;
        }

        protected FileStreamResult ExportGrid<T>(IList<T> exportData, string columnSettings, string selectedOptions, string title, IList<KeyValuePair<string, string>> filters = null, IList<string> rightAlignedColumns = null, bool supportsSorting = true)
        {
            dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
           
            DataSourceRequest request = new DataSourceRequest();
            request.Filters = new List<IFilterDescriptor>();
            request.Filters.Add(new FilterDescriptor("All", FilterOperator.Contains, options.exportFilters + "####" + columnSettings));
            if (supportsSorting && options.sort != null)
            {
                var sortDataList = JsonConvert.DeserializeObject<List<SortData>>(options.sort.ToString());
                request.Sorts = ConvertToSortDescriptor(sortDataList);
            }

            var exportFilters = SetUpFilters<T>(request);
            if (filters != null)
            {
                exportFilters.AddRange(filters);
            }

            exportData = exportData.ToDataSourceResult(request).Data.Cast<T>().ToList();
            if (options.format.ToString() == "xlsx")
            {
                return new ExcelExportTable<T>(columnSettings, selectedOptions, rightAlignedColumns).Export(exportData, title, exportFilters, Resources.Portrait);
            }
            else
            {
                return new PDFExportTable<T>(columnSettings, selectedOptions, rightAlignedColumns).Export(exportData, title, exportFilters, Resources.Portrait); ;
            }
        }

        #region Helpers
        protected ActionResult RedirectToLocal(string returnUrl, string controller, string action)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(action, controller);
        }
        #endregion

        protected DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        protected void SaveDocument(string serverFolderName, string fileName, Stream stream)
        {
            var physicalPath = Path.Combine(Server.MapPath("~/" + serverFolderName), fileName);
            using (var fileStream = System.IO.File.Create(physicalPath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }

        protected virtual ActionResult DownloadDocument(string serverFolderName, string filePath, string mode, string mediaTypeNames, bool isDownLoad = true)
        {
            string fullPath = Path.Combine(Server.MapPath("~/" + serverFolderName), filePath);
            if (!System.IO.File.Exists(fullPath))
            {
                return HttpNotFound();
            }

            if (mode == "action")
            {
                return Json(new { fileName = filePath }, JsonRequestBehavior.AllowGet);
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            if (isDownLoad)
            {
                return File(fileBytes, mediaTypeNames, filePath.Substring(filePath.IndexOf("_") + 1));
            }
            else
            {
                return File(fileBytes, mediaTypeNames);
            }
        }

        protected List<SortDescriptor> ConvertToSortDescriptor(List<SortData> sortData)
        {
            List<SortDescriptor> sortDescriptors = new List<SortDescriptor>();
            foreach (var sortItem in sortData)
            {
                sortDescriptors.Add(new SortDescriptor(sortItem.Field, sortItem.Dir.ToLower() == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending));
            }
            return sortDescriptors;
        }

        protected DateTime ConvertDashBoardPeriodToDate(DateTime toDate, string period)
        {
            switch(period)
            {
                case "1Week":
                    return toDate.AddDays(-7);
                case "2Week":
                    return toDate.AddDays(-14);
                case "3Week":
                    return toDate.AddDays(-21);
                case "4Week":
                    return toDate.AddDays(-28);
                case "1Month":
                    return toDate.AddMonths(-1);
                case "2Month":
                    return toDate.AddMonths(-2);
                case "6Month":
                    return toDate.AddMonths(-6);
                case "1Year":
                    return toDate.AddYears(-1);
            }
            return toDate;
        }
    }
}