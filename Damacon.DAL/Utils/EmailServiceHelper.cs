using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace Damacon.DAL.Utils
{
    public static class EmailServiceHelper
    {
        public static bool SendTestEmail(string server, string username, string password, int port, bool isSSL, string recipientEmailAddress)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(recipientEmailAddress);
                mail.From = new MailAddress("Test@StoreD.com");
                mail.Subject = Resources.TestEmail;
                string Body = Resources.TestEmail;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = server;
                smtp.Port = port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(username, password); // Enter seders User name and password  
                smtp.EnableSsl = isSSL;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendEmail(EmailServiceItem emailServiceItem, string recipientEmailAddress)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(recipientEmailAddress);
                mail.From = new MailAddress(emailServiceItem.SenderEmail);
                mail.Subject = emailServiceItem.Subject;
                string Body = emailServiceItem.Body;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = emailServiceItem.SMTPServer;
                smtp.Port = emailServiceItem.SMTPPort;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(emailServiceItem.SMTPUser, emailServiceItem.SMTPPassword); // Enter seders User name and password  
                smtp.EnableSsl = emailServiceItem.IsSSL;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendEmailWithAttachment(EmailServiceItem emailServiceItem, string recipientEmailAddress, Stream attachmentData, string fileName, Dictionary<string, string> tokenToReplace)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(recipientEmailAddress);
                mail.From = new MailAddress(emailServiceItem.SenderEmail);

                if(tokenToReplace != null)
                {
                    foreach (var token in tokenToReplace)
                    {
                        emailServiceItem.Subject = emailServiceItem.Subject.Replace(token.Key, token.Value);
                        emailServiceItem.Body = emailServiceItem.Body.Replace(token.Key, token.Value);
                    }
                }
                
                mail.Subject = emailServiceItem.Subject;
                mail.Body = emailServiceItem.Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = emailServiceItem.SMTPServer;
                smtp.Port = emailServiceItem.SMTPPort;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(emailServiceItem.SMTPUser, emailServiceItem.SMTPPassword); // Enter seders User name and password  
                smtp.EnableSsl = emailServiceItem.IsSSL;

                if (attachmentData != null)
                {
                    attachmentData.Seek(0, SeekOrigin.Begin);
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    Attachment attach = new Attachment(attachmentData, ct);
                    attach.ContentDisposition.FileName = fileName;
                    mail.Attachments.Add(attach);
                }

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
