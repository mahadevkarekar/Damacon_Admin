using System;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class DocumentationModel : BaseModel<long>
    {
        [Display(Name = "DocumentationTitle", ResourceType = typeof(DAL.i18n.Resources))]
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public string Title { get; set; }

        [Display(Name = "DocumentationType", ResourceType = typeof(DAL.i18n.Resources))]
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public long DocumentationTypeID { get; set; }

        [Display(Name = "Distribution", ResourceType = typeof(DAL.i18n.Resources))]
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        public int StoreSelectionType { get; set; }

        [Display(Name = "Tag", ResourceType = typeof(DAL.i18n.Resources))]
        public string Tag { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }

        [Display(Name = "UploadDocumentation", ResourceType = typeof(DAL.i18n.Resources))]
        public string FilePath { get; set; }

        [Display(Name = "DocumentationType", ResourceType = typeof(DAL.i18n.Resources))]
        public virtual string DocumentationTypeText { get; set; }

        public long[] StoreIds { get; set; }

        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return string.Empty;
                }
                else
                {
                    return FilePath.Substring(FilePath.IndexOf("_") + 1);
                }
            }
        }

        public bool IsLocked { get; set; }

        public DateTime UploadDate { get; set; }

        public string StoreSelection { get; set; }
    }
}