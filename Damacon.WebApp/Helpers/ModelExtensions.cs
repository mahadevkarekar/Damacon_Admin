using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Damacon.WebApp.Helpers
{
    public static class ModelExtensions
    {
        public static IList<ApplicationLink> GetUserApplicationLinksForGroup(this UserLite user, string groupName)
        {
            IList<ApplicationLink> result = new List<ApplicationLink>();
            if (user != null && user.UserType.ApplicationLinkUserTypePermissions != null)
            {
                result = user.UserType.ApplicationLinkUserTypePermissions
                    .Where(x => x.ApplicationLink != null && !x.ApplicationLink.Disable &&  string.Equals(x.ApplicationLink.GroupName, groupName, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.ApplicationLink).OrderBy(x => x.DisplayOrder).ToList();
            }
            return result;
        }

        //public static IList<Models.Report14DataItem> ConvertToReport14ParentDataItem(this IList<WorkedHour> workedHours, IList<Sale> salesData)
        //{
        //    IList<Models.Report14DataItem> result = new List<Models.Report14DataItem>();
        //    var models = ModelMapper.Instance.Mapper.Map<List<WorkedHourModel>>(workedHours);
        //    var employeeIds = workedHours.Where(x => !x.Employee.IsWindowsDresser).Select(x => x.EmployeeID).Distinct();
        //    foreach (long employeeID in employeeIds)
        //    {
        //        var modelsOfCurrentEmployee = models.Where(x => x.EmployeeID == employeeID);
        //        Report14DataItem report14DataItem = new Models.Report14DataItem();
        //        report14DataItem.EmployeeID = employeeID;
        //        report14DataItem.EmployeeName = workedHours.First(x => x.EmployeeID == employeeID).Employee.FullName;
        //        report14DataItem.Sold= modelsOfCurrentEmployee.Where(x => x.Sold.HasValue).Sum(x => x.Sold.Value);
        //        report14DataItem.WorkedHour = modelsOfCurrentEmployee.Sum(x => x.TotalReportHours);
        //        report14DataItem.Report14DataItems = new List<Models.Report14DataItem>();
        //        foreach (var modelsOfCurrentEmployeeForDate in modelsOfCurrentEmployee.OrderBy(x => x.WorkedDate))
        //        {
        //            Models.Report14DataItem report14DataItemSub = new Models.Report14DataItem();
        //            report14DataItemSub.WorkedDate = modelsOfCurrentEmployeeForDate.WorkedDate;
        //            report14DataItemSub.EmployeeID = employeeID;
        //            report14DataItemSub.EmployeeName = report14DataItem.EmployeeName;
        //            report14DataItemSub.Sold = modelsOfCurrentEmployeeForDate.Sold ?? 0 ;
        //            report14DataItemSub.WorkedHour = modelsOfCurrentEmployeeForDate.TotalReportHours;
        //            report14DataItem.Report14DataItems.Add(report14DataItemSub);
        //        }
        //        result.Add(report14DataItem);
        //    }

        //    result = result.OrderByDescending(x => x.IncomeToHour).ToList();
        //    Report14DataItem report14DataItemOther = GetReport14OtherData(workedHours, salesData);
        //    if (report14DataItemOther != null)
        //    {
        //        report14DataItemOther.Report14DataItems = new List<Models.Report14DataItem>();
        //        foreach (Sale sale in salesData)
        //        {
        //            var workedHourDataForCurrentDate = workedHours.Where(x => x.WorkedDate == sale.SalesDate);
        //            Report14DataItem report14DataItemOtherSub = GetReport14OtherData(workedHourDataForCurrentDate, new List<Sale> { sale });
        //            if (report14DataItemOtherSub != null)
        //            {
        //                report14DataItemOtherSub.WorkedDate = sale.SalesDate;
        //                report14DataItemOther.Report14DataItems.Add(report14DataItemOtherSub);
        //            }
        //        }
        //        result.Add(report14DataItemOther);
        //    }
        //    return result;
        //}

        //private static Report14DataItem GetReport14OtherData(IEnumerable<WorkedHour> workedHours, IList<Sale> salesData)
        //{
        //    var totalWorkedSalesValue = workedHours.Where(x => x.Sold.HasValue && !x.Employee.IsWindowsDresser).Sum(x => x.Sold.Value);
        //    var totalSaleSalesValue = salesData.Sum(x => x.TotalIncome);
        //    if (totalSaleSalesValue > totalWorkedSalesValue)
        //    {
        //        Report14DataItem report14DataItem = new Report14DataItem();
        //        report14DataItem.EmployeeID = -1;
        //        report14DataItem.EmployeeName = Resources.Other;
        //        report14DataItem.Sold = totalSaleSalesValue - totalWorkedSalesValue;
        //        report14DataItem.WorkedHour = ModelMapper.Instance.Mapper.Map<List<WorkedHourModel>>(workedHours.Where(x => x.Employee.IsWindowsDresser)).Sum(x => x.TotalReportHours);
        //        return report14DataItem;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}