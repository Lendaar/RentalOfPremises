using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Services.Contracts.Interface;

namespace RentalOfPremises.Services.Implementations
{
    public class AuthorizationService : ITokenService, IServiceAnchor
    {
        private readonly IUserReadRepository userReadRepository;

        public AuthorizationService(IUserReadRepository userReadRepository)
        {
            this.userReadRepository = userReadRepository;
        }

        async Task<string> ITokenService.Authorization(string login, string password, CancellationToken cancellationToken)
        {
            var user = await userReadRepository.AnyByLoginAsync(login, cancellationToken);
            if (user == null)
            {
                throw new RentalOfPremisesInvalidOperationException("USER_NOT_FOUND");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.LoginUser),
                new Claim(ClaimTypes.Role, user.RoleUser.ToString())
            };
            var accessToken = GenerateAccessToken(claims);
            return accessToken;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = Authorazation.GetSymmetricSecurityKey();
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                 issuer: Authorazation.ISSUER,
                 audience: Authorazation.AUDIENCE,
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(Authorazation.LIFETIME),
                 signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}
