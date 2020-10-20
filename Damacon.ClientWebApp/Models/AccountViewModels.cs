using System.ComponentModel.DataAnnotations;

namespace Damacon.ClientWebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Username", ResourceType =typeof(DAL.i18n.Resources))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DAL.i18n.Resources))]
        public string Password { get; set; }

        public bool IsPrivacyPolicyAccepted { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(DAL.i18n.Resources))]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(DAL.i18n.Resources))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Compare("NewPassword", ErrorMessageResourceName = "M_PasswordConfirmPasswordDontMatch", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(DAL.i18n.Resources))]
        public string ConfirmNewPassword { get; set; }
    }

    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "EnterCardCodeOrEmail", ResourceType = typeof(DAL.i18n.Resources))]
        public string CardCodeOrEmail { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Display(Name = "CurrentEmail", ResourceType = typeof(DAL.i18n.Resources))]
        public string CurrentEmail { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "M_InvalidEmail", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "NewEmail", ResourceType = typeof(DAL.i18n.Resources))]
        public string NewEmail { get; set; }
    }
}
