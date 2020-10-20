namespace Damacon.DAL.i18n.Abstract
{
    public interface IResourceProvider
    {
        void RefreshCache();

        object GetResource(string name, string culture);                
    }
}
