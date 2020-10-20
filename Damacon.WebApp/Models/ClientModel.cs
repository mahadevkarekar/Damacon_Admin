using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Damacon.WebApp.Helpers;
using Damacon.SharedWeb.Helpers;

namespace Damacon.WebApp.Models
{
    public class ClientModel : PersonModel<long>
    {
        [Display(Name = "DateOfBirth", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<System.DateTime> DateBirth { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }

        [Display(Name = "RegisteredBy", ResourceType = typeof(DAL.i18n.Resources))]
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public long RegisteredByStoreID { get; set; }

        [Display(Name = "RegisteredOn", ResourceType = typeof(DAL.i18n.Resources))]
        public System.DateTime RegisteredOnDateTime { get; set; }

        [Display(Name = "Username", ResourceType = typeof(DAL.i18n.Resources))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(DAL.i18n.Resources))]
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public string ClientPassword { get; set; }

        [RegularExpression(@"^[0-9]{1,20}$", ErrorMessageResourceName = "M_OnlyNumbersAreAllowed", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [StringLength(20, ErrorMessageResourceName = "M_MobilePhoneMustBeMinimun8Digit", ErrorMessageResourceType = typeof(DAL.i18n.Resources), MinimumLength = 7)]
        [Display(Name = "MobilePhone", ResourceType = typeof(DAL.i18n.Resources))]
        public string MobilePhoneEx { get; set; }

        [Display(Name = "Email", ResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "M_InvalidEmail", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public string EmailEx { get; set; }

        [Display(Name = "RegisteredBy", ResourceType = typeof(DAL.i18n.Resources))]
        public string RegisteredByStoreName { get; set; }

        [Display(Name = "CountryPhoneCode", ResourceType = typeof(DAL.i18n.Resources))]
        public string CountryMobileCode { get; set; }

        public List<string> ShoppingStores { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "CardCode", ResourceType = typeof(DAL.i18n.Resources))]
        public int? CardCode { get; set; }

        public string StoresDisplayText
        {
            get
            {
                if (ShoppingStores != null)
                {
                    return ShoppingStores.OrderBy(x => x).GetInfoFormatString();
                }
                return string.Empty;
            }
        }
    }
}