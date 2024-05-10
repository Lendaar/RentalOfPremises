using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="User"/>
    /// </summary>
    public interface IUserWriteRepository : IRepositoryWriter<User>
    {
    }
}
