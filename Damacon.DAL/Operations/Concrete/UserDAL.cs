using System;
using System.Linq;
using Damacon.DAL.Operations.Contracts;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using System.Data.Entity;

namespace Damacon.DAL.Operations.Concrete
{
    internal class UserDAL : IUserDAL
    {
        public GenericActionResult<User> AddNew(User newUser, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<User> addNewUserRequestDetails = new GenericActionResult<User>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {

                        User userDetail = context.Users.FirstOrDefault(u => u.Username.Equals(newUser.Username, StringComparison.OrdinalIgnoreCase));
                        if (userDetail == null)
                        {
                            newUser.LastModifyUserID = addedByUser.ID;
                            newUser.LastModifyDateTime = DateTime.Now;
                            newUser.LastModifyIP = accessIP;
                            context.Users.Add(newUser);
                            context.SaveChanges();
                            transactionScope.Commit();
                            newUser.UserType = context.UserTypes.FirstOrDefault(x => x.ID == newUser.UserTypeID);
                           // newUser.Store = context.Stores.FirstOrDefault(x => x.ID == newUser.StoreID);
                            addNewUserRequestDetails.SetSingleResult(newUser);
                            addNewUserRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("User : {0} {1}, Successfully Created.", user.FirstName, user.LastName));
                        }
                        else
                        {
                            addNewUserRequestDetails.ErrorMessage = Resources.M_UserAlreadyExist;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addNewUserRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return addNewUserRequestDetails;
        }

        public GenericActionResult<User> Update(User updateUser, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult<User> updateUserRequestDetails = new GenericActionResult<User>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        User userDetailFromDb = context.Users.Find(updateUser.ID);
                        if (userDetailFromDb != null)
                        {
                            updateUser.LastModifyUserID = updatedByUser.ID;
                            updateUser.LastModifyDateTime = DateTime.Now;
                            updateUser.LastModifyIP = accessIP;
                            if (string.IsNullOrEmpty(updateUser.Password))
                            {
                                updateUser.Password = userDetailFromDb.Password;
                            }
                            context.Entry(userDetailFromDb).CurrentValues.SetValues(updateUser);
                            context.SaveChanges();
                            transactionScope.Commit();
                            userDetailFromDb.UserType = userDetailFromDb.UserType;
                           // userDetailFromDb.Store = userDetailFromDb.Store;
                            updateUserRequestDetails.SetSingleResult(userDetailFromDb);
                            updateUserRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("User : {0} {1}, Successfully Updated.", userDetail.FirstName, userDetail.LastName));
                        }
                        else
                        {
                            updateUserRequestDetails.ErrorMessage = Resources.M_InvalidUser;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                updateUserRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return updateUserRequestDetails;
        }

        public GenericActionResult<User> GetById(long userid)
        {
            GenericActionResult<User> result = new GenericActionResult<User>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.Users.Where(x => x.ID == userid).ToList();
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

        public GenericActionResult<User> GetAll(bool isGetDeleted)
        {
            GenericActionResult<User> result = new GenericActionResult<User>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    if (isGetDeleted)
                    {
                        result.Result = context.Users.Include(x => x.UserType).ToList();
                    }
                    else
                    {
                        result.Result = context.Users.Include(x => x.UserType).Where(x => x.Deleted == false).ToList();
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

        public GenericActionResult<User> GetAllUsersWithPassword()
        {
            GenericActionResult<User> result = new GenericActionResult<User>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.Users.Include(x => x.UserType).ToList();
                    result.Result.ForEach(x => x.Password = null);
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

        public GenericActionResult<UserLite> VerifyUserCredentials(string username, string password, string accessIP)
        {
            GenericActionResult<UserLite> userLoginResult = new GenericActionResult<UserLite>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    User user = context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password));
                    
                    if (user != null)
                    {
                        if (!user.Disable && !user.Deleted)
                        {
                            UserLite userLite = (new UserLite()
                            {
                                ID = user.ID,
                                FirstName = user.FirstName,
                                Surname = user.Surname,
                                Username = user.Username,
                                UserType = user.UserType,
                               
                                Disable = user.Disable,
                                DefaultLoginPage = user.ApplicationLink
                            });
                            userLite.UserType.ApplicationLinkUserTypePermissions = userLite.UserType.ApplicationLinkUserTypePermissions.Where(x => x.Disable == false).ToList();
                            userLite.UserType.ApplicationLinkUserTypePermissions.ToList().ForEach(x => x.ApplicationLink = x.ApplicationLink);
                            userLoginResult.SetSingleResult(userLite);
                            user.LastAccessIP = accessIP;
                            user.LastAccessTime = DateTime.Now;
                            context.SaveChanges();
                            userLoginResult.IsSuccess = true;
                        }
                        else
                        {
                            userLoginResult.ErrorMessage = Resources.M_InactiveUser; // UserLoginResultStatus.InactiveUser;
                        }
                    }
                    else
                    {
                        userLoginResult.ErrorMessage = Resources.M_IncorrectUserNameorPasssword; // UserLoginResultStatus.InvalidUsernamePassword;
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                userLoginResult.ErrorMessage = Resources.M_InternalServerError; //UserLoginResultStatus.InternalServerError;
            }
            return userLoginResult;
        }

        public GenericActionResult<ApplicationLink> GetAllUserTypeLoginPages(int userTypeId)
        {
            GenericActionResult<ApplicationLink> result = new GenericActionResult<ApplicationLink>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    var permissionPages = context.ApplicationLinkUserTypePermissions.Where(x => x.UserTypeId == userTypeId && x.Disable == false).ToList();
                    if (permissionPages != null)
                    {
                        result.Result = permissionPages.Where(x => x.ApplicationLink != null && x.Disable == false).Select(x => x.ApplicationLink).ToList();
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
                        var userRecord = context.Users.FirstOrDefault(x => x.ID == id);
                        if (userRecord != null)
                        {
                            if (IsDeleteAllowed(userRecord))
                            {
                                userRecord.Deleted = true;
                                userRecord.LastModifyUserID = updatedByUser.ID;
                                userRecord.LastModifyDateTime = DateTime.Now;
                                userRecord.LastModifyIP = accessIP;
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

        public bool IsDeleteAllowed(User recordToDelete)
        {
            
            return true;
        }
    }
}
