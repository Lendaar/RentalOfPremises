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
    }
}
