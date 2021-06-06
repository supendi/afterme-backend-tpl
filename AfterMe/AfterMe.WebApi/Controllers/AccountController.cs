using AfterMe.Core.Domains.Accounts;
using AfterMe.Core.Domains.Accounts.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
