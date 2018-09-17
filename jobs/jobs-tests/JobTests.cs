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
        private static IRestClient PrepareMockRestClient(HttpStatusCode httpStatusCode, List<Reminder> responseObject)
        {
            var response = new Mock<IRestResponse<List<Reminder>>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(responseObject);
            var tokenResponse = new Mock<IRestResponse<Dictionary<string, string>>>();
            var tokenResponseDictionary = new Dictionary<string, string> { { "token", "testToken" } };
            tokenResponse.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            tokenResponse.Setup(_ => _.Data).Returns(tokenResponseDictionary);
            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient.Setup(x => x.Execute<List<Reminder>>(It.IsAny<IRestRequest>())).Returns(response.Object);
            mockIRestClient.Setup(x => x.Execute<Dictionary<string, string>>(It.IsAny<IRestRequest>())).Returns(tokenResponse.Object);
            return mockIRestClient.Object;
        }

        [Fact]
        public void OnGettingRemindersMailShouldBeSent()
        {
            var reminders = new List<Reminder>
            {
                new Reminder { DueDate = DateTime.Today, Id = "1", Title = "Test" }
            };
            var restClient = PrepareMockRestClient(HttpStatusCode.OK, reminders);
            var smtpClient = new FakeSmtpClient();
            var bootstrapper = new Bootstrapper(restClient, smtpClient);
            bootstrapper.Start(null);
            Assert.True(smtpClient.MailSent);
        }
    }
}