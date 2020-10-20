using System.Collections.Generic;

namespace Damacon.DAL.Database.EF
{
    public class DashBoardChartData
    {
        public DashBoardChartData()
        {
            DateRange = new List<string>();
            DashBoardChartSeriesList = new List<DashBoardChartSeriesData>();
        }

        public List<string> DateRange { get; set; }
        public List<DashBoardChartSeriesData> DashBoardChartSeriesList { get; set; }
    }

    public class DashBoardChartSeriesData
    {
        public long SeriesID { get; set; }
        public string SeriesName { get; set; }
        public List<DashBoardChartSeriesDataItem> Data { get; set; }
    }

    public class DashBoardChartSeriesDataItem
    {
        public string Category { get; set; }
        public decimal Value { get; set; }
    }
}