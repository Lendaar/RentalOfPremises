using RentalOfPremises.Services.Contracts.Enums;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Получить список всех <see cref="UserModel"/>
        /// </summary>
        Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="UserModel"/> по идентификатору
        /// </summary>
        Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="UserModel"/>
        /// </summary>
        Task<UserModel> AddAsync(UserRequestModel course, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="UserModel"/>
        /// </summary>
        Task<UserModel> EditAsync(UserRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="UserModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
