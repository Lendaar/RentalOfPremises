using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Services.Contracts.Interface;

namespace RentalOfPremises.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthorizationController(ITokenService tokenService, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        [HttpPost("SignIn")]
        public async Task<IActionResult> Auth(string login, string password, CancellationToken cancellationToken)
        {
            var result = await tokenService.Authorization(login, password, cancellationToken);
            return Ok(mapper.Map<TokenResponse>(result));
        }

        [HttpGet, Authorize(Roles = "SeniorEmployee")]
        public async Task<IActionResult> qwe(CancellationToken cancellationToken)
        {
            var login = User?.Identity?.Name;
            return Ok(login);
        }
    }
}
