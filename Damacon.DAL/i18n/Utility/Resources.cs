namespace Damacon.DAL.i18n
{
    public partial class Resources
    {
        public static void RefreshResourceCache()
        {
            resourceProvider.RefreshCache();
        }
    }
}
