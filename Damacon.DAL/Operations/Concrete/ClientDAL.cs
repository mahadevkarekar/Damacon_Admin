using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;
using System.Data.Entity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity.SqlServer;

namespace Damacon.DAL.Operations.Concrete
{
    internal class ClientDAL : IClientDAL
    {
        public GenericActionResult<Client> AddNew(Client newClient, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<Client> addNewClientRequestDetails = new GenericActionResult<Client>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        if (context.Clients.Any(x => !x.Deleted && (x.MobilePhone == newClient.MobilePhone)))
                        {
                            addNewClientRequestDetails.ErrorMessage = Resources.M_ClientWithPhoneExists;
                        }
                        else if (context.Clients.Any(x => !x.Deleted && (!string.IsNullOrEmpty(newClient.Email) && x.Email == newClient.Email)))
                        {
                            addNewClientRequestDetails.ErrorMessage = Resources.M_ClientWithEmailExists;
                        }
                        else if (context.Clients.Any(x => !x.Deleted && (newClient.CardCode.HasValue && x.CardCode == newClient.CardCode)))
                        {
                            addNewClientRequestDetails.ErrorMessage = Resources.M_CardCodeAlreadyExists;
                        }
                        else
                        {
                            newClient.RegisteredByUserID = addedByUser.ID;
                            newClient.RegisteredOnDateTime = DateTime.Now;
                            newClient.LastModifyUserID = addedByUser.ID;
                            newClient.LastModifyDateTime = DateTime.Now;
                            newClient.LastModifyIP = accessIP;
                            context.Clients.Add(newClient);
                            context.SaveChanges();
                            transactionScope.Commit();

                          //  newClient.Store = context.Stores.FirstOrDefault(x => x.ID == newClient.RegisteredByStoreID);
                            addNewClientRequestDetails.SetSingleResult(newClient);
                            addNewClientRequestDetails.IsSuccess = true;
                            //LoggerManager.Logger.Info(string.Format("Client : {0} {1}, Successfully Created.", Client.FirstName, Client.LastName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addNewClientRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return addNewClientRequestDetails;
        }

        public GenericActionResult<Client> Update(Client updateClient, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult<Client> updateClientRequestDetails = new GenericActionResult<Client>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        Client clientDetailFromDb = context.Clients.Find(updateClient.ID);
                        if (clientDetailFromDb != null)
                        {
                            if (context.Clients.Any(x => !x.Deleted && x.ID != updateClient.ID &&
                                x.MobilePhone == updateClient.MobilePhone))
                            {
                                updateClientRequestDetails.ErrorMessage = Resources.M_ClientWithPhoneExists;
                            }
                            else if (context.Clients.Any(x => !x.Deleted && x.ID != updateClient.ID &&
                                (!string.IsNullOrEmpty(updateClient.Email) && x.Email == updateClient.Email)))
                            {
                                updateClientRequestDetails.ErrorMessage = Resources.M_ClientWithEmailExists;
                            }
                            else if (context.Clients.Any(x => !x.Deleted && x.ID != updateClient.ID &&
                                (updateClient.CardCode.HasValue && x.CardCode == updateClient.CardCode)))
                            {
                                updateClientRequestDetails.ErrorMessage = Resources.M_CardCodeAlreadyExists;
                            }
                            else
                            {
                                updateClient.RegisteredByUserID = clientDetailFromDb.RegisteredByUserID;
                                updateClient.LastModifyUserID = updatedByUser.ID;
                                updateClient.LastModifyDateTime = DateTime.Now;
                                updateClient.LastModifyIP = accessIP;
                                context.Entry(clientDetailFromDb).CurrentValues.SetValues(updateClient);
                                context.SaveChanges();
                                transactionScope.Commit();

                              //  clientDetailFromDb.Store = context.Stores.FirstOrDefault(x => x.ID == clientDetailFromDb.RegisteredByStoreID);
                                updateClientRequestDetails.SetSingleResult(clientDetailFromDb);
                                updateClientRequestDetails.IsSuccess = true;
                            }
                            //LoggerManager.Logger.Info(string.Format("Client : {0} {1}, Successfully Updated.", ClientDetail.FirstName, ClientDetail.LastName));
                        }
                        else
                        {
                            updateClientRequestDetails.ErrorMessage = Resources.RecordDoesntExist;
                            transactionScope.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                updateClientRequestDetails.ErrorMessage = Resources.M_InternalServerError;
            }
            return updateClientRequestDetails;
        }

        public GenericActionResult<Client> GetById(long clientid)
        {
            GenericActionResult<Client> result = new GenericActionResult<Client>();
            //try
            //{
            //    using (DamaconEntities context = new DamaconEntities())
            //    {
            //        result.Result = context.Clients.Include(x => x.Store).Where(x => x.ID == clientid).ToList();
            //        if (result.Result.Count() > 0)
            //        {
            //            result.IsSuccess = true;
            //        }
            //        else
            //        {
            //            result.ErrorMessage = "";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //LoggerManager.Logger.Error(ex);
            //    result.ErrorMessage = "";
            //}
            return result;
        }

        public GenericActionResult<Client> GetAll(bool isGetDeleted)
        {
            GenericActionResult<Client> result = new GenericActionResult<Client>();
            //try
            //{
            //    using (DamaconEntities context = new DamaconEntities())
            //    {
            //        if (isGetDeleted)
            //        {
            //            result.Result = context.Clients.Include(x => x.Store).Include(x => x.ClientShoppings).ToList();
            //        }
            //        else
            //        {
            //            result.Result = context.Clients.Include(x => x.Store).Include(x => x.ClientShoppings).Where(x => x.Deleted == false).ToList();
            //        }
            //        result.Result.ForEach(x => x.ShoppingStores = x.ClientShoppings.Where(cs => !cs.Deleted).Select(s => s.Store.StoreName).Distinct().ToList());
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

        public GenericActionResult DeleteRecord(long id, UserLite updatedByUser, string accessIP)
        {
            GenericActionResult deleteRecordDataResult = new GenericActionResult();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {
                        var clientRecord = context.Clients.FirstOrDefault(x => x.ID == id);
                        if (clientRecord != null)
                        {
                            if (IsDeleteAllowed(clientRecord))
                            {
                                clientRecord.Deleted = true;
                                clientRecord.LastModifyUserID = updatedByUser.ID;
                                clientRecord.LastModifyDateTime = DateTime.Now;
                                clientRecord.LastModifyIP = accessIP;
                                context.SaveChanges();
                                transactionScope.Commit();
                                deleteRecordDataResult.IsSuccess = true;
                            }
                            else
                            {
                                deleteRecordDataResult.ErrorMessage = Resources.M_UnableToDeleteClient;
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

        public bool IsDeleteAllowed(Client recordToDelete)
        {
            //if (recordToDelete.ClientShoppings.Any(x => !x.Deleted))
            //{
            //    return false;
            //}
            return true;
        }

        ////public GenericActionResult<Client> GetByStoreId(long storeID)
        ////{
        ////    GenericActionResult<Client> result = new GenericActionResult<Client>();
        ////    try
        ////    {
        ////        using (DamaconEntities context = new DamaconEntities())
        ////        {
        ////            result.Result = context.Clients.Where(x => !x.Deleted && !x.Disable &&
        ////            x.ClientCompanyContracts.Any(y => !y.Deleted &&
        ////                (y.ContractEndDate == null || DbFunctions.TruncateTime(y.ContractEndDate) >= DbFunctions.TruncateTime(DateTime.Now))
        ////                && y.ClientStoreContracts.Any(s => s.StoreID == storeID))).ToList();
        ////            result.IsSuccess = true;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //LoggerManager.Logger.Error(ex);
        ////        result.ErrorMessage = Resources.M_InternalServerError;
        ////    }
        ////    return result;
        ////}

        public GenericActionResult<DataSourceResult> GetAll(DataSourceRequest request, DateTime? birthdayFromDate, DateTime? birthdayToDate, bool isGetDeleted)
        {
            GenericActionResult<DataSourceResult> result = new GenericActionResult<DataSourceResult>();
            try
            {
                if (request.Sorts != null && request.Sorts.Count > 0)
                {
                    if(request.Sorts.First().Member == "UserName")
                    {
                        request.Sorts.First().Member = "MobilePhone";
                    }
                    else if (request.Sorts.First().Member == "RegisteredByStoreName")
                    {
                        request.Sorts.First().Member = "RegisteredByStoreID";
                    }
                }
                using (DamaconEntities context = new DamaconEntities())
                {
                    DataSourceResult dataSourceResult = context.Clients
                        .Where(x => (isGetDeleted || !x.Deleted) &&
                            (birthdayFromDate == null || x.DateBirth == null || (x.DateBirth.Value.Month < birthdayFromDate.Value.Month) ||
                             (x.DateBirth.Value.Month == birthdayFromDate.Value.Month && x.DateBirth.Value.Day <= birthdayFromDate.Value.Day)) &&
                            (birthdayToDate == null || x.DateBirth == null || (x.DateBirth.Value.Month > birthdayToDate.Value.Month) ||
                             (x.DateBirth.Value.Month == birthdayToDate.Value.Month) && x.DateBirth.Value.Day >= birthdayToDate.Value.Day))
                        .ToDataSourceResult(request);
                    result.SetSingleResult(dataSourceResult);
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

        public GenericActionResult<ClientLite> VerifyClientCredentials(string username, string password, string accessIP)
        {
            GenericActionResult<ClientLite> clientLoginResult = new GenericActionResult<ClientLite>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    username = username.TrimStart('0');
                    Client client = context.Clients.FirstOrDefault(u => u.Username.Substring(SqlFunctions.PatIndex("%[^0]%", u.Username).Value - 1) == username && u.ClientPassword.Equals(password));

                    if (client != null)
                    {
                        if (!client.Disable && !client.Deleted)
                        {
                            ClientLite clientLite = (new ClientLite()
                            {
                                ID = client.ID,
                                ClientName = client.FullName,
                            });
                            clientLoginResult.SetSingleResult(clientLite);
                            context.SaveChanges();
                            clientLoginResult.IsSuccess = true;
                        }
                        else
                        {
                            clientLoginResult.ErrorMessage = Resources.M_InactiveUser; // UserLoginResultStatus.InactiveUser;
                        }
                    }
                    else
                    {
                        clientLoginResult.ErrorMessage = Resources.M_IncorrectUserNameorPasssword; // UserLoginResultStatus.InvalidUsernamePassword;
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                clientLoginResult.ErrorMessage = Resources.M_InternalServerError; //UserLoginResultStatus.InternalServerError;
            }
            return clientLoginResult;
        }

        public GenericActionResult ChangeClientPassword(long clientID, string currentPassword, string newPassword)
        {
            GenericActionResult clientLoginResult = new GenericActionResult();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    Client client = context.Clients.FirstOrDefault(u => u.ID == clientID);

                    if (client != null)
                    {
                        if (client.ClientPassword == currentPassword)
                        {
                            client.ClientPassword = newPassword;
                            context.SaveChanges();
                            clientLoginResult.IsSuccess = true;
                        }
                        else
                        {
                            clientLoginResult.ErrorMessage = Resources.M_CurrentPasswordNotCorrect; // UserLoginResultStatus.InactiveUser;
                        }
                    }
                    else
                    {
                        clientLoginResult.ErrorMessage = Resources.RecordDoesntExist; // UserLoginResultStatus.InvalidUsernamePassword;
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                clientLoginResult.ErrorMessage = Resources.M_InternalServerError; //UserLoginResultStatus.InternalServerError;
            }
            return clientLoginResult;
        }

        public GenericActionResult<Client> RecoverClientPassword(string recoveryData)
        {
            GenericActionResult<Client> clientRecoverPasswordResult = new GenericActionResult<Client>();
            try
            {
                int cardCode = 0;
                int.TryParse(recoveryData,out cardCode);
                if (cardCode == 0)
                {
                    cardCode = -10;
                }
                using (DamaconEntities context = new DamaconEntities())
                {
                    Client client = context.Clients.FirstOrDefault(u => recoveryData.Equals(u.Email) || cardCode == u.CardCode);

                    if (client != null)
                    {
                        if (string.IsNullOrWhiteSpace(client.Email))
                        {
                            clientRecoverPasswordResult.ErrorMessage = Resources.M_ClientEmailNoSet;
                        }
                        else
                        {
                            clientRecoverPasswordResult.SetSingleResult(client);
                            clientRecoverPasswordResult.IsSuccess = true;
                        }
                    }
                    else
                    {
                        clientRecoverPasswordResult.ErrorMessage = Resources.M_CardCodeOrEmailNotCorrect; // UserLoginResultStatus.InvalidUsernamePassword;
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                clientRecoverPasswordResult.ErrorMessage = Resources.M_InternalServerError; //UserLoginResultStatus.InternalServerError;
            }
            return clientRecoverPasswordResult;
        }

        public GenericActionResult ChangeClientEmail(long clientID, string newEmail)
        {
            GenericActionResult clientLoginResult = new GenericActionResult();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    Client client = context.Clients.FirstOrDefault(u => u.ID == clientID);

                    if (client != null)
                    {
                        if (!context.Clients.Any(u => u.ID != clientID && u.Email == newEmail))
                        {
                            client.Email = newEmail;
                            context.SaveChanges();
                            clientLoginResult.IsSuccess = true;
                        }
                        else
                        {
                            clientLoginResult.ErrorMessage = Resources.M_ClientWithEmailExists; // UserLoginResultStatus.InactiveUser;
                        }
                    }
                    else
                    {
                        clientLoginResult.ErrorMessage = Resources.RecordDoesntExist; // UserLoginResultStatus.InvalidUsernamePassword;
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                clientLoginResult.ErrorMessage = Resources.M_InternalServerError; //UserLoginResultStatus.InternalServerError;
            }
            return clientLoginResult;
        }
    }
}
