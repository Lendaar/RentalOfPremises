using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Services.Contracts.Interface;

namespace RentalOfPremises.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public AuthorizationController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        [HttpPost("in")]
        public async Task<string> Auth(string login, string password, CancellationToken cancellationToken)
        {
            var token = await tokenService.Authorization(login, password, cancellationToken);
            return token;
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        [HttpPost("ex")]
        public async void QQQQ(CancellationToken cancellationToken)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        [HttpGet, Authorize(Roles = "SeniorEmployee")]
        public async Task<IActionResult> qwe(CancellationToken cancellationToken)
        {
            var login = User?.Identity?.Name;
            return Ok(login);
        }
    }
}
