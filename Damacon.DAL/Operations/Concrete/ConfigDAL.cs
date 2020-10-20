using Damacon.DAL.Operations.Contracts;
using System;
using Damacon.DAL.Database.EF;
using System.Linq;
using Damacon.DAL.i18n;

namespace Damacon.DAL.Operations.Concrete
{
    internal class ConfigDAL : IConfigDAL
    {
        public GenericActionResult<WebConfiguration> AddOrUpdate(string key, string value, UserLite addedByUser, string accessIP)
        {
            GenericActionResult<WebConfiguration> addUpdateWebConfigurationResult = new GenericActionResult<WebConfiguration>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {

                        WebConfiguration webConfiguration = context.WebConfigurations.FirstOrDefault(u => u.ConfigKey.Equals(key, StringComparison.OrdinalIgnoreCase));
                        if (webConfiguration == null)
                        {
                            WebConfiguration newWebConfiguration = new WebConfiguration();
                            newWebConfiguration.ConfigKey = key;
                            newWebConfiguration.ConfigValue = value;
                            newWebConfiguration.LastModifyUserID = addedByUser.ID;
                            newWebConfiguration.LastModifyDateTime = DateTime.Now;
                            newWebConfiguration.LastModifyIP = accessIP;
                            context.WebConfigurations.Add(newWebConfiguration);
                            context.SaveChanges();
                            transactionScope.Commit();
                            addUpdateWebConfigurationResult.SetSingleResult(newWebConfiguration);
                            addUpdateWebConfigurationResult.IsSuccess = true;
                        }
                        else
                        {
                            webConfiguration.ConfigValue = value;
                            webConfiguration.LastModifyUserID = addedByUser.ID;
                            webConfiguration.LastModifyDateTime = DateTime.Now;
                            webConfiguration.LastModifyIP = accessIP;
                            context.SaveChanges();
                            transactionScope.Commit();
                            addUpdateWebConfigurationResult.SetSingleResult(webConfiguration);
                            addUpdateWebConfigurationResult.IsSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                addUpdateWebConfigurationResult.ErrorMessage = Resources.M_InternalServerError;
            }
            return addUpdateWebConfigurationResult;
        }

        public GenericActionResult<WebConfiguration> GetByKey(string key)
        {
            GenericActionResult<WebConfiguration> getWebConfigurationResult = new GenericActionResult<WebConfiguration>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    using (var transactionScope = context.Database.BeginTransaction())
                    {

                        WebConfiguration webConfiguration = context.WebConfigurations.FirstOrDefault(u => u.ConfigKey.Equals(key, StringComparison.OrdinalIgnoreCase));
                        if (webConfiguration != null)
                        {
                            getWebConfigurationResult.SetSingleResult(webConfiguration);
                            getWebConfigurationResult.IsSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                getWebConfigurationResult.ErrorMessage = Resources.M_InternalServerError;
            }
            return getWebConfigurationResult;
        }
    }
}
