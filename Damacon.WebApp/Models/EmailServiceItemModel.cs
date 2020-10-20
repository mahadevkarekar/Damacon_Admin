using System;
using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class EmailServiceItemModel : BaseModel<long>
    {
        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "EmailType", ResourceType = typeof(DAL.i18n.Resources))]
        public int SendCondition { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Description", ResourceType = typeof(DAL.i18n.Resources))]
        public string Description { get; set; }

        [Display(Name = "SendTime", ResourceType = typeof(DAL.i18n.Resources))]
        public string SendTime { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "SmtpServer", ResourceType = typeof(DAL.i18n.Resources))]
        public string SMTPServer { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "SmtpUser", ResourceType = typeof(DAL.i18n.Resources))]
        public string SMTPUser { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "SmtpPassword", ResourceType = typeof(DAL.i18n.Resources))]
        public string SMTPPassword { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Port", ResourceType = typeof(DAL.i18n.Resources))]
        public int SMTPPort { get; set; }

        [Display(Name = "SSL", ResourceType = typeof(DAL.i18n.Resources))]
        public bool IsSSL { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "M_InvalidEmail", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Sender", ResourceType = typeof(DAL.i18n.Resources))]
        public string SenderEmail { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "EmailRecipients", ResourceType = typeof(DAL.i18n.Resources))]
        public string RecipientEmail { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "Subject", ResourceType = typeof(DAL.i18n.Resources))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "M_FieldRequired", ErrorMessageResourceType = typeof(DAL.i18n.Resources))]
        [Display(Name = "EmailText", ResourceType = typeof(DAL.i18n.Resources))]
        public string Body { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(DAL.i18n.Resources))]
        public string Notes { get; set; }

        [Display(Name = "LastSentDateTime", ResourceType = typeof(DAL.i18n.Resources))]
        public DateTime? LastSentTime { get; set; }

        [Display(Name = "DataDate", ResourceType = typeof(DAL.i18n.Resources))]
        public int? DataDate { get; set; }
    }
}