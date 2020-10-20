using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;

namespace Damacon.DAL.Operations.Concrete
{
    internal class LanguageDAL: ILanguageDAL
    {
        public GenericActionResult<LanguageResource> AddNew(LanguageResource newLanguageResource, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<LanguageResource> addNewLanguageResourceRequestDetails = new GenericActionResult<LanguageResource>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        LanguageResource languageResourceDetail = context.LanguageResources.FirstOrDefault(u => u.ResourceKey.Equals(newLanguageResource.ResourceKey, StringComparison.InvariantCultureIgnoreCase));
                        if (languageResourceDetail == null)
                        {
                            context.LanguageResources.Add(newLanguageResource);
                            context.SaveChanges();
                            transactionScope.Commit();
                            addNewLanguageResourceRequestDetails.SetSingleResult(newLanguageResource);
                            addNewLanguageResourceRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("LanguageResource : {0} {1}, Successfully Created.", LanguageResource.FirstName, LanguageResource.LastName));
                        }
                        else
                        {
                            addNewLanguageResourceRequestDetails.ErrorMessage = Resources.M_LanguageResourceAlreadyExist;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addNewLanguageResourceRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return addNewLanguageResourceRequestDetails;
        }

        public GenericActionResult<LanguageResource> Update(LanguageResource updateLanguageResource, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult<LanguageResource> updateLanguageResourceRequestDetails = new GenericActionResult<LanguageResource>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        LanguageResource languageResourceDetailFromDb = context.LanguageResources.Find(updateLanguageResource.ResourceKey);
                        if (languageResourceDetailFromDb != null)
                        {
                            context.Entry(languageResourceDetailFromDb).CurrentValues.SetValues(updateLanguageResource);
                            context.SaveChanges();
                            transactionScope.Commit();
                            updateLanguageResourceRequestDetails.SetSingleResult(languageResourceDetailFromDb);
                            updateLanguageResourceRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("LanguageResource : {0} {1}, Successfully Updated.", LanguageResourceDetail.FirstName, LanguageResourceDetail.LastName));
                        }
                        else
                        {
                            updateLanguageResourceRequestDetails.ErrorMessage = Resources.M_InternalServerError;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                updateLanguageResourceRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return updateLanguageResourceRequestDetails;
        }

        public GenericActionResult<LanguageResource> GetById(string languageResourceid)
        {
            GenericActionResult<LanguageResource> result = new GenericActionResult<LanguageResource>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.LanguageResources.Where(x => x.ResourceKey == languageResourceid).ToList();
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

        public GenericActionResult<LanguageResource> GetAll(bool isGetDeleted)
        {
            GenericActionResult<LanguageResource> result = new GenericActionResult<LanguageResource>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.LanguageResources.ToList();
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

        public GenericActionResult<LanguageResource> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP)
        {
            throw new NotImplementedException();
        }

        public bool IsDeleteAllowed(LanguageResource recordToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
