using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface ICrudBaseDAL<T>
    {
        GenericActionResult<T> AddNew(T newRecord, UserLite addedByUser, string accessIP);
        GenericActionResult<T> Update(T updateUser, UserLite updatedByUser, string accessIP);
        GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP);
        bool IsDeleteAllowed(T recordToDelete);
        GenericActionResult<T> GetById(long id);
        GenericActionResult<T> GetAll(bool isGetDeleted);
    }
}
