using System;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class EmailTaskResultItemModel : BaseModel<long>
    {
        [Display(Name = "EmailType", ResourceType = typeof(DAL.i18n.Resources))]
        public int SendCondition { get; set; }
        [Display(Name = "LastSentResult", ResourceType = typeof(DAL.i18n.Resources))]
        public string SentResult { get; set; }
        [Display(Name = "SentDateTime", ResourceType = typeof(DAL.i18n.Resources))]
        public System.DateTime SentDateTime { get; set; }
        [Display(Name = "Sender", ResourceType = typeof(DAL.i18n.Resources))]
        public string SenderEmail { get; set; }
        [Display(Name = "Recepients", ResourceType = typeof(DAL.i18n.Resources))]
        public string RecipientEmail { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<long> SenderStoreID { get; set; }
        public Nullable<long> RecipientStoreID { get; set; }
        public string StoreName { get; set; }
    }
}