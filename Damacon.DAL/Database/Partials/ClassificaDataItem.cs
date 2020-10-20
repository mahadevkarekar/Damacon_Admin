using System.Collections.Generic;

namespace Damacon.DAL.Database.EF
{
    public class ClassificaDataItem
    {
        public long StoreID { get; set; }
        public string StoreName { get; set; }
        public List<ClassificaWeekDataItem> WeekDataItems { get; set; }

        public int TotalPoints
        {
            get
            {
                int totalPoints = 0;
                if (WeekDataItems != null)
                {
                    WeekDataItems.ForEach(x => totalPoints += x.Points);
                }
                return totalPoints;
            }
        }
    }

    public class ClassificaWeekDataItem
    {
        public int WeekNumber { get; set; }
        public decimal ProfitPercent { get; set; }
        public int Points { get; set; }
    }
}
