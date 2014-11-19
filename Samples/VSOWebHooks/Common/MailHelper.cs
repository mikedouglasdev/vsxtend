using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace VSOWebHooks.Common
{
    public class MailHelper
    {
        public void SendMessage(string from, string subject, string to, string body, string smtpServer, int smtpPort, string password)
        {
            var mailMessage = new MailMessage(from, to, subject, body);
            var credentials = new NetworkCredential(from, password);
            var smtpClient = new SmtpClient(smtpServer, smtpPort);

            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMessage);
        }
    }
}