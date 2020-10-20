using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
  public interface IHolidayDAL : ICrudBaseDAL<Holiday>
  {
    
    GenericActionResult DeleteMonthRecord(int iYear, int iMonth);

    GenericActionResult<Holiday> GetByDate(int iYear, int iMonth);

    GenericActionResult<Holiday> GetByDate(int iYear, int iMonth, int iDay);
  }
}
