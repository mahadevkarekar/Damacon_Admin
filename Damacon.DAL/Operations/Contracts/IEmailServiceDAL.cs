using Damacon.DAL.Database.EF;

namespace Damacon.DAL.Operations.Contracts
{
    public interface IEmailServiceDAL : ICrudBaseDAL<EmailServiceItem>
    {
        GenericActionResult<EmailTaskResultItem> GetEmailTaskResultItems();

        //GenericActionResult<EmailServiceItem> GetEmailServiceItemByType(EmailSendConditionType emailSendConditionType);
    }
}
