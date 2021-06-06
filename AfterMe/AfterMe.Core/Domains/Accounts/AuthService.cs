using AfterMe.Core.Domains.Accounts.Entities;
using AfterMe.Core.Domains.Accounts.Libs;
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
        ISecurityTokenHandler securityTokenHandler;
        public AuthService(
            UserManager<Account> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<Account>
            claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<Account>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<Account> confirmation,
            ISecurityTokenHandler securityTokenHandler
            ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            this.securityTokenHandler = securityTokenHandler;
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
            var claimsIdentity = new ClaimsIdentity(new[] { new Claim("userId", account.Id) });

            TokenInfo tokenInfo = new TokenInfo
            {
                AccessToken = securityTokenHandler.GetAccessToken(claimsIdentity),
                RefreshToken = securityTokenHandler.GetRefreshToken()//Guid.NewGuid().ToString()
            };

            return Task.FromResult(tokenInfo);
        }
    }
}
