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

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным inn
        /// </summary>
        Task<bool> AnyByInnAsync(string inn, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным inn, кроме своего
        /// </summary>
        bool AnyByInnForChange(Guid id, string inn);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным okpo
        /// </summary>
        Task<bool> AnyByOkpoAsync(string okpo, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным okpo, кроме своего
        /// </summary>
        bool AnyByOkpoForChange(Guid id, string okpo);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным ogrn
        /// </summary>
        Task<bool> AnyByOgrnAsync(string ogrn, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным ogrn, кроме своего
        /// </summary>
        bool AnyByOgrnForChange(Guid id, string ogrn);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным telephone
        /// </summary>
        Task<bool> AnyByTelephoneAsync(string telephone, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть ли <see cref="Tenant"/> с указанным telephone, кроме своего
        /// </summary>
        bool AnyByTelephoneForChange(Guid id, string telephone);
    }
}
