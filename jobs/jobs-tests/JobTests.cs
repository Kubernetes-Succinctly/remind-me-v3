using Moq;
using remind_me_cron_job;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace jobs_tests
{
    public class JobTests
    {
        private static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, T responseObject)
            where T : new()
        {
            var response = new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(responseObject);
            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient.Setup(x => x.Execute<T>(It.IsAny<IRestRequest>())).Returns(response.Object);
            return mockIRestClient.Object;
        }

        [Fact]
        public void OnGettingRemindersMailShouldBeSent()
        {
            var reminders = new List<Reminder>
            {
                new Reminder {DueDate = DateTime.Today, Id = "1", Title = "Test"}
            };
            var restClient = MockRestClient(HttpStatusCode.OK, reminders);
            var smtpClient = new FakeSmtpClient();
            var bootstrapper = new Bootstrapper(restClient, smtpClient, string.Empty);
            bootstrapper.Start(null);
            Assert.True(smtpClient.MailSent);
        }
    }
}