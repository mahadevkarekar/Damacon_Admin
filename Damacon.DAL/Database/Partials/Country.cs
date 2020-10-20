using System.Threading;

namespace Damacon.DAL.Database.EF
{
    public partial class Country
    {
        public string CountryNameAndCode
        {
            get
            {
                return string.Format("{0} {1}", Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() == "it-it" ? CountryIT : CountryEN, CountryPhoneCode);
            }
        }
    }
}
