using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pointwise.API.Admin.DTO;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.API.Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly AppSettings appSetings;

        public AuthController(IAuthService authService, IOptions<AppSettings> appSetings)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this.appSetings = appSetings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthDto auth)
        {
            var key = Encoding.ASCII.GetBytes(this.appSetings.Secret);
            var user = this.authService.Authenticate(auth.UserName, auth.Password, key);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }
    }
}