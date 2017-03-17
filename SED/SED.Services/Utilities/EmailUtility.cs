using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SED.Utilities
{
    public class EmailUtility
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailUtility));

        public static bool SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                string smtpClient = ConfigurationManager.AppSettings["SMTPClient"];
                string mailAddress = ConfigurationManager.AppSettings["MailAddress"];
                string username = ConfigurationManager.AppSettings["username"];
                string password = ConfigurationManager.AppSettings["password"];
                int port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpClient);

                mail.From = new MailAddress(mailAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                log.InfoFormat("Mail Sent\n\tTo: {0}\n\tSubject: {1}\n\tBody:\n\t\t{2}", toAddress, subject, body);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
