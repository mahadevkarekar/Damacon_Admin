using Damacon.DAL.Operations.Contracts;
using System;
using System.Linq;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;

namespace Damacon.DAL.Operations.Concrete
{
    internal class StaticDataDAL : IStaticDataDAL
    {
        public GenericActionResult<Country> GetAllCountries()
        {
            GenericActionResult<Country> result = new GenericActionResult<Country>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.Countries.ToList();
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

        public GenericActionResult<UserType> GetAllUserTypes()
        {
            GenericActionResult<UserType> result = new GenericActionResult<UserType>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.UserTypes.ToList();
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError; ;
            }
            return result;
        }

        public GenericActionResult<DocumentationType> GetAllDocumentationTypes()
        {
            GenericActionResult<DocumentationType> result = new GenericActionResult<DocumentationType>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.DocumentationTypes.Where(x => !x.Deleted && !x.Disable).ToList();
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError; ;
            }
            return result;
        }

        //public GenericActionResult<EmployeesCommisionType> GetAllEmployeesCommisionTypes()
        //{
        //    GenericActionResult<EmployeesCommisionType> result = new GenericActionResult<EmployeesCommisionType>();
        //    try
        //    {
        //        using (DamaconEntities context = new DamaconEntities())
        //        {
        //            result.Result = context.EmployeesCommisionTypes.ToList();
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

        public GenericActionResult<LeaveType> GetAllLeaveTypes()
        {
            GenericActionResult<LeaveType> result = new GenericActionResult<LeaveType>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.LeaveTypes.ToList();
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError; ;
            }
            return result;
        }

        public GenericActionResult<LanguageMeta> GetAllLanguageMetas()
        {
            GenericActionResult<LanguageMeta> result = new GenericActionResult<LanguageMeta>();
            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    result.Result = context.LanguageMetas.Where(x => x.IsActive).ToList();
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
                result.ErrorMessage = Resources.M_InternalServerError; ;
            }
            return result;
        }
    }
}
