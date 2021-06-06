using AfterMe.Core.Domains.Accounts;
using AfterMe.Core.Domains.Accounts.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AfterMe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        AccountService accountService;
        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Account account)
        {
            var registeredAccount = await accountService.CreateAsync(account, account.PasswordHash);
            return Ok(registeredAccount);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                var accountId = identity.FindFirst("userId").Value;

                var accountInfo = await accountService.FindByIdAsync(accountId);
                return Ok(accountInfo);

            }

            return NotFound();
        }
    }
}
