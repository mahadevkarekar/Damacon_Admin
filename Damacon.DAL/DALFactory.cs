using Damacon.DAL.Operations.Concrete;
using Damacon.DAL.Operations.Contracts;

namespace Damacon.DAL
{
    public class DALFactory
    {
        public static T GetDALObject<T>()
        {
            object dalObject = new object();
            if (typeof(T) == typeof(IUserDAL))
            {
                dalObject = new UserDAL();
            }
            //else if (typeof(T) == typeof(ICompanyDAL))
            //{
            //    dalObject = new CompanyDAL();
            //}
            //else if (typeof(T) == typeof(IStoreDAL))
            //{
            //    dalObject = new StoreDAL();
            //}
            //else if (typeof(T) == typeof(IGoodsTypeDAL))
            //{
            //    dalObject = new GoodsTypeDAL();
            //}
            //else if (typeof(T) == typeof(IWorkerTimeTypeDAL))
            //{
            //    dalObject = new WorkerTimeTypeDAL();
            //}
            //else if (typeof(T) == typeof(IDocumentTypeDAL))
            //{
            //    dalObject = new DocumentTypeDAL();
            //}
            else if (typeof(T) == typeof(ILanguageDAL))
            {
                dalObject = new LanguageDAL();
            }
            //else if (typeof(T) == typeof(IBuyerDAL))
            //{
            //    dalObject = new BuyerDAL();
            //}
            //else if (typeof(T) == typeof(ISupplierDAL))
            //{
            //    dalObject = new SupplierDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeDAL))
            //{
            //    dalObject = new EmployeeDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeCompanyContractDAL))
            //{
            //    dalObject = new EmployeeCompanyContractDAL();
            //}
            //else if (typeof(T) == typeof(ISalesDAL))
            //{
            //    dalObject = new SalesDAL();
            //}
            //else if (typeof(T) == typeof(IDifferentialDAL))
            //{
            //    dalObject = new DifferentialDAL();
            //}
            //else if (typeof(T) == typeof(IWorkedHourDAL))
            //{
            //    dalObject = new WorkedHourDAL();
            //}
            else if (typeof(T) == typeof(IStaticDataDAL))
            {
                dalObject = new StaticDataDAL();
            }
            //else if (typeof(T) == typeof(IReportsDAL))
            //{
            //    dalObject = new ReportsDAL();
            //}
            //else if (typeof(T) == typeof(IDocumentRegistrationDAL))
            //{
            //    dalObject = new DocumentRegistrationDAL();
            //}
            else if (typeof(T) == typeof(IBackupDAL))
            {
                dalObject = new BackupDAL();
            }
            else if (typeof(T) == typeof(IEmailServiceDAL))
            {
                dalObject = new EmailServiceDAL();
            }
            //else if (typeof(T) == typeof(IBrandDAL))
            //{
            //    dalObject = new BrandDAL();
            //}
            //else if (typeof(T) == typeof(IWorkersTimeTableDAL))
            //{
            //    dalObject = new WorkersTimeTableDAL();
            //}
            else if (typeof(T) == typeof(IDocumentationDAL))
            {
                dalObject = new DocumentationDAL();
            }
            //else if (typeof(T) == typeof(IBuyerManagementDAL))
            //{
            //    dalObject = new BuyerManagementDAL();
            //}
            else if (typeof(T) == typeof(IClientDAL))
            {
                dalObject = new ClientDAL();
            }
            //else if (typeof(T) == typeof(IClientShoppingDAL))
            //{
            //    dalObject = new ClientShoppingDAL();
            //}
            //else if (typeof(T) == typeof(IExtraPaymentSettingsDAL))
            //{
            //    dalObject = new ExtraPaymentSettingsDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeExtraPayDAL))
            //{
            //  dalObject = new EmployeeExtraPayDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeLevelCostDAL))
            //{
            //    dalObject = new EmployeeLevelCostDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeExtraPayMonthHolidayDAL))
            //{
            //  dalObject = new EmployeeExtraPayMonthHolidayDAL();
            //}
            //else if (typeof(T) == typeof(IEmployeeExtraPayStoreRefoundDAL))
            //{
            //  dalObject = new EmployeeExtraPayStoreRefoundDAL();
            //}
            else if (typeof(T) == typeof(IHolidayDAL))
            {
              dalObject = new HolidayDAL();
            }
            //else if (typeof(T) == typeof(IShoppingDetailDAL))
            //{
            //    dalObject = new ShoppingDetailDAL();
            //}
            else if (typeof(T) == typeof(IConfigDAL))
            {
                dalObject = new ConfigDAL();
            }
            return (T)(dalObject);
        }
    }
}
