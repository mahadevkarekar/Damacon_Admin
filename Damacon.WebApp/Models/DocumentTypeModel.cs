using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class DocumentTypeModel: BaseModel<long>
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "DocumentTypeText", ResourceType = typeof(DAL.i18n.Resources))]
        public string TypeText { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }
    }
}