using RestSharp;

namespace remind_me_cron_job
{
    internal class Program
    {
        private static readonly string SMTPHost = "in-v3.mailjet.com";
        private static readonly int SMTPPort = 587;
        private static readonly string SMTPUserName = "e3fb490299e128104a48d914da3d3ed3";
        private static readonly string SMTPPassword = "6ca9f9d34e9a6ebde9e7513fa6a30f12";
        private static readonly string APIUri = "http://localhost:5002";

        private static readonly string Token =
            "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJyZW1pbmQtbWUga3ViZXJuZXRlcyBzdWNjaW5jdGx5IiwiaWF0IjoxNTM3MDYxNjk5LCJleHAiOjE2MDAyMjAyMzMsImF1ZCI6InJlbWluZC1tZSBhcGkiLCJzdWIiOiJhcHBAcmVtaW5kLW1lLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFsbCJ9.J9cnEpSXqd9eQpVnpbbUtgkI-ZJSICyOUgTv2Z_6trk";

        private static void Main(string[] args)
        {
            var restClient = new RestClient(APIUri);
            var smtpClient = new MailjetSmtpClient(SMTPHost, SMTPPort, SMTPUserName, SMTPPassword);
            var bootstrapper = new Bootstrapper(restClient, smtpClient, Token);
            bootstrapper.Start(args);
        }
    }
}