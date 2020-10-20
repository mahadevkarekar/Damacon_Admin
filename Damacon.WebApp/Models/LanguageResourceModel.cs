using System;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class LanguageResourceModel : BaseModel<string>
    {
        public string EnglishValue { get; set; }
        public string ItalianoValue { get; set; }
        public Nullable<int> MaxLenght { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }
    }
}