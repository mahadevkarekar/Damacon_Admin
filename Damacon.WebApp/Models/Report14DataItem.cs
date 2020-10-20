using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class Report14DataItem
    {
        [Display(Name = "WorkedDate", ResourceType = typeof(DAL.i18n.Resources))]
        public DateTime? WorkedDate { get; set; }

        [Display(Name = "Employee", ResourceType = typeof(DAL.i18n.Resources))]
        public long EmployeeID { get; set; }

        [Display(Name = "Employee", ResourceType = typeof(DAL.i18n.Resources))]
        public string EmployeeName { get; set; }

        [Display(Name = "Sold", ResourceType = typeof(DAL.i18n.Resources))]
        public decimal Sold { get; set; }
        
        [Display(Name = "TotalHours", ResourceType = typeof(DAL.i18n.Resources))]
        public decimal WorkedHour { get; set; }

        [Display(Name = "ComparisonHours", ResourceType = typeof(DAL.i18n.Resources))]
        public decimal IncomeToHour
        {
            get
            {
                if (WorkedHour == 0)
                {
                    return 0;
                }
                return Sold / WorkedHour;
            }
        }

        public IList<Report14DataItem> Report14DataItems { get; set; }
    }
}
