using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
  public class HolidayModel : BaseModel<long>
  {

    public HolidayModel()
    {
      //Default
    }

    public HolidayModel(int iYear, int iMonth)
    {
      Year = iYear;
      Month = iMonth;

      Day_01 = new HolidayDetailModel(iYear, iMonth, 1);
      Day_02 = new HolidayDetailModel(iYear, iMonth, 2);
      Day_03 = new HolidayDetailModel(iYear, iMonth, 3);
      Day_04 = new HolidayDetailModel(iYear, iMonth, 4);
      Day_05 = new HolidayDetailModel(iYear, iMonth, 5);
      Day_06 = new HolidayDetailModel(iYear, iMonth, 6);
      Day_07 = new HolidayDetailModel(iYear, iMonth, 7);
      Day_08 = new HolidayDetailModel(iYear, iMonth, 8);
      Day_09 = new HolidayDetailModel(iYear, iMonth, 9);
      Day_10 = new HolidayDetailModel(iYear, iMonth, 10);

      Day_11 = new HolidayDetailModel(iYear, iMonth, 11);
      Day_12 = new HolidayDetailModel(iYear, iMonth, 12);
      Day_13 = new HolidayDetailModel(iYear, iMonth, 13);
      Day_14 = new HolidayDetailModel(iYear, iMonth, 14);
      Day_15 = new HolidayDetailModel(iYear, iMonth, 15);
      Day_16 = new HolidayDetailModel(iYear, iMonth, 16);
      Day_17 = new HolidayDetailModel(iYear, iMonth, 17);
      Day_18 = new HolidayDetailModel(iYear, iMonth, 18);
      Day_19 = new HolidayDetailModel(iYear, iMonth, 19);
      Day_20 = new HolidayDetailModel(iYear, iMonth, 20);

      Day_21 = new HolidayDetailModel(iYear, iMonth, 21);
      Day_22 = new HolidayDetailModel(iYear, iMonth, 22);
      Day_23 = new HolidayDetailModel(iYear, iMonth, 23);
      Day_24 = new HolidayDetailModel(iYear, iMonth, 24);
      Day_25 = new HolidayDetailModel(iYear, iMonth, 25);
      Day_26 = new HolidayDetailModel(iYear, iMonth, 26);
      Day_27 = new HolidayDetailModel(iYear, iMonth, 27);
      Day_28 = new HolidayDetailModel(iYear, iMonth, 28);
      Day_29 = new HolidayDetailModel(iYear, iMonth, 29);
      Day_30 = new HolidayDetailModel(iYear, iMonth, 30);
      Day_31 = new HolidayDetailModel(iYear, iMonth, 31);
    }

    [Display(Name = "Year", ResourceType = typeof(DAL.i18n.Resources))]
    public int Year { get; set; }

    [Display(Name = "Month", ResourceType = typeof(DAL.i18n.Resources))]
    public int Month { get; set; }

    [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
    public string Notes { get; set; }

    public HolidayDetailModel Day_01 { get; set; }
    public HolidayDetailModel Day_02 { get; set; }
    public HolidayDetailModel Day_03 { get; set; }
    public HolidayDetailModel Day_04 { get; set; }
    public HolidayDetailModel Day_05 { get; set; }
    public HolidayDetailModel Day_06 { get; set; }
    public HolidayDetailModel Day_07 { get; set; }
    public HolidayDetailModel Day_08 { get; set; }
    public HolidayDetailModel Day_09 { get; set; }
    public HolidayDetailModel Day_10 { get; set; }
    public HolidayDetailModel Day_11 { get; set; }
    public HolidayDetailModel Day_12 { get; set; }
    public HolidayDetailModel Day_13 { get; set; }
    public HolidayDetailModel Day_14 { get; set; }
    public HolidayDetailModel Day_15 { get; set; }
    public HolidayDetailModel Day_16 { get; set; }
    public HolidayDetailModel Day_17 { get; set; }
    public HolidayDetailModel Day_18 { get; set; }
    public HolidayDetailModel Day_19 { get; set; }
    public HolidayDetailModel Day_20 { get; set; }
    public HolidayDetailModel Day_21 { get; set; }
    public HolidayDetailModel Day_22 { get; set; }
    public HolidayDetailModel Day_23 { get; set; }
    public HolidayDetailModel Day_24 { get; set; }
    public HolidayDetailModel Day_25 { get; set; }
    public HolidayDetailModel Day_26 { get; set; }
    public HolidayDetailModel Day_27 { get; set; }
    public HolidayDetailModel Day_28 { get; set; }
    public HolidayDetailModel Day_29 { get; set; }
    public HolidayDetailModel Day_30 { get; set; }
    public HolidayDetailModel Day_31 { get; set; }

    [Display(Name = "Month", ResourceType = typeof(DAL.i18n.Resources))]
    public string MonthDescription
    {
      get
      {
        if (Year > 0 && Month > 0)
          return new System.DateTime(Year, Month, 1).ToString("MMMM");
        else
          return "";
      }
    }

  }

  public class HolidayDetailModel : BaseModel<long>
  {
    public HolidayDetailModel()
    {
      //Default
    }

    public HolidayDetailModel(int iYear, int iMonth, int iDay)
    {
      if (iDay > System.DateTime.DaysInMonth(iYear, iMonth))
        dateHoliday = System.DateTime.MinValue;
      else
        dateHoliday = new System.DateTime(iYear, iMonth, iDay);
    }

    public System.DateTime dateHoliday { get; set; }

    public bool IsHoliday { get; set; }

    public string StringDay
    {
      get
      {
        if (dateHoliday != System.DateTime.MinValue)
          return dateHoliday.ToString("ddd");
        else
          return "";
      }
    }

    public string StyleClassItem
    {
      get
      {
        if (IsHoliday || IsSunday)
          return "HolidayClass";
        else
          return "NoHolidayClass";
      }
    }

    public bool IsSunday
    {
      get
      {
        if (dateHoliday != System.DateTime.MinValue)
          return dateHoliday.DayOfWeek == System.DayOfWeek.Sunday;
        else
          return false;
      }
    }
  }
}