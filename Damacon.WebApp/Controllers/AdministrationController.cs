using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using Damacon.WebApp.Models;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Damacon.WebApp.Controllers
{
    public class AdministrationController : BaseController
    {
       

    #region ManageHolidays

    [HttpGet]
    public ActionResult ManageHolidays()
    {
      PopulateYears(2019, iLastYear: System.DateTime.Now.Year + 2);
      return View();
    }

    [HttpPost]
    public FileStreamResult ExportHolidaysGrid(string columnSettings, string selectedOptions)
    {
      List<ExportColumnSettingsEx> exportColumnSettings = new List<ExportColumnSettingsEx>();
      exportColumnSettings.Add(new ExportColumnSettingsEx() { Field = "MonthDescription", Title = Resources.Month });
      exportColumnSettings.Add(new ExportColumnSettingsEx() { Field = "HolidayDate", Title = Resources.Date });
      exportColumnSettings.Add(new ExportColumnSettingsEx() { Field = "Notes", Title = Resources.Notes, ExcelColumnWidth = 600 });
      exportColumnSettings.Add(new ExportColumnSettingsEx() { Field = "Erased"});

      columnSettings = JsonConvert.SerializeObject(exportColumnSettings);

      var getAllEmployeeExtraPayResult = DALFactory.GetDALObject<IHolidayDAL>().GetAll(true);

      dynamic options = JsonConvert.DeserializeObject(System.Web.HttpUtility.UrlDecode(selectedOptions));

      DataSourceRequest request = new DataSourceRequest();
      request.Filters = new List<IFilterDescriptor>();
      request.Filters.Add(new FilterDescriptor("All", FilterOperator.Contains, options.exportFilters + "####" + columnSettings));
      var exportFilters = SetUpFilters<ExportHolidayModel>(request);

      var flt = System.DateTime.Now.Year;
      if (request.Filters.Count > 0)
      {
        flt = System.Convert.ToInt32(((FilterDescriptor)request.Filters[0]).Value);
      }

      var exportData = ModelMapper.Instance.Mapper.Map<IList<ExportHolidayModel>>(DALFactory.GetDALObject<IHolidayDAL>().GetAll(true).Result.Where(h => h.HolidayDate.Year == flt).OrderBy(h => h.HolidayDate).ToList());

      return ExportGrid<ExportHolidayModel>(exportData, columnSettings, selectedOptions, Resources.HM_StoreHolidays, filters: exportFilters, supportsSorting: false);
    }

    public ActionResult ManageHolidayEditingPopup_Read([DataSourceRequest] DataSourceRequest request, bool? showDeleted)
    {

      var getAllEmployeeExtraPayResult = DALFactory.GetDALObject<IHolidayDAL>().GetAll(showDeleted.GetValueOrDefault());
      if (getAllEmployeeExtraPayResult.IsSuccess)
      {
        SetUpFilters<HolidayModel>(request);

        var flt = System.DateTime.Now.Year;
        if (request.Filters.Count > 0)
        {
          flt = System.Convert.ToInt32(((FilterDescriptor)request.Filters[0]).Value);
        }

        List<HolidayModel> holidayModels = InitHolidays(flt, getAllEmployeeExtraPayResult.Result.Where(h => h.HolidayDate.Year == flt).ToList());
        return Json(holidayModels.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
      }
      else
      {
        ModelState.AddModelError("", getAllEmployeeExtraPayResult.ErrorMessage);
        return Json(new HolidayModel[] { }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
      }
    }

    //[AcceptVerbs(HttpVerbs.Post)]
    //public ActionResult ManageHolidayEditingPopup_Create([DataSourceRequest] DataSourceRequest request, EmployeeExtraPayModel employeesExtraPayModel)
    //{
    //  //PreProcessEmployeesExtraPayModelState(employeesLevelCostModel);
    //  if (employeesExtraPayModel != null && ModelState.IsValid)
    //  {
    //    var addEmployeeLevelCostResult = DALFactory.GetDALObject<IEmployeeExtraPayDAL>().AddNew(ModelMapper.Instance.Mapper.Map<EmployeeExtraPay>(employeesExtraPayModel), UserManager.GetLoggedInUser(), GetIP());
    //    if (addEmployeeLevelCostResult.IsSuccess)
    //    {
    //      employeesExtraPayModel = ModelMapper.Instance.Mapper.Map<EmployeeExtraPayModel>(addEmployeeLevelCostResult.GetSingleResult());
    //    }
    //    else
    //    {
    //      ModelState.AddModelError("", addEmployeeLevelCostResult.ErrorMessage);
    //    }
    //  }
    //  return Json(new[] { employeesExtraPayModel }.ToDataSourceResult(request, ModelState));
    //}

    //[AcceptVerbs(HttpVerbs.Post)]
    //public ActionResult ManageHolidayEditingPopup_Update([DataSourceRequest] DataSourceRequest request, EmployeeExtraPayModel employeesLevelCostModel)
    //{
    //  //PreProcessEmployeeLevelCostModelState(employeesLevelCostModel);
    //  if (employeesLevelCostModel != null && ModelState.IsValid)
    //  {
    //    var updateEmployeeLevelCostResult = DALFactory.GetDALObject<IEmployeeExtraPayDAL>().Update(ModelMapper.Instance.Mapper.Map<EmployeeExtraPay>(employeesLevelCostModel), UserManager.GetLoggedInUser(), GetIP());
    //    if (updateEmployeeLevelCostResult.IsSuccess)
    //    {
    //      employeesLevelCostModel = ModelMapper.Instance.Mapper.Map<EmployeeExtraPayModel>(updateEmployeeLevelCostResult.GetSingleResult());
    //    }
    //    else
    //    {
    //      ModelState.AddModelError("", updateEmployeeLevelCostResult.ErrorMessage);
    //    }
    //  }

    //  return Json(new[] { employeesLevelCostModel }.ToDataSourceResult(request, ModelState));
    //}

    //[AcceptVerbs(HttpVerbs.Post)]
    //public ActionResult ManageHolidayEditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, EmployeeExtraPayModel employeesLevelCostModel)
    //{
    //  if (employeesLevelCostModel != null)
    //  {
    //    employeesLevelCostModel.Deleted = true;
    //    var updateEmployeeLevelCostResult = DALFactory.GetDALObject<IEmployeeExtraPayDAL>().DeleteRecord(employeesLevelCostModel.ID, UserManager.GetLoggedInUser(), GetIP());
    //    if (!updateEmployeeLevelCostResult.IsSuccess)
    //    {
    //      ModelState.AddModelError("", updateEmployeeLevelCostResult.ErrorMessage);
    //    }
    //  }

    //  return Json(new[] { employeesLevelCostModel }.ToDataSourceResult(request, ModelState));
    //}

    private List<HolidayModel> InitHolidays(int iYear, List<Holiday> holidays)
    {
      List<HolidayModel> holidayModels = new List<HolidayModel>();
      for (int i = 1; i < 13; i++)
      {
        HolidayModel itm = new HolidayModel(iYear, i);
        holidayModels.Add(itm);

        List<Holiday> hTmp = holidays.Where(h => h.HolidayDate.Month == i).ToList();
        foreach(Holiday holiday in hTmp)
        {
          if (holiday.Notes != "")
            itm.Notes = holiday.Notes;

          switch (holiday.HolidayDate.Day)
          {
            case 1:
              itm.Day_01.ID = holiday.ID;
              itm.Day_01.dateHoliday = holiday.HolidayDate;
              itm.Day_01.IsHoliday = true;
              break;
            case 2:
              itm.Day_02.ID = holiday.ID;
              itm.Day_02.dateHoliday = holiday.HolidayDate;
              itm.Day_02.IsHoliday = true;
              break;
            case 3:
              itm.Day_03.ID = holiday.ID;
              itm.Day_03.dateHoliday = holiday.HolidayDate;
              itm.Day_03.IsHoliday = true;
              break;
            case 4:
              itm.Day_04.ID = holiday.ID;
              itm.Day_04.dateHoliday = holiday.HolidayDate;
              itm.Day_04.IsHoliday = true;
              break;
            case 5:
              itm.Day_05.ID = holiday.ID;
              itm.Day_05.dateHoliday = holiday.HolidayDate;
              itm.Day_05.IsHoliday = true;
              break;
            case 6:
              itm.Day_06.ID = holiday.ID;
              itm.Day_06.dateHoliday = holiday.HolidayDate;
              itm.Day_06.IsHoliday = true;
              break;
            case 7:
              itm.Day_07.ID = holiday.ID;
              itm.Day_07.dateHoliday = holiday.HolidayDate;
              itm.Day_07.IsHoliday = true;
              break;
            case 8:
              itm.Day_08.ID = holiday.ID;
              itm.Day_08.dateHoliday = holiday.HolidayDate;
              itm.Day_08.IsHoliday = true;
              break;
            case 9:
              itm.Day_09.ID = holiday.ID;
              itm.Day_09.dateHoliday = holiday.HolidayDate;
              itm.Day_09.IsHoliday = true;
              break;
            case 10:
              itm.Day_10.ID = holiday.ID;
              itm.Day_10.dateHoliday = holiday.HolidayDate;
              itm.Day_10.IsHoliday = true;
              break;
            case 11:
              itm.Day_11.ID = holiday.ID;
              itm.Day_11.dateHoliday = holiday.HolidayDate;
              itm.Day_11.IsHoliday = true;
              break;
            case 12:
              itm.Day_12.ID = holiday.ID;
              itm.Day_12.dateHoliday = holiday.HolidayDate;
              itm.Day_12.IsHoliday = true;
              break;
            case 13:
              itm.Day_13.ID = holiday.ID;
              itm.Day_13.dateHoliday = holiday.HolidayDate;
              itm.Day_13.IsHoliday = true;
              break;
            case 14:
              itm.Day_14.ID = holiday.ID;
              itm.Day_14.dateHoliday = holiday.HolidayDate;
              itm.Day_14.IsHoliday = true;
              break;
            case 15:
              itm.Day_15.ID = holiday.ID;
              itm.Day_15.dateHoliday = holiday.HolidayDate;
              itm.Day_15.IsHoliday = true;
              break;
            case 16:
              itm.Day_16.ID = holiday.ID;
              itm.Day_16.dateHoliday = holiday.HolidayDate;
              itm.Day_16.IsHoliday = true;
              break;
            case 17:
              itm.Day_17.ID = holiday.ID;
              itm.Day_17.dateHoliday = holiday.HolidayDate;
              itm.Day_17.IsHoliday = true;
              break;
            case 18:
              itm.Day_18.ID = holiday.ID;
              itm.Day_18.dateHoliday = holiday.HolidayDate;
              itm.Day_18.IsHoliday = true;
              break;
            case 19:
              itm.Day_19.ID = holiday.ID;
              itm.Day_19.dateHoliday = holiday.HolidayDate;
              itm.Day_19.IsHoliday = true;
              break;
            case 20:
              itm.Day_20.ID = holiday.ID;
              itm.Day_20.dateHoliday = holiday.HolidayDate;
              itm.Day_20.IsHoliday = true;
              break;
            case 21:
              itm.Day_21.ID = holiday.ID;
              itm.Day_21.dateHoliday = holiday.HolidayDate;
              itm.Day_21.IsHoliday = true;
              break;
            case 22:
              itm.Day_22.ID = holiday.ID;
              itm.Day_22.dateHoliday = holiday.HolidayDate;
              itm.Day_22.IsHoliday = true;
              break;
            case 23:
              itm.Day_23.ID = holiday.ID;
              itm.Day_23.dateHoliday = holiday.HolidayDate;
              itm.Day_23.IsHoliday = true;
              break;
            case 24:
              itm.Day_24.ID = holiday.ID;
              itm.Day_24.dateHoliday = holiday.HolidayDate;
              itm.Day_24.IsHoliday = true;
              break;
            case 25:
              itm.Day_25.ID = holiday.ID;
              itm.Day_25.dateHoliday = holiday.HolidayDate;
              itm.Day_25.IsHoliday = true;
              break;
            case 26:
              itm.Day_26.ID = holiday.ID;
              itm.Day_26.dateHoliday = holiday.HolidayDate;
              itm.Day_26.IsHoliday = true;
              break;
            case 27:
              itm.Day_27.ID = holiday.ID;
              itm.Day_27.dateHoliday = holiday.HolidayDate;
              itm.Day_27.IsHoliday = true;
              break;
            case 28:
              itm.Day_28.ID = holiday.ID;
              itm.Day_28.dateHoliday = holiday.HolidayDate;
              itm.Day_28.IsHoliday = true;
              break;
            case 29:
              itm.Day_29.ID = holiday.ID;
              itm.Day_29.dateHoliday = holiday.HolidayDate;
              itm.Day_29.IsHoliday = true;
              break;
            case 30:
              itm.Day_30.ID = holiday.ID;
              itm.Day_30.dateHoliday = holiday.HolidayDate;
              itm.Day_30.IsHoliday = true;
              break;
            case 31:
              itm.Day_31.ID = holiday.ID;
              itm.Day_31.dateHoliday = holiday.HolidayDate;
              itm.Day_31.IsHoliday = true;
              break;
          }
        }
      }

      return holidayModels;
    }

    [HttpGet]
    public ActionResult GetMonthHoliday(string yearId, string monthId, string notes)
    {
      HolidayModel holidayModel = new HolidayModel() { Year = System.Int32.Parse(yearId), Month = System.Int32.Parse(monthId), Notes = notes };

      //return PartialView("EditorTemplates/TableSchedulerEmployeeExtraPay", model);
      return PartialView("EditorTemplates/EditHoliday", holidayModel);
    }

    public ActionResult GetTableMonthHoliday(string yearId, string monthId)
    {

      TableSchedulerModel model = new TableSchedulerModel() { iYear = System.Int32.Parse(yearId), iMonth = System.Int32.Parse(monthId) };
      //model.Days = new List<int>();

      ////PopulateStoresForSalesData();
      //var holidayData = DALFactory.GetDALObject<IHolidayDAL>().GetByDate(System.Int32.Parse(yearId), System.Int32.Parse(monthId));
      //if (holidayData.IsSuccess)
      //{
      //  var epMonthHoliday = DALFactory.GetDALObject<IEmployeeExtraPayStoreRefoundDAL>().GetAll(false);

      //  if (epMonthHoliday.IsSuccess)
      //  {
      //    epMonthHoliday.Result = epMonthHoliday.Result.Where(mh => mh.Year == System.Int32.Parse(yearId) && mh.Month == System.Int32.Parse(monthId)).ToList();
      //  }

      //  model.bIsDisable = epMonthHoliday.IsSuccess && epMonthHoliday.Result.Count > 0;
      //  model.bIsUsed = model.bIsDisable;

      //  foreach (Holiday eep in holidayData.Result)
      //  {
      //    model.Days.Add(eep.HolidayDate.Day);
      //  }

      //}

      return PartialView("EditorTemplates/TableSchedulerEmployeeExtraPay", model);
    }

    
    public ActionResult MonthHoliday(FormCollection form)
    {
      int iYear = (form["Year"] == "" ? 0 : System.Int32.Parse(form["Year"]));
      int iMonth = (form["Month"] == "" ? 0 : System.Int32.Parse(form["Month"]));
      string sDay = form["DayAddNew"];
      string sNotes = form["Notes"];

      if (Request.Form["AddMonthHolidays"] != null)
      {
        var holidayDeleteData = DALFactory.GetDALObject<IHolidayDAL>().DeleteMonthRecord(iYear, iMonth);
        if (!holidayDeleteData.IsSuccess)
        {
          ModelState.AddModelError("", holidayDeleteData.ErrorMessage);
        }
        else
        {
          if (sDay != "")
          {
            foreach (string str in sDay.Split(';'))
            {
              if (str == "")
                continue;

              int iDay = System.Int32.Parse(str);
              if (iDay > 0)
              {
                System.DateTime dtHoliday = new System.DateTime(iYear, iMonth, iDay);
                if (dtHoliday.DayOfWeek != System.DayOfWeek.Sunday)
                {
                  var holidayData = DALFactory.GetDALObject<IHolidayDAL>().GetByDate(iYear, iMonth, iDay);
                  if (holidayData.IsSuccess && holidayData.Result.Count == 0)
                  {
                    Holiday holiday = new Holiday();
                    holiday.HolidayDate = new System.DateTime(iYear, iMonth, iDay);
                    if (sNotes != "")
                    {
                      holiday.Notes = sNotes;
                      sNotes = "";
                    }

                    var addHolidayResult = DALFactory.GetDALObject<IHolidayDAL>().AddNew(holiday, UserManager.GetLoggedInUser(), GetIP());
                    if (!addHolidayResult.IsSuccess)
                    {
                      ModelState.AddModelError("", addHolidayResult.ErrorMessage);
                      break;
                    }
                  }
                }
              }
            }
          }
        }
      }

      PopulateYears(2019, iLastYear: System.DateTime.Now.Year + 2);

      return View("ManageHolidays");
    }
    #endregion

  }
}