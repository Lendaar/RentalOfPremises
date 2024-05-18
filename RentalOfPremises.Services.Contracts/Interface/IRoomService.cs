using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface IRoomService
    {
        /// <summary>
        /// Получить список всех <see cref="RoomModel"/>
        /// </summary>
        Task<IEnumerable<RoomModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="RoomModel"/> по идентификатору
        /// </summary>
        Task<RoomModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="RoomModel"/>
        /// </summary>
        Task<RoomModel> AddAsync(RoomRequestModel course, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="RoomModel"/>
        /// </summary>
        Task<RoomModel> EditAsync(RoomRequestModel source, string login, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="RoomModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, string login, CancellationToken cancellationToken);
    }
}
