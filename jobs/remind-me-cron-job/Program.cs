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
            "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL3JlbWluZC1tZS9vYXV0aDIvZGVmYXVsdCIsImlhdCI6MTUzNzA2MTY5OSwiZXhwIjoxNjAwMjIwMjMzLCJhdWQiOiJhcGk6Ly9kZWZhdWx0Iiwic3ViIjoiYXBwQHJlbWluZC1tZS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIn0.DPvwRIbg1wyGXmjX5Al6fsiw2K-k2TWGmPUevQzZv6c";

        private static void Main(string[] args)
        {
            var restClient = new RestClient(APIUri);
            var smtpClient = new MailjetSmtpClient(SMTPHost, SMTPPort, SMTPUserName, SMTPPassword);
            var bootstrapper = new Bootstrapper(restClient, smtpClient, Token);
            bootstrapper.Start(args);
        }
    }
}