using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Learning.Application.Senders
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            //const string fromEmail = "support@MoneyMagnet.com";
            //var message = new MailMessage
            //{
            //    From = new MailAddress(fromEmail),
            //    To = { to },
            //    Subject = subject,
            //    Body = body,
            //    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            //};
            //using (SmtpClient smtpClient = new SmtpClient("webmail.MoneyMagnet.com"))
            //{
            //    smtpClient.Credentials = new NetworkCredential("support@MoneyMagnet.com", "%s5yrI088");
            //    smtpClient.Port = 25;
            //    smtpClient.EnableSsl = true;
            //    smtpClient.Send(message);
            //}
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("MoneyMagnet2022@gmail.com", "آموزشگاه MoneyMagnet");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("MoneyMagnet2022@gmail.com", "yxhomesqrcpxfaxc");
            SmtpServer.EnableSsl = true; // only for port 465
            SmtpServer.Send(mail);

        }
    }
}