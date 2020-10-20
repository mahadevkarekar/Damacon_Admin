using Damacon.DAL;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;
using Kendo.Mvc.Extensions;
using Damacon.SharedWeb.Helpers;

namespace Damacon.ClientWebApp.Controllers
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


            // Modify current thread's cultures   
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture
            return RedirectToLocal(null, "Account", "Login");
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

        //protected void PopulateBrands()
        //{
        //    var result = DALFactory.GetDALObject<IStoreDAL>().GetBrandsOfActiceStores();
        //    if (result.IsSuccess)
        //    {
        //        ViewData["Brands"] = result.Result.OrderBy(x => x.Name).ToList();
        //    }
        //}

        //protected void PopulateStoreCities()
        //{
        //    var result = DALFactory.GetDALObject<IStoreDAL>().GetStoreCities();
        //    if (result.IsSuccess)
        //    {
        //        ViewData["StoreCities"] = result.Result.OrderBy(x => x).ToList();
        //    }
        //}

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
                    var columnsData = JsonConvert.DeserializeObject<IList<ExportColumnSettingsEx>>(HttpUtility.UrlDecode(splitValues[1]));
                    request.Filters.RemoveAt(0);

                    CompositeFilterDescriptor textFilter = new CompositeFilterDescriptor();
                    textFilter.LogicalOperator = FilterCompositionLogicalOperator.Or;

                    columnsData.RemoveAt(columnsData.Count - 1);
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
                        else if (!string.IsNullOrWhiteSpace(filterData.GenericSearchText))
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
    }
}