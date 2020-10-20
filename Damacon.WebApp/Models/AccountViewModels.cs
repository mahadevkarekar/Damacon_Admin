using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
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

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
