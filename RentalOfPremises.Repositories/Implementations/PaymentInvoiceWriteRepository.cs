using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="IPaymentInvoiceWriteRepository"/>
    public class PaymentInvoiceWriteRepository : BaseWriteRepository<PaymentInvoice>,
        IPaymentInvoiceWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PaymentInvoiceWriteRepository"/>
        /// </summary>
        public PaymentInvoiceWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
