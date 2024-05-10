using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Tenant"/>
    /// </summary>
    public interface ITenantReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Tenant"/>
        /// </summary>
        Task<IReadOnlyCollection<Tenant>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Tenant"/> по идентификатору
        /// </summary>
        Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Tenant"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Tenant>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
