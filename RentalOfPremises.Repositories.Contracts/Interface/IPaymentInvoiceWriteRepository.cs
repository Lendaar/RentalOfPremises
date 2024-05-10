using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="PaymentInvoice"/>
    /// </summary>

    public interface IPaymentInvoiceWriteRepository : IRepositoryWriter<PaymentInvoice>
    {
    }
}
