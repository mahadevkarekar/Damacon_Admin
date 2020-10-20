using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Damacon.WebApp.Models
{
    public class StoreModel: AddressBaseModel<long>
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Company", ResourceType = typeof(DAL.i18n.Resources))]
        public long CompanyID { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "StoreName", ResourceType = typeof(DAL.i18n.Resources))]
        public string StoreName { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "StoreShortName", ResourceType = typeof(DAL.i18n.Resources))]
        public string StoreShortName { get; set; }

        [Display(Name = "Brand", ResourceType = typeof(DAL.i18n.Resources))]
        public string Brand { get; set; }

        [Display(Name = "EmailAlertEnable", ResourceType = typeof(DAL.i18n.Resources))]
        public bool EmailAlertEnable { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }

        [Display(Name = "IsStoreOffice", ResourceType = typeof(DAL.i18n.Resources))]
        public bool IsOffice { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Brand", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<long> BrandID { get; set; }

        [Display(Name = "WebEnable", ResourceType = typeof(DAL.i18n.Resources))]
        public bool IsWebEnable { get; set; }

        [Display(Name = "WebName", ResourceType = typeof(DAL.i18n.Resources))]
        public string WebName { get; set; }

        [Display(Name = "WebAddress", ResourceType = typeof(DAL.i18n.Resources))]
        public string WebAddress { get; set; }

        [Display(Name = "EnableBonus", ResourceType = typeof(DAL.i18n.Resources))]
        public bool IsBonusEnable { get; set; }

        [Display(Name = "WebLink", ResourceType = typeof(DAL.i18n.Resources))]
        public string WebLink { get; set; }

        public List<UserModel> StoreManagerUsers { get; set; }
        public List<EmployeeModel> StoreEmployees { get; set; }

        public string UsersDisplayText
        {
            get
            {
                if (StoreManagerUsers != null)
                {
                    return StoreManagerUsers.Where(x => x != null).Select(x => x.FullName).OrderBy(x => x).GetInfoFormatString();
                }
                return string.Empty;
            }
        }

        public string EmployeesDisplayText
        {
            get
            {
                if (StoreEmployees != null)
                {
                    return StoreEmployees.Where(x => x != null).Where(x => !x.Disable).Select(x => x.FullName).OrderBy(x => x).GetInfoFormatString();
                }
                return string.Empty;
            }
        }

        [Display(Name = "Company", ResourceType = typeof(DAL.i18n.Resources))]
        public string CompanyName { get; set; }

        [Display(Name = "Brand", ResourceType = typeof(DAL.i18n.Resources))]
        public string BrandName { get; set; }

        public string BrandColor { get; set; }

        [Display(Name = "EmployeesCommisionType", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<long> EmployeesCommisionTypeID { get; set; }

        [Display(Name = "LocalHoliday", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<System.DateTime> LocalHoliday { get; set; }

        [Display(Name = "CommisionType", ResourceType = typeof(DAL.i18n.Resources))]
        public string EmployeesCommisionTypeText { get; set; }
    }
}