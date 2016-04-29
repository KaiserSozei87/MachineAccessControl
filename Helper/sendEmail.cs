using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;


namespace Helper
{
    public class sendEmail
    {
        

        public sendEmail() { }

        public static void SendMail(emailMessage message, string _sentFrom)
        {
            var credentialUserName = "";
            var sentFrom = _sentFrom;
            var pwd = "";

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("149.223.200.206");

            client.Port = 25;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


            // Creatte the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);
            client.EnableSsl = false;
            //set to false if wanting to provide your won credentials
            client.UseDefaultCredentials = true;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = message.IsBodyHTML;

            client.Send(mail);
            //return Task.FromResult(0);
        }

        public static Task SendAsync(emailMessage message, string _sentFrom)
        {
            var credentialUserName = "";
            var sentFrom = _sentFrom;
            var pwd = "";

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("149.223.200.206");

            client.Port = 25;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            

            // Creatte the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);
            client.EnableSsl = false;
            //set to false if wanting to provide your won credentials
            client.UseDefaultCredentials = true;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = message.IsBodyHTML;

            return client.SendMailAsync(mail);
            //return Task.FromResult(0);
        }
    }

    public class emailMessage
    {

        public emailMessage()
        {
            //NOTHING TO SEE HERE
        }

        public string Body { get; set; }

        public string Destination { get; set; }

        public string Subject { get; set; }

        public bool IsBodyHTML { get; set; }
    }
}
