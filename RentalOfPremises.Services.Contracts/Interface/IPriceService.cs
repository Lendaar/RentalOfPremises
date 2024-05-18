using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface IPriceService
    {
        /// <summary>
        /// Получить список всех <see cref="PriceModel"/>
        /// </summary>
        Task<IEnumerable<PriceModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PriceModel"/> по идентификатору
        /// </summary>
        Task<PriceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="PriceModel"/>
        /// </summary>
        Task<PriceModel> AddAsync(PriceRequestModel course, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="PriceModel"/>
        /// </summary>
        Task<PriceModel> EditAsync(PriceRequestModel source, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="PriceModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, string login, CancellationToken cancellationToken);
    }
}
