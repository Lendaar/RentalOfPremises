using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Price"/>
    /// </summary>
    public interface IPriceReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Price"/>
        /// </summary>
        Task<IReadOnlyCollection<Price>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Price"/> по идентификатору
        /// </summary>
        Task<Price?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Price"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Price>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="Price"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
