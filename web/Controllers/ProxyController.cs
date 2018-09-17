using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

public class ProxyController : Controller
{
    public IConfiguration Configuration { get; set; }

    private RestClient client;

    public ProxyController(IConfiguration config)
    {
        this.Configuration = config;
        this.client = new RestClient(Configuration["apiUrl"]);
    }
    public IActionResult SaveReminder([FromBody] Reminder reminder)
    {
        // Get auth token.
        var token = this.GetJwtToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var request = new RestRequest("/api/Reminder", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddParameter("application/json", JsonConvert.SerializeObject(reminder), ParameterType.RequestBody);
            var response = this.client.Execute(request);
            return StatusCode((int) response.StatusCode);
        }

        return Unauthorized();

    }
    public IActionResult GetAllReminders()
    {
        var token = this.GetJwtToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var request = new RestRequest("/api/Reminder/all", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            var response = this.client.Execute(request);
            return Ok(response.Content);
        }

        return Unauthorized();
    }
    public IActionResult DeleteReminder([FromRoute] string id)
    {
        var token = this.GetJwtToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var request = new RestRequest("/api/Reminder/{reminderId}", Method.DELETE);
            request.AddUrlSegment("reminderId", id);
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            var response = this.client.Execute(request);
            return Ok(response.Content);
        }

        return Unauthorized();
    }

    private string GetJwtToken()
    {
        var request = new RestRequest("/Authentication/GetToken", Method.POST);
        request.AddParameter(
            "application/json",
            JsonConvert.SerializeObject(
                new TokenRequest { Username = "System", Password = "WellKnownPassword" }),
            ParameterType.RequestBody);
        var tokenResponse = client.Execute<Dictionary<string, string>>(request);
        if (null != tokenResponse.Data)
        {
            return tokenResponse.Data["token"];
        }

        return string.Empty;
    }
}