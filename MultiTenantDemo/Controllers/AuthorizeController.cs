using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MultiTenantDemo.Data;
using MultiTenantDemo.Helpers;
using MultiTenantDemo.Model;

namespace MultiTenantDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private readonly IRepository _mRepo;
        private static HttpContext _httpContext;

        public AuthorizeController(IRepository repo, IHttpContextAccessor httpContentAccessor)
        {
            _mRepo = repo;
            _httpContext = httpContentAccessor.HttpContext;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]UserViewModel user)
        {
            var validated_user = _mRepo.GetUserByUsernameAndPassword(user);

            if (validated_user == null)
            {
                return BadRequest(new { status = "400", error = "Bad Request", message = "Invalid request parameters" });
            }

            var response = GenerateToken(user.Username);

            validated_user.Token = response.AccessToken;

            return Ok(response);
        }

        private static TokenResponse GenerateToken(string username)
        {
            var stringToken = Constants.SymmetricSecurityKey;

            if (string.IsNullOrWhiteSpace(stringToken))
            {
                throw new Exception("SecurityKey not found");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(stringToken));

            var slug = _httpContext.Request.Headers[Constants.TenantId].ToString();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.PrimaryGroupSid, slug),
                new Claim(JwtRegisteredClaimNames.Exp,
                    string.Format("{0}",new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds())),
                new Claim(JwtRegisteredClaimNames.Nbf, string.Format("{0}",new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()))
            };

            var expiryTime = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiryTime,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new TokenResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiryTime
            };
        }

        private class TokenResponse
        {
            public string AccessToken { get; set; }
            public DateTime ExpiresAt { get; set; }
        }

    }


}
