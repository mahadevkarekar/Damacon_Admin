using Damacon.DAL.Database.EF;
using Kendo.Mvc.UI;
using System;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IClientDAL : ICrudBaseDAL<Client>
    {
        GenericActionResult<DataSourceResult> GetAll(DataSourceRequest request, DateTime? birthdayFromDate, DateTime? birthdayToDate, bool isGetDeleted);

        GenericActionResult<ClientLite> VerifyClientCredentials(string username, string password, string accessIP);

        GenericActionResult ChangeClientPassword(long clientID, string currentPassword, string newPassword);
        GenericActionResult<Client> RecoverClientPassword(string recoveryData);

        GenericActionResult ChangeClientEmail(long clientID, string newEmail);
    }
}
