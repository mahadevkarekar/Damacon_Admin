using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using System.Data.Entity;

namespace Damacon.DAL.Operations.Concrete
{
    public class EmailServiceDAL : IEmailServiceDAL
    {
        public GenericActionResult<EmailServiceItem> AddNew(EmailServiceItem newEmailServiceItem, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<EmailServiceItem> addNewEmailServiceItemRequestDetails = new GenericActionResult<EmailServiceItem>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        EmailServiceItem emailServiceItemDetail = context.EmailServiceItems.FirstOrDefault(u => u.SendCondition == newEmailServiceItem.SendCondition && !u.Deleted);
                        if (emailServiceItemDetail == null)
                        {
                            newEmailServiceItem.LastModifyUserID = addedByUser.ID;
                            newEmailServiceItem.LastModifyDateTime = DateTime.Now;
                            newEmailServiceItem.LastModifyIP = accessIP;
                            context.EmailServiceItems.Add(newEmailServiceItem);
                            context.SaveChanges();
                            transactionScope.Commit();
                            addNewEmailServiceItemRequestDetails.SetSingleResult(newEmailServiceItem);
                            addNewEmailServiceItemRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("EmailServiceItem : {0} {1}, Successfully Created.", EmailServiceItem.FirstName, EmailServiceItem.LastName));
                        }
                        else
                        {
                            addNewEmailServiceItemRequestDetails.ErrorMessage = Resources.M_EmailServiceItemAlreadyExist;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addNewEmailServiceItemRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return addNewEmailServiceItemRequestDetails;
        }

        public GenericActionResult<EmailServiceItem> Update(EmailServiceItem updateEmailServiceItem, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult<EmailServiceItem> updateEmailServiceItemRequestDetails = new GenericActionResult<EmailServiceItem>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        EmailServiceItem emailServiceItemDetailFromDb = context.EmailServiceItems.Find(updateEmailServiceItem.ID);
                        if (emailServiceItemDetailFromDb != null)
                        {
                            updateEmailServiceItem.LastModifyUserID = updatedByUser.ID;
                            updateEmailServiceItem.LastModifyDateTime = DateTime.Now;
                            updateEmailServiceItem.LastModifyIP = accessIP;
                            context.Entry(emailServiceItemDetailFromDb).CurrentValues.SetValues(updateEmailServiceItem);
                            context.SaveChanges();
                            transactionScope.Commit();
                            updateEmailServiceItemRequestDetails.SetSingleResult(emailServiceItemDetailFromDb);
                            updateEmailServiceItemRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("EmailServiceItem : {0} {1}, Successfully Updated.", EmailServiceItemDetail.FirstName, EmailServiceItemDetail.LastName));
                        }
                        else
                        {
                            updateEmailServiceItemRequestDetails.ErrorMessage = Resources.M_InternalServerError;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                updateEmailServiceItemRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return updateEmailServiceItemRequestDetails;
        }

        public GenericActionResult<EmailServiceItem> GetById(long emailServiceItemid)
        {
            GenericActionResult<EmailServiceItem> result = new GenericActionResult<EmailServiceItem>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.EmailServiceItems.Where(x => x.ID == emailServiceItemid).ToList();
                    if (result.Result.Count() > 0)
                    {
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ErrorMessage = "";
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = "";
            }
            return result;
        }

        public GenericActionResult<EmailServiceItem> GetAll(bool isGetDeleted)
        {
            GenericActionResult<EmailServiceItem> result = new GenericActionResult<EmailServiceItem>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    if (isGetDeleted)
                    {
                        result.Result = context.EmailServiceItems.ToList();
                    }
                    else
                    {
                        result.Result = context.EmailServiceItems.Where(x => !x.Deleted).ToList();
                    }
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

        public GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult deleteRecordDataResult = new GenericActionResult();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        var emailServiceItemRecord = context.EmailServiceItems.FirstOrDefault(x => x.ID == id);
                        if (emailServiceItemRecord != null)
                        {
                            if (IsDeleteAllowed(emailServiceItemRecord))
                            {
                                emailServiceItemRecord.Deleted = true;
                                emailServiceItemRecord.LastModifyUserID = updatedByUser.ID;
                                emailServiceItemRecord.LastModifyDateTime = DateTime.Now;
                                emailServiceItemRecord.LastModifyIP = accessIP;
                                context.SaveChanges();
                                transactionScope.Commit();
                                deleteRecordDataResult.IsSuccess = true;
                            }
                            else
                            {
                                deleteRecordDataResult.ErrorMessage = Resources.M_CantDeleteRecord;
                                transactionScope.Rollback();
                            }
                        }
                        else
                        {
                            deleteRecordDataResult.ErrorMessage = Resources.M_InternalServerError;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                deleteRecordDataResult.ErrorMessage = Resources.M_InternalServerError;
            }
            return deleteRecordDataResult;
        }

        public bool IsDeleteAllowed(EmailServiceItem recordToDelete)
        {
            return true;
        }

        public GenericActionResult<EmailTaskResultItem> GetEmailTaskResultItems()
        {
            GenericActionResult<EmailTaskResultItem> result = new GenericActionResult<EmailTaskResultItem>();
            //try
            //{
            //    using (DamaconEntities context = new DamaconEntities())
            //    {
            //        result.Result = context.EmailTaskResultItems.Include(x => x.Store).ToList();
            //        result.IsSuccess = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //LoggerManager.Logger.Error(ex);
            //    result.ErrorMessage = Resources.M_InternalServerError;
            //}
            return result;
        }

        //public GenericActionResult<EmailServiceItem> GetEmailServiceItemByType(EmailSendConditionType emailSendConditionType)
        //{
        //    GenericActionResult<EmailServiceItem> result = new GenericActionResult<EmailServiceItem>();
        //    try
        //    {
        //        using (DamaconEntities context = new DamaconEntities())
        //        {
        //            result.Result = context.EmailServiceItems.Where(x => !x.Deleted && x.SendCondition == emailSendConditionType).ToList();
        //            result.IsSuccess = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //LoggerManager.Logger.Error(ex);
        //        result.ErrorMessage = Resources.M_InternalServerError;
        //    }
        //    return result;
        //}
    }
}
