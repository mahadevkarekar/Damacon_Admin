using Damacon.DAL.i18n;

namespace Damacon.DAL.Database.EF
{
    public partial class ApplicationLink
    {
        public string DisplayText
        {
            get
            {
                return Resources.GetResource(LanguageResourceID);
            }
        }
    }
}
