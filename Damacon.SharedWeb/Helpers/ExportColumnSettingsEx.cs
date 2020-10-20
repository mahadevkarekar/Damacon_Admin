using Kendo.Mvc.Export;
using System.Collections.Generic;

namespace Damacon.SharedWeb.Helpers
{
    public class ExportColumnSettingsEx : ExportColumnSettings
    {
        public IList<NameValue> Values { get; set; }

        public float PDFColumnWidth { get; set; }

        public int ExcelColumnWidth { get; set; }
        public bool ApplyDecimalFormat  { get; set; }
    }

    public class NameValue
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsMultiColumn { get; set; }
        public string Operator { get; set; }
        public string DisplayText { get; set; }
        public bool IsIncludeInGenericSearch { get; set; }
    }

    public class SortData
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}