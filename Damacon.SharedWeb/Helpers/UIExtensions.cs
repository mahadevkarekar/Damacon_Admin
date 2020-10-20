using Damacon.DAL.Database.EF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Damacon.SharedWeb.Helpers
{
    public static class UIExtensions
    {
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        public const string DateTimeFormatKendo = "{0: dd/MM/yyyy HH:mm:ss}";
        public const string DateFormatKendo = "{0: dd/MM/yyyy}";
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateFormatWithDay = "dddd dd/MM/yyyy";

        public const string ExportFormat_Inline = "inline";
        public const string ExportFormat_PDF = "pdf";
        public const string ExportFormat_Excel = "xlsx";
        public const string LanguageMetaCacheKey = "LanguageMetaList";

        public static List<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return (from Enum e in Enum.GetValues(enumValue.GetType())
                    select new SelectListItem
                    {
                        Selected = false,
                        Text = e.ToString(),
                    }).ToList();
        }

        public static bool HasUserEditAccess(long userType)
        {
            UserLite user = UserManager.GetLoggedInUser();
            return user != null && user.UserType.ID == userType;
        }

        public static bool IsBackOfficerAndAboveUser()
        {
            UserLite user = UserManager.GetLoggedInUser();
            return user != null && (user.UserType.ID == 1 || user.UserType.ID == 2 || user.UserType.ID == 5);
        }

        public static string GetInfoFormatString(this IEnumerable<string> list)
        {
            if (list == null)
            {
                return string.Empty;
            }
            return string.Join(" ", list.Select(x => string.Format("<div class='col-md-3 addEditInfoItem'><i class='fa fa-circle mr-1'></i> {0}</div>", x)));
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetWeekOfYear(DateTime dateTime)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            return cul.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) + 1;
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetYearForNextWeek()
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            int currentDayWeekNumber = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday) ;
            int lastDayOfYearWeekNumber = cul.Calendar.GetWeekOfYear(new DateTime(DateTime.Now.Year, 12, 31), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (currentDayWeekNumber < lastDayOfYearWeekNumber)
            {
                return DateTime.Now.Year;
            }
            else
            {
                return DateTime.Now.Year + 1;
            }
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetNextWeekNumber(DateTime dateTime)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            int currentDayWeekNumber = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int lastDayOfYearWeekNumber = cul.Calendar.GetWeekOfYear(new DateTime(DateTime.Now.Year, 12, 31), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (currentDayWeekNumber < lastDayOfYearWeekNumber)
            {
                return currentDayWeekNumber + 1;
            }
            else
            {
                return 1;
            }
        }

        public static string GetDate(int year, int weekNumber, int addDays)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekNumber;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays((weekNum * 7) + addDays);
            return result.AddDays(-3).ToString("dd/MM/yyyy");
        }

        public static string GetBase64FromStream(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static int GetSecondsTillMidNight()
        {
            return 3570;
            //var midnight = DateTime.Today.AddDays(1);
            //return (int)(midnight - DateTime.Now).TotalSeconds;
        }

        public static string GetCountryColumn()
        {
            return Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() == "it-it" ? "CountryIT" : "CountryEN";
        }

        public static string GetEmployeesCommisionTypeColumn()
        {
            return Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() == "it-it" ? "CommisionTypeTextIT" : "CommisionTypeTextEN";
        }

        public static string GetLeaveTypeColumn()
        {
            return Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() == "it-it" ? "LeaveTypeTextIT" : "LeaveTypeTextEN";
        }
    }
}