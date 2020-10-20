using System.Collections.Generic;

namespace Damacon.SharedWeb.Helpers
{
    public class FilterData
    {
        public string GenericSearchText { get; set; }
        public List<NameValue> ColumnFilters { get; set; }
    }
}