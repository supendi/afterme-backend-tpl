using AfterMe.Core.Domains.Accounts.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Accounts
{
    public interface ITokenService
    {
        string GetAccessToken(Account account);
        string GetRefreshToken();
        string GetClaimValue(string accessToken, string claimKey, string secretKey);
        bool Verify(string password, string hashedPassword);
    }

    /// <summary>
    /// Provides the authentication functionalities 
    /// </summary>
    public class AuthService : SignInManager<Account>
    {
        public AuthService(UserManager<Account> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Account> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<Account>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<Account> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        /// <summary>
        /// SignIn or Login (Authentication)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenInfo> Authenticate(LoginRequest loginRequest)
        {
            //try by email first
            Account account = await this.UserManager.FindByEmailAsync(loginRequest.Username);
            if (account is null)
            {
                //try by username
                account = await this.UserManager.FindByNameAsync(loginRequest.Username);
                bool accountIsExist = account != null;
                if (!accountIsExist)
                {
                    throw new ApplicationException("Username/Email or password is incorrect.");
                }
            }
           
            var signInResult = await PasswordSignInAsync(account.UserName, loginRequest.Password, true, false);
            if (!signInResult.Succeeded)
            {
                throw new ApplicationException("Username/Email or password is incorrect.");
            }

            TokenInfo tokenInfo = await GetTokenInfo(account);

            return tokenInfo;
        }

        public Task<TokenInfo> GetTokenInfo(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = "rahasiapanjanag banaget deh pokoknya";//Environment.GetEnvironmentVariable("AfterMe_JWTSecret");
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", account.Id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);

            TokenInfo tokenInfo = new TokenInfo
            {
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid().ToString()
            };

            return Task.FromResult(tokenInfo);
        }
    }
}
