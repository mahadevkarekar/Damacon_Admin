using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IUserDAL : ICrudBaseDAL<User>
    {
        GenericActionResult<UserLite> VerifyUserCredentials(string username, string password, string accessIP);
        GenericActionResult<ApplicationLink> GetAllUserTypeLoginPages(int userTypeId);

        GenericActionResult<User> GetAllUsersWithPassword();
    }
}
