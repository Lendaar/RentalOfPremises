using RentalOfPremises.Services.Contracts.Models;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface ITokenService
    {
        Task<TokenModel> Authorization(string login, string password, CancellationToken cancellationToken);
    }
}
