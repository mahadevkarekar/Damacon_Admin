using Damacon.DAL.Database.EF;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Damacon.WebApp.Models
{
    public class UserModel : PersonModel<long>
    {

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^[0-9a-zA-Z''-']{1,20}$", ErrorMessageResourceName = "M_SpecialCharactersAndSpaceNotAllowed", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Username", ResourceType = typeof(DAL.i18n.Resources))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Password", ResourceType = typeof(DAL.i18n.Resources))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "UserType", ResourceType = typeof(DAL.i18n.Resources))]
        public long UserTypeID { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "HM_Store", ResourceType = typeof(DAL.i18n.Resources))]
        public Nullable<long> StoreID { get; set; }

        public string LastAccessTimeFormatted
        {
            get
            {
                if (LastAccessTime.HasValue)
                {
                    return LastAccessTime.Value.ToString(UIExtensions.DateTimeFormat);
                }
                return string.Empty;
            }
        }

        public Nullable<System.DateTime> LastAccessTime { get; set; }

        public string LastAccessIP { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public int DefaultLoginPage { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }

        public string PitcurePath { get; set; }

        [Display(Name = "UploadPhoto", ResourceType = typeof(DAL.i18n.Resources))]
        public IEnumerable<HttpPostedFileBase> ProfileImageFile { get; set; }

        [Display(Name = "UserType", ResourceType = typeof(DAL.i18n.Resources))]
        public string UserTypeString { get; set; }

        [Display(Name = "HM_Store", ResourceType = typeof(DAL.i18n.Resources))]
        public string StoreName { get; set; }
    }
}