using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;
using System.Data.Entity;

namespace Damacon.DAL.Operations.Concrete
{
    internal class DocumentationDAL : IDocumentationDAL
    {
        public GenericActionResult<Documentation> AddNew(Documentation newDocumentation, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<Documentation> addNewDocumentationRequestDetails = new GenericActionResult<Documentation>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        Documentation documentation = context.Documentations.FirstOrDefault(u => u.Title.Equals(newDocumentation.Title, StringComparison.OrdinalIgnoreCase));
                        if (documentation == null)
                        {
                            newDocumentation.LastModifyUserID = addedByUser.ID;
                            newDocumentation.LastModifyDateTime = DateTime.Now;
                            newDocumentation.LastModifyIP = accessIP;
                            context.Documentations.Add(newDocumentation);
                            context.SaveChanges();
                            transactionScope.Commit();
                            newDocumentation.DocumentationType = context.DocumentationTypes.FirstOrDefault(x => x.ID == newDocumentation.DocumentationTypeID);
                            addNewDocumentationRequestDetails.SetSingleResult(newDocumentation);
                            addNewDocumentationRequestDetails.IsSuccess = true;
                        }
                        else
                        {
                            addNewDocumentationRequestDetails.ErrorMessage = Resources.M_DocumentationAlreadyExist;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addNewDocumentationRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return addNewDocumentationRequestDetails;
        }

        //public GenericActionResult<Documentation> Update(Documentation updateDocumentation, UserLite updatedByUser, string accessIP)
        //{
        //    GenericActionResult<Documentation> updateDocumentationRequestDetails = new GenericActionResult<Documentation>();
        //    try
        //    {
        //        using (DamaconEntities context = new DamaconEntities())
        //        {
        //            using (var transactionScope = context.Database.BeginTransaction())
        //            {
        //                Documentation documentationDetailFromDb = context.Documentations.Find(updateDocumentation.ID);
        //                if (documentationDetailFromDb != null)
        //                {
        //                    string validatonMessage = ValidateUpdate(documentationDetailFromDb, updateDocumentation, context);
        //                    if (validatonMessage == null)
        //                    {
        //                        documentationDetailFromDb.DocumentationStores.ToList().ForEach(x => context.DocumentationStores.Remove(x));
        //                        updateDocumentation.LastModifyUserID = updatedByUser.ID;
        //                        updateDocumentation.LastModifyDateTime = DateTime.Now;
        //                        updateDocumentation.LastModifyIP = accessIP;
        //                        context.Entry(documentationDetailFromDb).CurrentValues.SetValues(updateDocumentation);
        //                        context.SaveChanges();

        //                        foreach (var storeContract in updateDocumentation.DocumentationStores)
        //                        {
        //                            storeContract.DocumentationID = updateDocumentation.ID;
        //                            context.DocumentationStores.Add(storeContract);
        //                        }
        //                        context.SaveChanges();

        //                        transactionScope.Commit();
        //                        documentationDetailFromDb.DocumentationType = documentationDetailFromDb.DocumentationType;
        //                        updateDocumentationRequestDetails.SetSingleResult(documentationDetailFromDb);
        //                        updateDocumentationRequestDetails.IsSuccess = true;
        //                        //LoggerManager.Logger.Info(string.Format("Documentation : {0} {1}, Successfully Updated.", DocumentationDetail.FirstName, DocumentationDetail.LastName));
        //                    }
        //                    else
        //                    {
        //                        updateDocumentationRequestDetails.ErrorMessage = validatonMessage;
        //                        transactionScope.Rollback();
        //                    }
        //                }
        //                else
        //                {
        //                    updateDocumentationRequestDetails.ErrorMessage = Resources.M_InternalServerError;
        //                    transactionScope.Rollback();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //LoggerManager.Logger.Error(ex);
        //        updateDocumentationRequestDetails.ErrorMessage = Resources.M_InternalServerError;
        //    }
        //    return updateDocumentationRequestDetails;
        //}

        public GenericActionResult<Documentation> GetById(long documentationid)
        {
            GenericActionResult<Documentation> result = new GenericActionResult<Documentation>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.Documentations.Where(x => x.ID == documentationid).ToList();
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

        //public GenericActionResult<Documentation> GetAll(bool isGetDeleted)
        //{
        //    GenericActionResult<Documentation> result = new GenericActionResult<Documentation>();
        //    try
        //    {
        //        using (DamaconEntities context = new DamaconEntities())
        //        {
        //            if (isGetDeleted)
        //            {
        //                result.Result = context.Documentations.Include(x => x.DocumentationType).Include(x => x.DocumentationStores)
        //                    .OrderByDescending(x => x.LastModifyDateTime).ToList();
        //            }
        //            else
        //            {
        //                result.Result = context.Documentations.Include(x => x.DocumentationType).Include(x => x.DocumentationStores)
        //                    .Where(x => x.Deleted == false).OrderByDescending(x => x.LastModifyDateTime).ToList();
        //            }
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

        public GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult deleteRecordDataResult = new GenericActionResult();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        var documentationRecord = context.Documentations.FirstOrDefault(x => x.ID == id);
                        if (documentationRecord != null)
                        {
                            if (IsDeleteAllowed(documentationRecord))
                            {
                                documentationRecord.Deleted = true;
                                documentationRecord.LastModifyUserID = updatedByUser.ID;
                                documentationRecord.LastModifyDateTime = DateTime.Now;
                                documentationRecord.LastModifyIP = accessIP;
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
            return deleteRecordDataResult; ;
        }

        public bool IsDeleteAllowed(Documentation recordToDelete)
        {
            //if (recordToDelete.Employee.WorkedHours.Any(x =>
            //    recordToDelete.EmployeeStoreContracts.Any(y => y.StoreID == x.StoreID) &&
            //    recordToDelete.ContractStartDate.Value.Date <= x.WorkedDate.Date &&
            //    contractEndDate.Date >= x.WorkedDate.Date))
            //{
            //    return false;
            //}
            return true;
        }

        private string ValidateUpdate(Documentation dbValue, Documentation updatedValue, DamaconEntities context)
        {
            string validationMessage = null;

            if (context.Documentations.Any(u => u.ID != updatedValue.ID && u.Title.Equals(updatedValue.Title, StringComparison.OrdinalIgnoreCase)))
            {
                validationMessage = Resources.M_DocumentationAlreadyExist;
            }

            return validationMessage;
        }

        GenericActionResult<Documentation> ICrudBaseDAL<Documentation>.Update(Documentation updateUser, UserLite updatedByUser, string accessIP)
        {
            throw new NotImplementedException();
        }

        GenericActionResult<Documentation> ICrudBaseDAL<Documentation>.GetAll(bool isGetDeleted)
        {
            throw new NotImplementedException();
        }
    }
}