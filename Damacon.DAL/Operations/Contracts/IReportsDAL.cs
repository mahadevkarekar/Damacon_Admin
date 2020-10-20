//using Damacon.DAL.Database.EF;
//using System;

//namespace Damacon.DAL.Operations.Contracts
//{
//    public interface IReportsDAL
//    {
//        GenericActionResult<StoreIncome> GetDailyIncomeReportData(DateTime selectedDate, long companyID, long storeID);

//        GenericActionResult<StoreIncome> GetMonthlyIncomeReportData(int year, int month, long storeID);

//        GenericActionResult<EmployeesWorkedHours> GetEmployeesWorkedHoursReportData(DateTime from, DateTime to, long companyID, long employeeID);

//        GenericActionResult<WorkedHour> GetEmployeesMonthlyWorkedHoursByStoreReportData(int year, int month, long companyID, long storeID);

//        GenericActionResult<StoreIncome> GetShoppingCenterReportData(DateTime from, DateTime to, long companyID, long storeID);

//        GenericActionResult<IncomeComparison> GetIncomeComparisonReportData(int yearCurrent, int weekCurrent, int yearPrevious, int weekPrevious, long companyID, long storeID);

//        GenericActionResult<IncomeComparison> GetComparisonReportData(DateTime currentPeriodStartDay, DateTime currentPeriodEndDay, DateTime previousPeriodStartDay, DateTime previousPeriodEndDay,
//            long companyID, long storeID);

//        GenericActionResult<ClassificaDataItem> GetClassificaReportData(int year, int month);

//        GenericActionResult<StoreSaleByMonthDataItem> GetComparisonByYearReportData(long companyID, long brandID, long storeID, int numberOfYears, int lastYear);

//        GenericActionResult<WorkedHour> GetReport14WorkedHourData(long storeId, DateTime fromDate, DateTime toDate);

//        GenericActionResult<Sale> GetReport14SalesData(long storeId, DateTime fromDate, DateTime toDate);

//        GenericActionResult<DashBoardChartData> GetDashBoardDailyGlobalIncome(DateTime fromDate, DateTime toDate, int years);

//        GenericActionResult<DashBoardChartData> GetDashBoardGlobalIncome(DateTime fromDate, DateTime toDate, string groupBy);

//        GenericActionResult<DashBoardChartData> GetDashBoardYearIncome(string groupBy, long? groupId, int years);
//    }
//}
