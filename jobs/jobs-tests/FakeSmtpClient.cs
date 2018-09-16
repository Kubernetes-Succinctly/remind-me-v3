using remind_me_cron_job;
using System.Net.Mail;

namespace jobs_tests
{
    public class FakeSmtpClient : ISmtpClient
    {
        public FakeSmtpClient() => this.MailSent = false;

        public bool MailSent { get; set; }

        public void Send(MailMessage message)
        {
            this.MailSent = true;
        }
    }
}