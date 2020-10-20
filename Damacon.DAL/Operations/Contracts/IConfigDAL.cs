using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IConfigDAL
    {
        GenericActionResult<WebConfiguration> GetByKey(string key);
        GenericActionResult<WebConfiguration> AddOrUpdate(string key, string value, UserLite addedByUser, string accessIP);
    }
}
