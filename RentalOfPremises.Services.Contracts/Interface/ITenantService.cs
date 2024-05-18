using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface ITenantService
    {
        /// <summary>
        /// Получить список всех <see cref="TenantModel"/>
        /// </summary>
        Task<IEnumerable<TenantModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TenantModel"/> по идентификатору
        /// </summary>
        Task<TenantModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="TenantModel"/>
        /// </summary>
        Task<TenantModel> AddAsync(TenantRequestModel course, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="TenantModel"/>
        /// </summary>
        Task<TenantModel> EditAsync(TenantRequestModel source, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="TenantModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, string login, CancellationToken cancellationToken);
    }
}
