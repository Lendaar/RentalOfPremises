using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface IContractService
    {
        /// <summary>
        /// Получить список всех <see cref="ContractModel"/>
        /// </summary>
        Task<IEnumerable<ContractModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ContractModel"/> по идентификатору
        /// </summary>
        Task<ContractModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="ContractModel"/>
        /// </summary>
        Task<ContractModel> AddAsync(ContractRequestModel contract, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="ContractModel"/>
        /// </summary>
        Task<ContractModel> EditAsync(ContractRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="ContractModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
