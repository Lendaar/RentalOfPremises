using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="PaymentInvoice"/>
    /// </summary>
    public interface IPaymentInvoiceReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="PaymentInvoice"/>
        /// </summary>
        Task<IReadOnlyCollection<PaymentInvoice>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PaymentInvoice"/> по идентификатору
        /// </summary>
        Task<PaymentInvoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="PaymentInvoice"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, PaymentInvoice>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="PaymentInvoice"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить номер последнего <see cref="PaymentInvoice"/>
        /// </summary>
        Task<int?> GetMaxNumberAsync(CancellationToken cancellationToken);
    }
}
