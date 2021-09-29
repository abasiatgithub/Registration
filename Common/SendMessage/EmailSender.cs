using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common.SendMessage
{
    public class EmailSender
    {
        public void SendEmail(string email, string subject, string message)
        {

            using (var Client = new SmtpClient())
            {
                var Credential = new NetworkCredential
                {
                    UserName = "ars.abasi1400",
                    Password = "14952051",
                };

                Client.UseDefaultCredentials = false;
                Client.Credentials = Credential;
                Client.Host = "smtp.gmail.com";
                Client.Port = 587;
                Client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("ars.abasi1400@gmail.com");
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = message;

                    Client.Send(emailMessage);
                };

            }
        }
    }
}
