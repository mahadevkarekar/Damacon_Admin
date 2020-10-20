using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IBackupDAL
    {
        GenericActionResult<DBBackupDetail> GetLastDBBackupDetail();
        GenericActionResult<DBBackupDetail> CreateNewBackup(long userID, string rootPath);
    }
}
