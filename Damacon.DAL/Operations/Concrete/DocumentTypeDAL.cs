//using Damacon.DAL.Database.EF;
//using Damacon.DAL.i18n;
//using Damacon.DAL.Operations.Contracts;
//using System;
//using System.Linq;

//namespace Damacon.DAL.Operations.Concrete
//{
//    internal class DocumentTypeDAL : IDocumentTypeDAL
//    {
//        public GenericActionResult<DocumentType> AddNew(DocumentType newDocumentType, UserLite addedByUser, string accessIP)
//        {
//            GenericActionResult<DocumentType> addNewDocumentTypeRequestDetails = new GenericActionResult<DocumentType>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    using (var transactionScope = context.Database.BeginTransaction())
//                    {
//                        DocumentType documentTypeDetail = context.DocumentTypes.FirstOrDefault(u => u.TypeText.Equals(newDocumentType.TypeText, StringComparison.InvariantCultureIgnoreCase));
//                        if (documentTypeDetail == null)
//                        {
//                            newDocumentType.LastModifyUserID = addedByUser.ID;
//                            newDocumentType.LastModifyDateTime = DateTime.Now;
//                            newDocumentType.LastModifyIP = accessIP;
//                            context.DocumentTypes.Add(newDocumentType);
//                            context.SaveChanges();
//                            transactionScope.Commit();
//                            addNewDocumentTypeRequestDetails.SetSingleResult(newDocumentType);
//                            addNewDocumentTypeRequestDetails.IsSuccess = true;
//                            //LoggerManager.Logger.Info(string.Format("DocumentType : {0} {1}, Successfully Created.", DocumentType.FirstName, DocumentType.LastName));
//                        }
//                        else
//                        {
//                            addNewDocumentTypeRequestDetails.ErrorMessage = Resources.M_DocumentTypeAlreadyExist;
//                            transactionScope.Rollback();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                addNewDocumentTypeRequestDetails.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return addNewDocumentTypeRequestDetails;
//        }

//        public GenericActionResult<DocumentType> Update(DocumentType updateDocumentType, UserLite updatedByUser, string accessIP)
//        {
//            GenericActionResult<DocumentType> updateDocumentTypeRequestDetails = new GenericActionResult<DocumentType>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    using (var transactionScope = context.Database.BeginTransaction())
//                    {
//                        DocumentType documentTypeDetailFromDb = context.DocumentTypes.Find(updateDocumentType.ID);
//                        if (documentTypeDetailFromDb != null)
//                        {
//                            updateDocumentType.LastModifyUserID = updatedByUser.ID;
//                            updateDocumentType.LastModifyDateTime = DateTime.Now;
//                            updateDocumentType.LastModifyIP = accessIP;
//                            context.Entry(documentTypeDetailFromDb).CurrentValues.SetValues(updateDocumentType);
//                            context.SaveChanges();
//                            transactionScope.Commit();
//                            updateDocumentTypeRequestDetails.SetSingleResult(documentTypeDetailFromDb);
//                            updateDocumentTypeRequestDetails.IsSuccess = true;
//                            //LoggerManager.Logger.Info(string.Format("DocumentType : {0} {1}, Successfully Updated.", DocumentTypeDetail.FirstName, DocumentTypeDetail.LastName));
//                        }
//                        else
//                        {
//                            updateDocumentTypeRequestDetails.ErrorMessage = Resources.M_InternalServerError;
//                            transactionScope.Rollback();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                updateDocumentTypeRequestDetails.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return updateDocumentTypeRequestDetails;
//        }

//        public GenericActionResult<DocumentType> GetById(long documentTypeid)
//        {
//            GenericActionResult<DocumentType> result = new GenericActionResult<DocumentType>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    result.Result = context.DocumentTypes.Where(x => x.ID == documentTypeid).ToList();
//                    if (result.Result.Count() > 0)
//                    {
//                        result.IsSuccess = true;
//                    }
//                    else
//                    {
//                        result.ErrorMessage = "";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = "";
//            }
//            return result;
//        }

//        public GenericActionResult<DocumentType> GetAll(bool isGetDeleted)
//        {
//            GenericActionResult<DocumentType> result = new GenericActionResult<DocumentType>();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    if (isGetDeleted)
//                    {
//                        result.Result = context.DocumentTypes.ToList();
//                    }
//                    else
//                    {
//                        result.Result = context.DocumentTypes.Where(x => x.Deleted == false).ToList();
//                    }
//                    result.IsSuccess = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                result.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return result;
//        }

//        public GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP)
//        {
//            GenericActionResult deleteRecordDataResult = new GenericActionResult();
//            try
//            {
//                using (DamaconEntities context = new DamaconEntities())
//                {
//                    using (var transactionScope = context.Database.BeginTransaction())
//                    {
//                        var documentTypeRecord = context.DocumentTypes.FirstOrDefault(x => x.ID == id);
//                        if (documentTypeRecord != null)
//                        {
//                            if (IsDeleteAllowed(documentTypeRecord))
//                            {
//                                documentTypeRecord.Deleted = true;
//                                documentTypeRecord.LastModifyUserID = updatedByUser.ID;
//                                documentTypeRecord.LastModifyDateTime = DateTime.Now;
//                                documentTypeRecord.LastModifyIP = accessIP;
//                                context.SaveChanges();
//                                transactionScope.Commit();
//                                deleteRecordDataResult.IsSuccess = true;
//                            }
//                            else
//                            {
//                                deleteRecordDataResult.ErrorMessage = Resources.M_CantDeleteRecord;
//                                transactionScope.Rollback();
//                            }
//                        }
//                        else
//                        {
//                            deleteRecordDataResult.ErrorMessage = Resources.M_InternalServerError;
//                            transactionScope.Rollback();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //LoggerManager.Logger.Error(ex);
//                deleteRecordDataResult.ErrorMessage = Resources.M_InternalServerError;
//            }
//            return deleteRecordDataResult;
//        }

//        public bool IsDeleteAllowed(DocumentType recordToDelete)
//        {
//            return true;
//        }
//    }
//}
