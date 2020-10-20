using System;
using System.Collections.Generic;

namespace Damacon.WebApp.Models
{
    public class Report14Model : ReportModelBase
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CompanyID { get; set; }
        public int StoreID { get; set; }

        public IList<Report14DataItem> ReportItems { get; set; }

        public string ChartData { get; set; }
}
}