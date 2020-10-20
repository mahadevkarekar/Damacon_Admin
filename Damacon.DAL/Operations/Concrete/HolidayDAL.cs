using System;
using System.Linq;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using System.Data.Entity;

namespace Damacon.DAL.Operations.Concrete
{
  internal class HolidayDAL : IHolidayDAL
  {
    public GenericActionResult<Holiday> AddNew(Holiday newRecord, UserLite addedByUser, string accessIP)
    {
      GenericActionResult<Holiday> addNewHoliday = new GenericActionResult<Holiday>();
      try
      {
        using (DamaconEntities context = new DamaconEntities())
        {
          using (var transactionScope = context.Database.BeginTransaction())
          {

            newRecord.LastModifyUserID = addedByUser.ID;
            newRecord.LastModifyDateTime = DateTime.Now;
            newRecord.LastModifyIP = accessIP;
            context.Holidays.Add(newRecord);
            context.SaveChanges();
            transactionScope.Commit();
            addNewHoliday.SetSingleResult(newRecord);
            addNewHoliday.IsSuccess = true;

          }
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        addNewHoliday.ErrorMessage = Resources.M_InternalServerError;
      }
      return addNewHoliday;
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
            var eepRecords = context.Holidays.Where(x => x.ID == id);

            if (eepRecords != null)
            {
              foreach (Holiday eepRecord in eepRecords)
              {
                context.Holidays.Remove(eepRecord);
              }

              context.SaveChanges();
              transactionScope.Commit();
              deleteRecordDataResult.IsSuccess = true;

            }
          }
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        deleteRecordDataResult.ErrorMessage = i18n.Resources.M_InternalServerError;
      }
      return deleteRecordDataResult;
    }

    public GenericActionResult DeleteMonthRecord(int iYear, int iMonth)
    {
      GenericActionResult deleteRecordDataResult = new GenericActionResult();
      try
      {
        using (DamaconEntities context = new DamaconEntities())
        {
          using (var transactionScope = context.Database.BeginTransaction())
          {
            var eepRecords = context.Holidays.Where(x => x.HolidayDate.Year == iYear && x.HolidayDate.Month == iMonth);

            if (eepRecords != null)
            {
              foreach (Holiday eepRecord in eepRecords)
              {
                context.Holidays.Remove(eepRecord);
              }

              context.SaveChanges();
              transactionScope.Commit();
              deleteRecordDataResult.IsSuccess = true;

            }
          }
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        deleteRecordDataResult.ErrorMessage = i18n.Resources.M_InternalServerError;
      }
      return deleteRecordDataResult;
    }

    public GenericActionResult<Holiday> GetAll(bool isGetDeleted)
    {
      GenericActionResult<Holiday> result = new GenericActionResult<Holiday>();
      try
      {
        using (DamaconEntities context = new DamaconEntities())
        {
          result.Result = context.Holidays.ToList();
          result.IsSuccess = true;
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        result.ErrorMessage = i18n.Resources.M_InternalServerError;
      }
      return result;
    }

    public GenericActionResult<Holiday> GetById(long id)
    {
      throw new NotImplementedException();
    }

    public bool IsDeleteAllowed(Holiday recordToDelete)
    {
      throw new NotImplementedException();
    }

    public GenericActionResult<Holiday> Update(Holiday updateUser, UserLite updatedByUser, string accessIP)
    {
      throw new NotImplementedException();
    }


    public GenericActionResult<Holiday> GetByDate(int iYear, int iMonth)
    {
      GenericActionResult<Holiday> result = new GenericActionResult<Holiday>();
      try
      {
        using (DamaconEntities context = new DamaconEntities())
        {
          result.Result = context.Holidays.Where(mh => mh.HolidayDate.Year == iYear && mh.HolidayDate.Month == iMonth).ToList();
          result.IsSuccess = true;
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        result.ErrorMessage = i18n.Resources.M_InternalServerError;
      }
      return result;
    }

    public GenericActionResult<Holiday> GetByDate(int iYear, int iMonth, int iDay)
    {
      GenericActionResult<Holiday> result = new GenericActionResult<Holiday>();
      try
      {
        using (DamaconEntities context = new DamaconEntities())
        {
          result.Result = context.Holidays.Where(mh => mh.HolidayDate.Year == iYear && mh.HolidayDate.Month == iMonth && mh.HolidayDate.Day == iDay).ToList();
          result.IsSuccess = true;
        }
      }
      catch (Exception ex)
      {
        //LoggerManager.Logger.Error(ex);
        result.ErrorMessage = i18n.Resources.M_InternalServerError;
      }
      return result;
    }

  }
}
