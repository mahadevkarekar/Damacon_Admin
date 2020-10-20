using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IStaticDataDAL
    {
        GenericActionResult<UserType> GetAllUserTypes();
        GenericActionResult<Country> GetAllCountries();
        GenericActionResult<DocumentationType> GetAllDocumentationTypes();
      
        GenericActionResult<LeaveType> GetAllLeaveTypes();
        GenericActionResult<LanguageMeta> GetAllLanguageMetas();
    }
}
