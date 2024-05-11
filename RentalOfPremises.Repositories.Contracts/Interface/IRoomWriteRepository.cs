using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Room"/>
    /// </summary>
    public interface IRoomWriteRepository : IRepositoryWriter<Room>
    {
    }
}
