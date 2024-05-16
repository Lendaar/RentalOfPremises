using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Enums;
using RentalOfPremises.Api.Infrastructure;
using RentalOfPremises.Services.Contracts.Interface;

namespace RentalOfPremises.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthorizationController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        [HttpPost("in")]
        public async Task<RoleTypes> Auth(string login, string password, CancellationToken cancellationToken)
        {
            var result = await userService.GetByLoginAndPasswordAsync(login, password, cancellationToken);
            if (result != null)
            {
                var usernameClaim = new Claim(ClaimTypes.Name, login);
                var claims = new List<Claim> { usernameClaim };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                ClassAuthorazation.Name = HttpContext.User.Identity.Name;
            }
            return (RoleTypes)result.RoleUser;
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        [HttpPost("ex")]
        public async void QQQQ(CancellationToken cancellationToken)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<string> qwe(string login, string password, CancellationToken cancellationToken)
        {
            return "fdss";
        }
    }
}
