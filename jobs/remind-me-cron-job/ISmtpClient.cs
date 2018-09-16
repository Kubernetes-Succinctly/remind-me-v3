using System.Net.Mail;

namespace remind_me_cron_job
{
    public interface ISmtpClient
    {
        void Send(MailMessage message);
    }
}