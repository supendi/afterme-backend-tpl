using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AfterMe.Core.Domains.Accounts.Libs
{
    public interface ISecurityTokenHandler
    {
        string GetAccessToken(ClaimsIdentity claimsIdentity);
        string GetRefreshToken();
    }

    public class SecurityTokenHandler : ISecurityTokenHandler
    {
        string secretKey = "rahasiapanjanag banaget deh pokoknya"; // Environment.GetEnvironmentVariable("AfterMe_JWTSecret");

        public string GetAccessToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer= "http://localhost:8000/"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);

            var result = tokenHandler.ReadToken(accessToken);

            return accessToken;
        }

        public string GetRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
