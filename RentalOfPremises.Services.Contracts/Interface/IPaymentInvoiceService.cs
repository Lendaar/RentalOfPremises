using DinkToPdf;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Contracts.Interface
{
    public interface IPaymentInvoiceService
    {
        /// <summary>
        /// Получить список всех <see cref="PaymentInvoiceModel"/>
        /// </summary>
        Task<IEnumerable<PaymentInvoiceModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PaymentInvoiceModel"/> по идентификатору
        /// </summary>
        Task<PaymentInvoiceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="PaymentInvoiceModel"/>
        /// </summary>
        Task<PaymentInvoiceModel> AddAsync(PaymentInvoiceRequestModel paymentInvoice, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="PaymentInvoiceModel"/>
        /// </summary>
        Task<PaymentInvoiceModel> EditAsync(PaymentInvoiceRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="PaymentInvoiceModel"/>
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Формирует документ
        /// </summary>
        Task<HtmlToPdfDocument> GetPaymentInvoiceAsync(string path, int id, CancellationToken cancellationToken);
    }
}
