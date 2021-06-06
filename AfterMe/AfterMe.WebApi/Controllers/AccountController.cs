using AfterMe.Core.Accounts;
using AfterMe.Core.Accounts.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
