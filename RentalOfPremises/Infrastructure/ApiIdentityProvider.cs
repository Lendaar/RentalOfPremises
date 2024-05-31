using System.Security.Claims;
using RentalOfPremises.Common.Entity.InterfaceDB;

namespace RentalOfPremises.Api.Infrastructure
{
    public class ApiIdentityProvider : IIdentityProvider
    {
        private readonly IEnumerable<Claim> claims;

        public ApiIdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            claims = httpContextAccessor?.HttpContext?.User?.Claims ?? Array.Empty<Claim>();
        }

        public string Name => claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Anonymous";

        public Guid Id => Guid.TryParse(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var value) ? value : Guid.Empty;

        public IEnumerable<KeyValuePair<string, string>> Claims => claims.Select(x => new KeyValuePair<string, string>(x.Type, x.Value));
    }
}
