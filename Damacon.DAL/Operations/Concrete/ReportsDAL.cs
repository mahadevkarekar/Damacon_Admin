//using Damacon.DAL.Operations.Contracts;
//using System;
//using System.Linq;
//using Damacon.DAL.Database.EF;
//using System.Data.Entity;
//using Damacon.DAL.i18n;
//using System.Collections.Generic;
//using System.Globalization;
//using Damacon.DAL.Utils;

//namespace Damacon.DAL.Operations.Concrete
//{
//    internal class ReportsDAL : IReportsDAL
//    {
//        public GenericActionResult<StoreIncome> GetDailyIncomeReportData(DateTime selectedDate, long companyID, long storeID)
//        {
//            GenericActionResult<StoreIncome> result = new GenericActionResult<StoreIncome>();
//            result.Result = new List<StoreIncome>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {

//                    var stores = GetStores(context, companyID, 0, storeID);

//                    var salesDataForSelectedDate = context.Sales.Where(x => DbFunctions.TruncateTime(x.SalesDate) == DbFunctions.TruncateTime(selectedDate)).ToList();
//                    var workerHourDataForSelectedDate = context.WorkedHours.Where(x => DbFunctions.TruncateTime(x.WorkedDate) == DbFunctions.TruncateTime(selectedDate)).ToList();
//                    foreach (Store store in stores)
//                    {
//                        StoreIncome storeIncome = new StoreIncome();
//                        storeIncome.StoreName = store.StoreName;
//                        storeIncome.CompanyID = store.CompanyID;
//                        storeIncome.CompanyName = store.Company.CompanyName;
//                        var saleData = salesDataForSelectedDate.FirstOrDefault(x => x.StoreID == store.ID);
//                        if (saleData != null)
//                        {
//                            storeIncome.Sale = saleData;
//                        }
//                        else
//                        {
//                            storeIncome.Sale = new Sale() { StoreID = store.ID };
//                        }

//                        var workerHourData = workerHourDataForSelectedDate.Where(x => x.StoreID == store.ID);
//                        foreach (var workHourItem in workerHourData)
//                        {
//                            storeIncome.TotalWorkedHours += GetWorkedTotalHours(workHourItem);
//                        }
//                        result.Result.Add(storeIncome);
//                    }
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<StoreIncome> GetMonthlyIncomeReportData(int year, int month, long storeID)
//        {
//            GenericActionResult<StoreIncome> result = new GenericActionResult<StoreIncome>();
//            result.Result = new List<StoreIncome>();
//            try
//            {
//                DateTime startDate = new DateTime(year, month, 1);
//                int numberOFDays = DateTime.DaysInMonth(year, month);
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    for(int currentDay = 1; currentDay <= numberOFDays; currentDay ++)
//                    {
//                        DateTime currentDate = new DateTime(year, month, currentDay);
//                        StoreIncome monthlyIncome = new StoreIncome();
//                        monthlyIncome.IncomeDate = currentDate;
//                        var salesDataForSelectedDate = context.Sales.FirstOrDefault(x => DbFunctions.TruncateTime(x.SalesDate) == DbFunctions.TruncateTime(currentDate) && x.StoreID == storeID);
//                        if (salesDataForSelectedDate != null)
//                        {
//                            monthlyIncome.Sale = salesDataForSelectedDate;
//                        }
//                        else
//                        {
//                            monthlyIncome.Sale = new Sale() { StoreID = storeID };
//                        }

//                        var workerHoursData = context.WorkedHours.Where(x => DbFunctions.TruncateTime(x.WorkedDate) == DbFunctions.TruncateTime(currentDate) && x.StoreID == storeID);
//                        foreach (var workerHourData in workerHoursData)
//                        {
//                            monthlyIncome.TotalWorkedHours += GetWorkedTotalHours(workerHourData);
//                        }

//                        monthlyIncome.BuyerData = context.SalesBuyers.Where(x => !x.Deleted && DbFunctions.TruncateTime(x.SalesDate) == DbFunctions.TruncateTime(currentDate) && x.StoreID == storeID).ToList();
//                        result.Result.Add(monthlyIncome);
//                    }
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<EmployeesWorkedHours> GetEmployeesWorkedHoursReportData(DateTime from, DateTime to, long companyID, long employeeID)
//        {
//            GenericActionResult<EmployeesWorkedHours> result = new GenericActionResult<EmployeesWorkedHours>();
//            result.Result = new List<EmployeesWorkedHours>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var employees = GetEmployees(context, companyID, employeeID);
//                    foreach (Employee employee in employees)
//                    {
//                        EmployeesWorkedHours employeesWorkedHours = new EmployeesWorkedHours();
//                        employeesWorkedHours.EmployeeID = employee.ID;
//                        employeesWorkedHours.EmployeeName = string.Format("{0} {1}", employee.FirstName, employee.Surname);
//                        employeesWorkedHours.WorkedHours = new List<WorkedHour>();
//                        var workedHoursDataForSelectedDate = context.WorkedHours.Include(x => x.WorkedHourTimes).Include(x => x.WorkedHourTimes.Select(y => y.WorkerTimeType)).Where(x =>
//                                                                                DbFunctions.TruncateTime(x.WorkedDate) >= DbFunctions.TruncateTime(from) &&
//                                                                                DbFunctions.TruncateTime(x.WorkedDate) <= DbFunctions.TruncateTime(to) &&
//                                                                                x.EmployeeID == employee.ID).OrderBy(x => x.WorkedDate).ToList();
//                        employeesWorkedHours.WorkedHours.AddRange(workedHoursDataForSelectedDate);
//                        if (!employee.Disable || employeesWorkedHours.WorkedHours.Count > 0)
//                        {
//                            result.Result.Add(employeesWorkedHours);
//                        }
//                    }
                        
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<WorkedHour> GetEmployeesMonthlyWorkedHoursByStoreReportData(int year, int month, long companyID, long storeID)
//        {
//            GenericActionResult<WorkedHour> result = new GenericActionResult<WorkedHour>();
//            result.Result = new List<WorkedHour>();
//            try
//            {
//                DateTime from = new DateTime(year, month, 1);
//                DateTime to = new DateTime(year, month, DateTime.DaysInMonth(year, month));
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var stores = GetStores(context, companyID, 0, storeID, true);
//                    foreach (Store store in stores)
//                    {
//                        var workedHoursDataForSelectedDate = context.WorkedHours.Include(x => x.WorkedHourTimes).Include(x => x.WorkedHourTimes.Select(y => y.WorkerTimeType)).Where(x =>
//                                                                                DbFunctions.TruncateTime(x.WorkedDate) >= DbFunctions.TruncateTime(from) &&
//                                                                                DbFunctions.TruncateTime(x.WorkedDate) <= DbFunctions.TruncateTime(to) &&
//                                                                                x.StoreID == store.ID).OrderBy(x => x.WorkedDate).ToList();
//                        workedHoursDataForSelectedDate.ForEach(x => x.WorkedHourTimes = x.WorkedHourTimes.ToList());
//                        workedHoursDataForSelectedDate.ForEach(x => x.WorkedHourTimes.ToList().ForEach(y => y.WorkerTimeType = y.WorkerTimeType));
//                        result.Result.AddRange(workedHoursDataForSelectedDate);
//                    }

//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<StoreIncome> GetShoppingCenterReportData(DateTime from, DateTime to, long companyID, long storeID)
//        {
//            GenericActionResult<StoreIncome> result = new GenericActionResult<StoreIncome>();
//            result.Result = new List<StoreIncome>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var stores = GetStores(context, companyID, 0, storeID);
//                    foreach (Store store in stores)
//                    {
//                        var salesDataForSelectedDate = context.Sales.Where(x =>
//                                                                                DbFunctions.TruncateTime(x.SalesDate) >= DbFunctions.TruncateTime(from) &&
//                                                                                DbFunctions.TruncateTime(x.SalesDate) <= DbFunctions.TruncateTime(to) &&
//                                                                                x.StoreID == store.ID).OrderBy(x => x.SalesDate).ToList();
//                        result.Result.AddRange(salesDataForSelectedDate.Select(x => new StoreIncome() {StoreName = store.StoreName,Sale = x, CompanyID = store.CompanyID, CompanyName = store.Company.CompanyName }));
//                    }

//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<IncomeComparison> GetIncomeComparisonReportData(int yearCurrent, int weekCurrent, int yearPrevious, int weekPrevious, long companyID, long storeID)
//        {
//            GenericActionResult<IncomeComparison> incomeComparisonResult = new GenericActionResult<IncomeComparison>();
//            incomeComparisonResult.Result = new List<IncomeComparison>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var stores = GetStores(context, companyID, 0, storeID);
//                    DateTime currentPeriodStartDay = DateTimeExtensions.FirstDateOfWeekISO8601(yearCurrent, weekCurrent);
//                    DateTime currentPeriodEndDay = currentPeriodStartDay.AddDays(6);
//                    DateTime previousPeriodStartDay = DateTimeExtensions.FirstDateOfWeekISO8601(yearPrevious, weekPrevious);
//                    DateTime previousPeriodEndDay = previousPeriodStartDay.AddDays(6);

//                    foreach (Store store in stores)
//                    {
//                        incomeComparisonResult.Result.AddRange(GetIncomeComparisons(context, store, currentPeriodStartDay, currentPeriodEndDay, previousPeriodStartDay, previousPeriodEndDay, false));
//                    }
//                    incomeComparisonResult.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                incomeComparisonResult.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return incomeComparisonResult;
//        }

//        public GenericActionResult<IncomeComparison> GetComparisonReportData(DateTime currentPeriodStartDay, DateTime currentPeriodEndDay, DateTime previousPeriodStartDay, DateTime previousPeriodEndDay,
//            long companyID, long storeID)
//        {
//            GenericActionResult<IncomeComparison> incomeComparisonResult = new GenericActionResult<IncomeComparison>();
//            incomeComparisonResult.Result = new List<IncomeComparison>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var stores = GetStores(context, companyID, 0, storeID);
//                    foreach (Store store in stores)
//                    {
//                        IncomeComparison incomeComparison = new IncomeComparison();
//                        incomeComparison.Init(store);
//                        var storeResult = GetIncomeComparisons(context, store, currentPeriodStartDay, currentPeriodEndDay, previousPeriodStartDay, previousPeriodEndDay, true, true);
//                        storeResult.ForEach(x => incomeComparison.Add(x));
//                        incomeComparisonResult.Result.Add(incomeComparison);
//                    }
//                    incomeComparisonResult.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                incomeComparisonResult.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return incomeComparisonResult;
//        }

//        public GenericActionResult<ClassificaDataItem> GetClassificaReportData(int year, int month)
//        {
//            GenericActionResult<ClassificaDataItem> classificaResult = new GenericActionResult<ClassificaDataItem>();
//            classificaResult.Result = new List<ClassificaDataItem>();
//            try
//            {
//                var weeksBetweenDateRange = DateTimeExtensions.GetWeeksOfTheMonth(year, month);
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    var stores = GetStores(context, 0, 0, 0);
//                    foreach (Store store in stores)
//                    {
//                        ClassificaDataItem classificaDataItem = new ClassificaDataItem();
//                        classificaDataItem.StoreID = store.ID;
//                        classificaDataItem.StoreName = store.StoreName;
//                        classificaDataItem.WeekDataItems = new List<ClassificaWeekDataItem>();
//                        foreach (var week in weeksBetweenDateRange)
//                        {
//                            ClassificaWeekDataItem classificaWeekDataItem = new ClassificaWeekDataItem();
//                            classificaWeekDataItem.WeekNumber = week.Item1;

//                            DateTime currentPeriodStartDay = DateTimeExtensions.FirstDateOfWeekISO8601(week.Item2, week.Item1);
//                            DateTime currentPeriodEndDay = currentPeriodStartDay.AddDays(6);
//                            DateTime previousPeriodStartDay = DateTimeExtensions.FirstDateOfWeekISO8601(week.Item2 - 1, week.Item1);
//                            DateTime previousPeriodEndDay = previousPeriodStartDay.AddDays(6);
//                            var storeResult = GetIncomeComparisons(context, store, currentPeriodStartDay, currentPeriodEndDay, previousPeriodStartDay, previousPeriodEndDay, false, true);
//                            decimal currentYearSale = 0;
//                            decimal previousYearSale = 0;
//                            foreach (var salesData in storeResult)
//                            {
//                                if (salesData.SalePeriod1.Sale.IsIncludeInClassifica)
//                                {
//                                    currentYearSale += salesData.SalePeriod1.Sale.TotalIncome;
//                                }
//                                if (salesData.SalePeriod2.Sale.IsIncludeInClassifica)
//                                {
//                                    previousYearSale += salesData.SalePeriod2.Sale.TotalIncome;
//                                }
//                            }
//                            classificaWeekDataItem.ProfitPercent = Math.Round(previousYearSale == 0 ? 0 : currentYearSale / previousYearSale * 100 , 2);
//                            classificaDataItem.WeekDataItems.Add(classificaWeekDataItem);
//                        }
//                        classificaResult.Result.Add(classificaDataItem);
//                    }

//                    foreach (var week in weeksBetweenDateRange)
//                    {
//                        List<NumberRank> numberRanks = classificaResult.Result.SelectMany(x => x.WeekDataItems.Where(y => y.WeekNumber == week.Item1).Select(y => y.ProfitPercent)).Distinct()
//                            .OrderByDescending(n => n).Select((n, i) => new NumberRank(n, i + 1)).ToList();

//                        foreach (var item in classificaResult.Result)
//                        {
//                            ClassificaWeekDataItem weekData = item.WeekDataItems.FirstOrDefault(y => y.WeekNumber == week.Item1);
//                            if (weekData != null)
//                            {
//                                int rank = numberRanks.First(x => x.Number == weekData.ProfitPercent).Rank;
//                                weekData.Points = GetPointsByRank(rank);
//                            }
//                        }
//                    }
//                    classificaResult.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                classificaResult.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return classificaResult;
//        }

//        public GenericActionResult<StoreSaleByMonthDataItem> GetComparisonByYearReportData(long companyID, long brandID, long storeID, int numberOfYears, int lastYear)
//        {
//            GenericActionResult<StoreSaleByMonthDataItem> getComparisonByYearReportData = new GenericActionResult<StoreSaleByMonthDataItem>();
//            getComparisonByYearReportData.Result = new List<StoreSaleByMonthDataItem>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    for (int yearCounter = 0; yearCounter <= numberOfYears; yearCounter++)
//                    {
//                        int year = lastYear - numberOfYears + yearCounter;

//                        var stores = GetStoresWithSalesData(context, companyID, brandID, storeID, year);
//                        foreach (Store store in stores)
//                        {
//                            StoreSaleByMonthDataItem yearReportDataItem = new StoreSaleByMonthDataItem();
//                            yearReportDataItem.StoreID = store.ID;
//                            yearReportDataItem.StoreName = store.StoreShortName;
//                            yearReportDataItem.Year = year;
//                            if (store.BrandDetail != null)
//                            {
//                                yearReportDataItem.BrandName = store.BrandDetail.Name;
//                                yearReportDataItem.BrandColor = store.BrandDetail.ColorCode;
//                            }
//                            yearReportDataItem.StoreSaleByMonthSubDataItems = new List<StoreSaleByMonthSubDataItem>();

//                            for (int month = 1; month <= 12; month++)
//                            {
//                                DateTime from = new DateTime(year, month, 1);
//                                int numberOfDays = DateTime.DaysInMonth(year, month);
//                                DateTime to = new DateTime(year, month, numberOfDays);

//                                if (to > DateTime.Now)
//                                {
//                                    continue;
//                                }

//                                StoreSaleByMonthSubDataItem yearReportMonthlySaleDataItem = new StoreSaleByMonthSubDataItem();
//                                yearReportMonthlySaleDataItem.Month = month;


//                                var salesDataForSelectedDate = context.Sales.Where(x =>
//                                                                                DbFunctions.TruncateTime(x.SalesDate) >= DbFunctions.TruncateTime(from) &&
//                                                                                DbFunctions.TruncateTime(x.SalesDate) <= DbFunctions.TruncateTime(to) &&
//                                                                                x.StoreID == store.ID).ToList();

//                                yearReportMonthlySaleDataItem.Income = salesDataForSelectedDate.Sum(x => x.TotalIncome);
//                                yearReportMonthlySaleDataItem.Pieces = salesDataForSelectedDate.Sum(x => x.TotalPieces);
//                                yearReportDataItem.StoreSaleByMonthSubDataItems.Add(yearReportMonthlySaleDataItem);

//                                if (yearCounter > 0)
//                                {
//                                    var previousYearItem = getComparisonByYearReportData.Result.FirstOrDefault(x => x.StoreID == store.ID && x.Year == year - 1);
//                                    if (previousYearItem != null)
//                                    {
//                                        var previousYearThisMonthItem = previousYearItem.StoreSaleByMonthSubDataItems.FirstOrDefault(x => x.Month == month);
//                                        if (previousYearThisMonthItem != null)
//                                        {
//                                            yearReportMonthlySaleDataItem.PreviousYearIncome = previousYearThisMonthItem.Income;
//                                        }
//                                    }
//                                }
//                            }

//                            getComparisonByYearReportData.Result.Add(yearReportDataItem);
//                        }
//                    }
//                    getComparisonByYearReportData.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                getComparisonByYearReportData.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return getComparisonByYearReportData;
//        }

//        public GenericActionResult<WorkedHour> GetReport14WorkedHourData(long storeId, DateTime fromDate, DateTime toDate)
//        {
//            GenericActionResult<WorkedHour> result = new GenericActionResult<WorkedHour>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    if (context.WorkedHours.Any(x => x.StoreID == storeId))
//                    {
//                        result.Result = context.WorkedHours.Include(x => x.Employee).Where(x => x.StoreID == storeId && 
//                        DbFunctions.TruncateTime(x.WorkedDate) >= DbFunctions.TruncateTime(fromDate) &&
//                        DbFunctions.TruncateTime(x.WorkedDate) <= DbFunctions.TruncateTime(toDate)
//                        ).ToList();
                        
//                        result.Result.ForEach(x => x.WorkedHourTimes = x.WorkedHourTimes.ToList());
//                        result.Result.ForEach(x => x.WorkedHourTimes.ToList().ForEach(y => y.WorkerTimeType = y.WorkerTimeType));
//                    }

//                    if (result.Result == null)
//                    {
//                        result.Result = new List<WorkedHour>();
//                    }

//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<Sale> GetReport14SalesData(long storeId, DateTime fromDate, DateTime toDate)
//        {
//            GenericActionResult<Sale> result = new GenericActionResult<Sale>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    if (context.Sales.Any(x => x.StoreID == storeId))
//                    {
//                        result.Result = context.Sales.Where(x => x.StoreID == storeId &&
//                        DbFunctions.TruncateTime(x.SalesDate) >= DbFunctions.TruncateTime(fromDate) &&
//                        DbFunctions.TruncateTime(x.SalesDate) <= DbFunctions.TruncateTime(toDate)
//                        ).ToList();
//                    }

//                    if (result.Result == null)
//                    {
//                        result.Result = new List<Sale>();
//                    }

//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<DashBoardChartData> GetDashBoardDailyGlobalIncome(DateTime fromDate, DateTime toDate, int years)
//        {
//            GenericActionResult<DashBoardChartData> result = new GenericActionResult<DashBoardChartData>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    DashBoardChartData data = new DashBoardChartData();
//                    var dateRange = Enumerable.Range(0, (toDate - fromDate).Days + 1).Select(d => fromDate.AddDays(d));
//                    data.DateRange = dateRange.Select(d => d.ToString("dd/MM/yyyy")).ToList();
//                    for (int yearNumber = 0; yearNumber < years; yearNumber++)
//                    {
//                        var dashBoardChartSeriesData = new DashBoardChartSeriesData();
//                        dashBoardChartSeriesData.SeriesID = yearNumber;
//                        dashBoardChartSeriesData.SeriesName = toDate.AddYears(-yearNumber).Year.ToString();
//                        dashBoardChartSeriesData.Data = new List<DashBoardChartSeriesDataItem>();

//                        DateTime currentFromDate = fromDate.AddYears(-yearNumber);
//                        DateTime currentToDate = toDate.AddYears(-yearNumber);
//                        var dataFromDB = context.usp_GetDashBoardDailyIncomeGraphData(currentFromDate, currentToDate).ToList();
//                        foreach(var date in dateRange)
//                        {
//                            var currentDate = date.AddYears(-yearNumber);
//                            var dataForCurrentDate = dataFromDB.FirstOrDefault(x => x.SalesDate.Value.Date == currentDate.Date);
//                            var dashBoardChartSeriesDataItem = new DashBoardChartSeriesDataItem();
//                            dashBoardChartSeriesDataItem.Value = dataForCurrentDate == null ? 0 : dataForCurrentDate.TotalIncome ?? 0;
//                            dashBoardChartSeriesData.Data.Add(dashBoardChartSeriesDataItem);
//                        }
//                        data.DashBoardChartSeriesList.Add(dashBoardChartSeriesData);
//                    }
//                    result.SetSingleResult(data);
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<DashBoardChartData> GetDashBoardGlobalIncome(DateTime fromDate, DateTime toDate, string groupBy)
//        {
//            GenericActionResult<DashBoardChartData> result = new GenericActionResult<DashBoardChartData>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    DashBoardChartData data = new DashBoardChartData();
//                    data.DateRange = Enumerable.Range(0, (toDate - fromDate).Days + 1).Select(d => fromDate.AddDays(d).ToString("dd/MM/yyyy")).ToList();
//                    var dashBoardChartSeriesData = new DashBoardChartSeriesData();
//                    dashBoardChartSeriesData.SeriesID = 1;
//                    dashBoardChartSeriesData.SeriesName = DateTime.Now.Year.ToString();
//                    dashBoardChartSeriesData.Data = new List<DashBoardChartSeriesDataItem>();
//                    var dataFromDB = context.usp_GetDashBoardGlobalIncomeGraphData(fromDate, toDate, groupBy);
//                    foreach (var dataItem in dataFromDB)
//                    {
//                        var dashBoardChartSeriesDataItem = new DashBoardChartSeriesDataItem();
//                        dashBoardChartSeriesDataItem.Category = dataItem.GroupName;
//                        dashBoardChartSeriesDataItem.Value = dataItem.GroupIncome ?? 0;
//                        dashBoardChartSeriesData.Data.Add(dashBoardChartSeriesDataItem);
//                    }
//                    data.DashBoardChartSeriesList.Add(dashBoardChartSeriesData);
//                    result.SetSingleResult(data);
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult<DashBoardChartData> GetDashBoardYearIncome(string groupBy, long? groupId, int years)
//        {
//            GenericActionResult<DashBoardChartData> result = new GenericActionResult<DashBoardChartData>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    DashBoardChartData data = new DashBoardChartData();
//                    var dateRange = Enumerable.Range(1, 12).Select(d => new DateTime(DateTime.Now.Year, d, 1)).ToList();
//                    data.DateRange = dateRange.Select(d => d.ToString("dd/MM/yyyy")).ToList();
//                    for (int yearNumber = 0; yearNumber < years; yearNumber++)
//                    {
//                        var dashBoardChartSeriesData = new DashBoardChartSeriesData();
//                        dashBoardChartSeriesData.SeriesID = yearNumber;
//                        dashBoardChartSeriesData.SeriesName = DateTime.Now.AddYears(-yearNumber).Year.ToString();
//                        dashBoardChartSeriesData.Data = new List<DashBoardChartSeriesDataItem>();

//                        var dataFromDB = context.usp_GetDashBoardYearIncomeGraphData(DateTime.Now.AddYears(-yearNumber).Year, groupBy, groupId).ToList();
//                        foreach (var date in dateRange)
//                        {
//                            var dataForCurrentDate = dataFromDB.FirstOrDefault(x => x.SalesMonth == date.Month);
//                            var dashBoardChartSeriesDataItem = new DashBoardChartSeriesDataItem();
//                            dashBoardChartSeriesDataItem.Value = dataForCurrentDate == null ? 0 : dataForCurrentDate.GroupIncome ?? 0;
//                            dashBoardChartSeriesData.Data.Add(dashBoardChartSeriesDataItem);
//                        }
//                        data.DashBoardChartSeriesList.Add(dashBoardChartSeriesData);
//                    }
//                    result.SetSingleResult(data);
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        private IList<Store> GetStores(DamaconEntities context, long companyID, long brandId, long storeID, bool getOfficeStore = false)
//        {
//            var stores = context.Stores.Include(x => x.Company).Where(x => !x.Deleted && !x.Disable);
//            if (companyID > 0)
//            {
//                stores = stores.Where(x => x.CompanyID == companyID);
//            }

//            if (brandId > 0)
//            {
//                stores = stores.Where(x => x.BrandID == brandId);
//            }

//            if (storeID > 0)
//            {
//                stores = stores.Where(x => x.ID == storeID);
//            }

//            if (!getOfficeStore)
//            {
//                stores = stores.Where(x => !x.IsOffice);
//            }

//            return stores.ToList();
//        }

//        private IList<Store> GetStoresWithSalesData(DamaconEntities context, long companyID, long brandId, long storeID, int year, bool getOfficeStore = false)
//        {
//            var stores = context.Sales.Where(x => x.SalesDate.Year == year).Select(x => x.Store).Include(x => x.Company).Where(x => !x.Deleted && !x.Disable).Distinct();
//            if (companyID > 0)
//            {
//                stores = stores.Where(x => x.CompanyID == companyID);
//            }

//            if (brandId > 0)
//            {
//                stores = stores.Where(x => x.BrandID == brandId);
//            }

//            if (storeID > 0)
//            {
//                stores = stores.Where(x => x.ID == storeID);
//            }

//            if (!getOfficeStore)
//            {
//                stores = stores.Where(x => !x.IsOffice);
//            }

//            return stores.ToList();
//        }


//        private IList<Employee> GetEmployees(DamaconEntities context, long companyID, long employeeID)
//        {
//            var employees = context.Employees.Where(x => !x.Deleted);
//            if (companyID > 0)
//            {
//                employees = context.EmployeeCompanyContracts.Where(x => x.CompanyID == companyID && !x.Employee.Deleted).Select(x => x.Employee);
//            }

//            if (employeeID > 0)
//            {
//                employees = employees.Where(x => x.ID == employeeID);
//            }

//            return employees.ToList();
//        }

//        private List<IncomeComparison> GetIncomeComparisons(DamaconEntities context, Store store, 
//            DateTime currentPeriodStartDay, DateTime currentPeriodEndDay, DateTime previousPeriodStartDay, DateTime previousPeriodEndDay,
//            bool getWorkerHourData, bool getDifferentialIncome = false)                                                                            
//        {                                                                                                    
//            var result = new List<IncomeComparison>();
//            int daysCountCurrent = currentPeriodEndDay.Subtract(currentPeriodStartDay).Days + 1;
//            int daysCountPrevious = previousPeriodEndDay.Subtract(previousPeriodStartDay).Days + 1;
//            int daysCount = daysCountCurrent > daysCountPrevious ? daysCountCurrent : daysCountPrevious;
//            var salesDataForCurrentPeriod = context.Sales.Where(x =>
//                                                                        DbFunctions.TruncateTime(x.SalesDate) >= DbFunctions.TruncateTime(currentPeriodStartDay) &&
//                                                                        DbFunctions.TruncateTime(x.SalesDate) <= DbFunctions.TruncateTime(currentPeriodEndDay) &&
//                                                                        x.StoreID == store.ID).OrderBy(x => x.SalesDate).ToList();
//            var salesDataForPreviousPeriod = context.Sales.Where(x =>
//                                                                    DbFunctions.TruncateTime(x.SalesDate) >= DbFunctions.TruncateTime(previousPeriodStartDay) &&
//                                                                    DbFunctions.TruncateTime(x.SalesDate) <= DbFunctions.TruncateTime(previousPeriodEndDay) &&
//                                                                    x.StoreID == store.ID).OrderBy(x => x.SalesDate).ToList();

//            IList<WorkedHour> workerHourDataForCurrentPeriod = null;
//            IList<WorkedHour> workerHourForPreviousPeriod = null;
//            if (getWorkerHourData)
//            {
//                workerHourDataForCurrentPeriod = context.WorkedHours.Include(x => x.WorkedHourTimes).Include(x => x.WorkedHourTimes.Select(y => y.WorkerTimeType)).Where(x =>
//                                                                   DbFunctions.TruncateTime(x.WorkedDate) >= DbFunctions.TruncateTime(currentPeriodStartDay) &&
//                                                                   DbFunctions.TruncateTime(x.WorkedDate) <= DbFunctions.TruncateTime(currentPeriodEndDay) &&
//                                                                   x.StoreID == store.ID).OrderBy(x => x.WorkedDate).ToList();
//                workerHourForPreviousPeriod = context.WorkedHours.Include(x => x.WorkedHourTimes).Include(x => x.WorkedHourTimes.Select(y => y.WorkerTimeType)).Where(x =>
//                                                                        DbFunctions.TruncateTime(x.WorkedDate) >= DbFunctions.TruncateTime(previousPeriodStartDay) &&
//                                                                        DbFunctions.TruncateTime(x.WorkedDate) <= DbFunctions.TruncateTime(previousPeriodEndDay) &&
//                                                                        x.StoreID == store.ID).OrderBy(x => x.WorkedDate).ToList();
//            }

//            List<Differential> differentialForPreviousPeriod = null;
//            if (getDifferentialIncome)
//            {
//                var weeksBetweenDateRange = DateTimeExtensions.GetWeeksBetweenDateRange(previousPeriodStartDay, previousPeriodEndDay);
//                var weekNumbers = weeksBetweenDateRange.Select(x => x.Item1);
//                differentialForPreviousPeriod = context.Differentials.Where(x => x.StoreID == store.ID && weekNumbers.Any(y => y == x.WeekNumber)).ToList();
//                differentialForPreviousPeriod = differentialForPreviousPeriod.Where(x => weeksBetweenDateRange.Any(y => y.Item1 == x.WeekNumber && y.Item2 == x.Year)).ToList();
//            }


//            for (int dayCounter = 0; dayCounter < daysCount; dayCounter++)
//            {
//                DateTime currentPeriodDateTime = currentPeriodStartDay.AddDays(dayCounter);
//                IncomeComparison incomeComparison = new IncomeComparison();
//                incomeComparison.StoreID = store.ID;
//                incomeComparison.IncomeDatePeriod1 = currentPeriodStartDay.AddDays(dayCounter);
//                incomeComparison.SalePeriod1 = new StoreIncome();
//                incomeComparison.SalePeriod1.StoreName = store.StoreName;
//                incomeComparison.SalePeriod1.CompanyID = store.CompanyID;
//                incomeComparison.SalePeriod1.CompanyName = store.Company.CompanyName;
//                incomeComparison.SalePeriod1.Sale = salesDataForCurrentPeriod.FirstOrDefault(x => x.SalesDate.Date == currentPeriodDateTime.Date);
//                incomeComparison.SalePeriod1.Sale = incomeComparison.SalePeriod1.Sale ?? new Sale() { IsIncludeInClassifica = true };
//                incomeComparison.SalePeriod1.BuyerData = context.SalesBuyers.Where(x => !x.Deleted && DbFunctions.TruncateTime(x.SalesDate) == DbFunctions.TruncateTime(currentPeriodDateTime) && x.StoreID == store.ID).ToList();
//                if (getWorkerHourData)
//                {
//                    var selectedWorkerHour = workerHourDataForCurrentPeriod.Where(x => x.WorkedDate.Date == currentPeriodStartDay.AddDays(dayCounter).Date);
//                    foreach (WorkedHour workHour in selectedWorkerHour)
//                    {
//                        incomeComparison.SalePeriod1.TotalWorkedHours += GetWorkedTotalHours(workHour);
//                    }
//                }

//                incomeComparison.IncomeDatePeriod2 = previousPeriodStartDay.AddDays(dayCounter);
//                incomeComparison.SalePeriod2 = new StoreIncome();
//                incomeComparison.SalePeriod2.StoreName = store.StoreName;
//                incomeComparison.SalePeriod2.CompanyID = store.CompanyID;
//                incomeComparison.SalePeriod2.CompanyName = store.Company.CompanyName;
//                incomeComparison.SalePeriod2.Sale = salesDataForPreviousPeriod.FirstOrDefault(x => x.SalesDate.Date == previousPeriodStartDay.AddDays(dayCounter).Date);
//                incomeComparison.SalePeriod2.Sale = incomeComparison.SalePeriod2.Sale ?? new Sale() { IsIncludeInClassifica = true };
//                if (getWorkerHourData)
//                {
//                    var selectedWorkerHour = workerHourForPreviousPeriod.Where(x => x.WorkedDate.Date == previousPeriodStartDay.AddDays(dayCounter).Date);
//                    foreach (WorkedHour workHour in selectedWorkerHour)
//                    {
//                        incomeComparison.SalePeriod2.TotalWorkedHours += GetWorkedTotalHours(workHour);
//                    }
//                }

//                if (getDifferentialIncome)
//                {
//                    DateTime currentDate = previousPeriodStartDay.AddDays(dayCounter);
//                    int weekNumber = DateTimeExtensions.GetWeekFromDate(currentDate);
//                    int year = currentDate.Month == 1 && weekNumber > 50 ? currentDate.Year - 1 : currentDate.Year;
//                    Differential differential = differentialForPreviousPeriod.FirstOrDefault(x => x.WeekNumber == weekNumber && x.Year == year);
//                    if (differential != null)
//                    {
//                        incomeComparison.DifferentialIncome = incomeComparison.SalePeriod2.Sale.TotalIncome * (100 + Convert.ToDecimal(differential.DifferentialPercent)) / 100;
//                    }
//                    else
//                    {
//                        incomeComparison.DifferentialIncome = incomeComparison.SalePeriod2.Sale.TotalIncome;
//                    }
//                }
//                else
//                {
//                    incomeComparison.DifferentialIncome = incomeComparison.SalePeriod2.Sale.TotalIncome;
//                }
//                result.Add(incomeComparison);
//            }
//            return result;
//        }

//        private decimal GetWorkedTotalHours(WorkedHour workerHourData)
//        {
//            decimal totalHours = 0;
//            if (workerHourData != null && workerHourData.WorkedHourTimes != null)
//            {
//                foreach (var workerType in workerHourData.WorkedHourTimes.Where(x => x.WorkerTimeType.InStatistic))
//                {
//                    totalHours += workerType.WorkerTimeType.HalfHour == true ? (decimal)0.5 : (decimal)1;
//                }
//            }
//            return totalHours;
//        }

//        private int GetPointsByRank(int rank)
//        {
//            if (rank < 6)
//            {
//                return 1;
//            }
//            else if (rank < 13)
//            {
//                return 2;
//            }
//            return 3;
//        }

//        private class NumberRank
//        {
//            public decimal Number { get; set; }
//            public int Rank { get; set; }

//            public NumberRank(decimal number, int rank)
//            {
//                Number = number;
//                Rank = rank;
//            }
//        }
//    }
//}
