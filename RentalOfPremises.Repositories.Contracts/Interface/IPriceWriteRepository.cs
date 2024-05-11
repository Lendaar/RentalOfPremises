using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Price"/>
    /// </summary>
    public interface IPriceWriteRepository : IRepositoryWriter<Price>
    {
    }
}
