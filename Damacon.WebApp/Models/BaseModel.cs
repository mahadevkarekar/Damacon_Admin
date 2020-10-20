using Damacon.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public abstract class BaseModel<T>
    {
        public T ID { get; set; }
        public long LastModifyUserID { get; set; }

        public string LastModifyIP { get; set; }

        [Display(Name = "State", ResourceType = typeof(DAL.i18n.Resources))]
        public bool Disable { get; set; }

        public bool Deleted { get; set; }
                
        [Display(Name = "LASTCHANGEDATEHOUR", ResourceType = typeof(DAL.i18n.Resources))]
        public string LastModifyDateTimeFormatted { get; set; }
        
    }
}