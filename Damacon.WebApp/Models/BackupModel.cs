using System.ComponentModel.DataAnnotations;

namespace Damacon.WebApp.Models
{
    public class BackupModel
    {
        [Display(Name = "LastBackupFileCreated", ResourceType = typeof(DAL.i18n.Resources))]
        public string BackupFileName { get; set; }
        [Display(Name = "LastBackupCreatedByUser", ResourceType = typeof(DAL.i18n.Resources))]
        public string BackupUserName { get; set; }
    }
}