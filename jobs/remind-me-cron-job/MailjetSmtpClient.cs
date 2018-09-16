using System.Net;
using System.Net.Mail;

namespace remind_me_cron_job
{
    public class MailjetSmtpClient : ISmtpClient
    {
        private readonly string smtpHost;
        private readonly string smtpPassword;
        private readonly int smtpPort;
        private readonly string smtpUserName;

        public MailjetSmtpClient(string smtpHost, int smtpPort, string smtpUserName, string smtpPassword)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.smtpPassword = smtpPassword;
            this.smtpUserName = smtpUserName;
        }

        public void Send(MailMessage message)
        {
            using (var client = new SmtpClient(this.smtpHost, this.smtpPort))
            {
                client.Credentials = new NetworkCredential(this.smtpUserName, this.smtpPassword);
                client.EnableSsl = true;
                client.Send(message);
            }
        }
    }
}