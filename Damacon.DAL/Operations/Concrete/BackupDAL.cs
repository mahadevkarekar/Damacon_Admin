using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using System.Data.Entity;

namespace Damacon.DAL.Operations.Concrete
{
    internal class BackupDAL : IBackupDAL
    {
        public GenericActionResult<DBBackupDetail> GetLastDBBackupDetail()
        {
            GenericActionResult<DBBackupDetail> result = new GenericActionResult<DBBackupDetail>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    var lastBackup = context.DBBackupDetails.Include(x => x.User).OrderByDescending(p => p.ID).FirstOrDefault();
                    if (lastBackup != null)
                    {
                        result.SetSingleResult(lastBackup);
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Record not found";
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError;
            }
            return result;
        }

        public GenericActionResult<DBBackupDetail> CreateNewBackup(long userID, string rootPath)
        {
            GenericActionResult<DBBackupDetail> result = new GenericActionResult<DBBackupDetail>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    DateTime backUpTime = DateTime.Now;
                    string fileName = backUpTime.ToString("yyyy_MM_dd_HH.mm") + "_StoreD_Backup.bak";
                    string dbname = context.Database.Connection.Database;
                    string dbBackUp = System.IO.Path.Combine(rootPath, fileName);
                    string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'StoreD Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                    int path = context.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, dbname, dbBackUp));
                    DBBackupDetail dbBackupDetail = new DBBackupDetail();
                    dbBackupDetail.BackupTime = backUpTime;
                    dbBackupDetail.FileName = fileName;
                    dbBackupDetail.UserID = userID;
                    context.DBBackupDetails.Add(dbBackupDetail);
                    context.SaveChanges();
                    dbBackupDetail.User = context.Users.FirstOrDefault(x => x.ID == userID);
                    result.SetSingleResult(dbBackupDetail);
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError;
            }
            return result;
        }
    }
}
