namespace Damacon.WebApp.Models
{
    public class ReportModelBase
    {
        public string ReportFormat { get; set; }
        public string ReportPDFData { get; set; }
        public string Error { get; set; }
        public string ReportLayout { get; set; }
    }
}