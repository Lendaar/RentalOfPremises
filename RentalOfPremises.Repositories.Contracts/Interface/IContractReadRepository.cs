using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Contract"/>
    /// </summary>
    public interface IContractReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Contract"/>
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Contract"/> по идентификатору
        /// </summary>
        Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Contract"/> по относящихся к одному договору
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetOneContractAsync(int number, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Contract"/> у которых сегодня истек срок
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetEndContractAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить номер последнего <see cref="Contract"/>
        /// </summary>
        Task<int?> GetMaxNumberAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Contract"/> по идентификаторам
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Contract"/> по идентификаторам <see cref="Room"/>
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetByIdRoomsAsync(Guid idRooms, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Contract"/> по идентификаторам <see cref="Tenant"/>
        /// </summary>
        Task<IReadOnlyCollection<Contract>> GetByIdTenantsAsync(Guid idTenants, CancellationToken cancellation);
    }
}
