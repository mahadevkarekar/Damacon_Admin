using System;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class AddressBaseModel<T> : BaseModel<T>
    {
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,50}$", ErrorMessageResourceName = "M_SpecialCharactersNotAllowed", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Address", ResourceType = typeof(DAL.i18n.Resources))]
        public string Address { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "Only number are allowed.")]
        public string ZipCode { get; set; }

        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,50}$", ErrorMessageResourceName = "M_SpecialCharactersNotAllowed", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "City", ResourceType = typeof(DAL.i18n.Resources))]
        public string City { get; set; }

        [RegularExpression(@"^[0-9a-zA-Z''-']{1,50}$", ErrorMessageResourceName = "M_SpecialCharactersAndSpaceNotAllowed", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Prov", ResourceType = typeof(DAL.i18n.Resources))]
        public string Prov { get; set; }

        [Display(Name = "Country", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<long> CountryID { get; set; }

        [Display(Name = "Email", ResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "M_InvalidEmail", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public string Email { get; set; }

        [RegularExpression(@"^\+?\d+$", ErrorMessageResourceName = "M_InvalidPhoneNumber", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Fax", ResourceType = typeof(DAL.i18n.Resources))]
        public string Fax { get; set; }

        [RegularExpression(@"^\+?\d+$", ErrorMessageResourceName = "M_InvalidPhoneNumber", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Phone", ResourceType = typeof(DAL.i18n.Resources))]
        public string Phone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(DAL.i18n.Resources))]
        public string FullAddress
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}", Address, ZipCode, City, Prov);
            }
        }
    }
}