using System.Net;
using System.Net.Mail;
//using System.Web.Mvc;

namespace JB.Helpers
{
    public class GMailService    {

        public static MailMessage CreateMailMessage(string to = "", string subject = "", string body = "")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EnvironmentSecret.Instance.EmailAdmin);
            mail.ReplyToList.Add( new MailAddress(EnvironmentSecret.Instance.EmailAdmin));
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body =body;
            mail.IsBodyHtml = true;
            return mail;
        }

        public static void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            client.Host = EnvironmentSecret.Instance.SmtpHost;
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(
                EnvironmentSecret.Instance.NetworkCredentialUserName,
                EnvironmentSecret.Instance.NetworkCredentialPassword);
            client.Send(Message);
        }

        public static void SendMail(string to = "", string subject = "", string body = "", string unsubscribeUserId ="")
        {
            var message = CreateMailMessage(to, subject, body);
            if (unsubscribeUserId.Length>0)
            {
                //message = AddUnsubscribe(message, unsubscribeUserId);
            }
            SendMail (message);

        }

        public static void SendMailNewWhatsAppMember(string userName, string phoneNumber)
        {
            SendMail(CreateMailMessage(EnvironmentSecret.Instance.EmailAdmin, $"Add {userName} with {phoneNumber} to Metropole Whats App Group", userName + ":" + phoneNumber));
        }

        



    }
}