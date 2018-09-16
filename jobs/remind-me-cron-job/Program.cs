using RestSharp;
using RestSharp.Extensions;

namespace remind_me_cron_job
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJyZW1pbmQtbWUga3ViZXJuZXRlcyBzdWNjaW5jdGx5IiwiaWF0IjoxNTM3MDYxNjk5LCJleHAiOjE2MDAyMjAyMzMsImF1ZCI6InJlbWluZC1tZSBhcGkiLCJzdWIiOiJhcHBAcmVtaW5kLW1lLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFsbCJ9.J9cnEpSXqd9eQpVnpbbUtgkI-ZJSICyOUgTv2Z_6trk";
            //var client = new RestClient(args[0]);
            var client = new RestClient(args[0]);
            var request = new RestRequest("/api/Reminder/all", Method.GET);
            request.AddHeader("Authorization", string.Format("bearer {0}", token));
            var response = client.Execute(request);
            var reminders = response.Content;
        }
    }
}
