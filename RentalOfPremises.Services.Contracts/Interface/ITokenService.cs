namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface ITokenService
    {
        Task<string> Authorization(string login, string password, CancellationToken cancellationToken);
    }
}
