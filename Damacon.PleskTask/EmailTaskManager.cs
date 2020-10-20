using Damacon.DAL;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.DAL.Operations.Contracts;
using Damacon.DAL.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace Damacon.PleskTask
{
    public class EmailTaskManager
     {
    //    public void ProcessEmailTasks()
    //    {
    //        LoggingService.Log("Email Service Started");
    //        var emailTasksResult = DALFactory.GetDALObject<IEmailServiceDAL>().GetAll(false);
    //        if (emailTasksResult.IsSuccess)
    //        {
    //            var currentDateTime = DateTime.Now;
    //            var applicableEmailTasks = emailTasksResult.Result.Where(x => !x.Disable
    //            && x.SendTime.Value.Hour == currentDateTime.Hour
    //            && currentDateTime.Minute - x.SendTime.Value.Minute < 29);

    //            LoggingService.Log("Number of applicable email tasks: " + applicableEmailTasks.Count());
    //            if (applicableEmailTasks.Any())
    //            {
    //                foreach (var emailTask in applicableEmailTasks)
    //                {
    //                    LoggingService.Log("Starting email task: " + emailTask.SendCondition.ToString());
    //                    if (emailTask.SendCondition == EmailSendConditionType.SalesNWorkedHourNotCompiled)
    //                    {
    //                        MarkTaskExecutionStatus(emailTask.ID);
    //                        ProcessAndSendSalesNWorkedHourNotCompiledEmailTask(emailTask);
    //                    }
    //                    LoggingService.Log("Finishing email task: " + emailTask.SendCondition.ToString());
    //                }
    //            }
    //        }
    //    }

    //    private void ProcessAndSendSalesNWorkedHourNotCompiledEmailTask(EmailServiceItem emailTask)
    //    {
    //        var currentDateTime = DateTime.Now;
    //        if (emailTask.DataDate == EmailDataDateType.DataDateYestarday)
    //        {
    //            currentDateTime = currentDateTime.AddDays(-1);
    //        }
    //        using (DamaconEntities context = new DamaconEntities())
    //        {
    //            var stores = context.Stores.Where(x => !x.Disable && !x.Deleted && x.EmailAlertEnable).ToList();

    //            foreach (Store store in stores)
    //            {
    //                bool isSalesDataCompiled = false;
    //                Sale saleDataForStore = context.Sales.FirstOrDefault(x => x.StoreID == store.ID && DbFunctions.TruncateTime(x.SalesDate) == DbFunctions.TruncateTime(currentDateTime));
    //                if (saleDataForStore != null && saleDataForStore.Lock)
    //                {
    //                    isSalesDataCompiled = true;
    //                }


    //                bool isWorkedHourDataCompiled = false;
    //                var employeeContracts = context.EmployeeCompanyContracts.Where(x => !x.Deleted && x.EmployeeStoreContracts.Any(esc => esc.StoreID == store.ID))
    //                   .Where(x => DbFunctions.TruncateTime(x.ContractStartDate) <= DbFunctions.TruncateTime(currentDateTime) &&
    //                               (x.ContractEndDate == null || DbFunctions.TruncateTime(x.ContractEndDate) >= DbFunctions.TruncateTime(currentDateTime))).ToList();
    //                if (!employeeContracts.Any())
    //                {
    //                    isWorkedHourDataCompiled = true;
    //                }
    //                else
    //                {
    //                    var workedHourData = context.WorkedHours.Where(x => x.StoreID == store.ID && DbFunctions.TruncateTime(x.WorkedDate) == DbFunctions.TruncateTime(currentDateTime));
    //                    if (workedHourData.Any(x => x.Lock == true))
    //                    {
    //                        isWorkedHourDataCompiled = true;
    //                    }
    //                }

    //                if (!isSalesDataCompiled || !isWorkedHourDataCompiled)
    //                {
    //                    if (!string.IsNullOrEmpty(store.Email))
    //                    {
    //                        bool result = EmailServiceHelper.SendEmail(emailTask, store.Email);
    //                        AddToSentEmail(context, emailTask.SendCondition, result ? "M_EmailSentSuccessfully" : "M_InvalidEmailCredentials", emailTask.SenderEmail, store.Email, store.ID, null, store.ID);
    //                    }
    //                    else
    //                    {
    //                        AddToSentEmail(context, emailTask.SendCondition, "M_EmailNotConfiguredInStore", emailTask.SenderEmail, string.Empty, store.ID, null, store.ID);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    private void MarkTaskExecutionStatus(long taskID)
    //    {
    //        using (DamaconEntities context = new DamaconEntities())
    //        {
    //            var emailServiceItem = context.EmailServiceItems.FirstOrDefault(x => x.ID == taskID);
    //            if (emailServiceItem != null)
    //            {
    //                emailServiceItem.LastSentTime = DateTime.Now;
    //                context.SaveChanges();
    //            }
    //        }
    //    }

    //    private void AddToSentEmail(DamaconEntities context, EmailSendConditionType emailSendConditionType, string sentResult, string senderEmail,
    //        string recipientEmail, long storeID, long? senderStoreID, long? recipientStoreID)
    //    {
    //        EmailTaskResultItem emailTaskResultItem = new EmailTaskResultItem();
    //        emailTaskResultItem.SendCondition = emailSendConditionType;
    //        emailTaskResultItem.SentResult = sentResult;
    //        emailTaskResultItem.SentDateTime = DateTime.Now;
    //        emailTaskResultItem.SenderEmail = senderEmail;
    //        emailTaskResultItem.RecipientEmail = recipientEmail;
    //        emailTaskResultItem.StoreID = storeID;
    //        emailTaskResultItem.SenderStoreID = senderStoreID;
    //        emailTaskResultItem.RecipientStoreID = recipientStoreID;
    //        var emailServiceItem = context.EmailTaskResultItems.Add(emailTaskResultItem);
    //        context.SaveChanges();
    //    }
    }
}

