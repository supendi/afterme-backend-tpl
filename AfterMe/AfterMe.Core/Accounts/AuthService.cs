using AfterMe.Core.Accounts.Entities;
using System;

namespace AfterMe.Core.Accounts
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
    public class AuthService
    {
        ITokenService tokenService;
        IPasswordHasher passwordHasher;
        IAccountRepository accountRepository;

        public AuthService(ITokenService tokenManager, IPasswordHasher passwordHasher, IAccountRepository accountRepository)
        {
            this.tokenService = tokenManager;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// SignIn or Login (Authentication)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TokenInfo Authenticate(LoginRequest loginRequest)
        {
            Account account = this.accountRepository.GetByEmail(loginRequest.Username);

            bool accountIsExist = account != null;
            if (!accountIsExist)
            {
                throw new ApplicationException("Username/Email or password is incorrect.");
            }

            bool passwordIsCorrect = this.tokenService.Verify(loginRequest.Password, account.Password);
            if (!passwordIsCorrect)
            {
                throw new ApplicationException("Username/Email or password is incorrect.");
            }

            TokenInfo tokenInfo = new TokenInfo
            {
                AccessToken = this.tokenService.GetAccessToken(account),
                RefreshToken = this.tokenService.GetRefreshToken()
            };

            return tokenInfo;
        }
    }
}
