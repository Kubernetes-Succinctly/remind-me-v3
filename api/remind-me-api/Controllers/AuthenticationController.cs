using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using remind_me_api.Models;

namespace remind_me_api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public ActionResult RequestToken([FromBody] TokenRequest request)
        {
            // This is a mock identity service implementation for Demo only.
            if (request.Username == "System" && request.Password == "WellKnownPassword")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secureDemoEncryptionKey"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    "remind-me://oauth2/default",
                    "api://default",
                    new[] {new Claim(ClaimTypes.Role, "Application")},
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);
                return this.Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return this.BadRequest("Authentication failed.");
        }
    }
}