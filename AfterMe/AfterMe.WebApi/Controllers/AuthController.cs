using AfterMe.Core.Accounts;
using AfterMe.Core.Accounts.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AfterMe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await authService.Authenticate(loginRequest);
            return Ok(token);
        }
    }
}
