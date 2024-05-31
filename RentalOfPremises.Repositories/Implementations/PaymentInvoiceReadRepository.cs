using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Repositories.Implementations
{
    public class PaymentInvoiceReadRepository : IPaymentInvoiceReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public PaymentInvoiceReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<PaymentInvoice>> IPaymentInvoiceReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<PaymentInvoice?> IPaymentInvoiceReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, PaymentInvoice>> IPaymentInvoiceReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.CreatedAt)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPaymentInvoiceReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);

        Task<int?> IPaymentInvoiceReadRepository.GetMaxNumberAsync(CancellationToken cancellationToken)
              => reader.Read<PaymentInvoice>()
                .MaxAsync(x => (int?)x.Number);

        Task<PaymentInvoice> IPaymentInvoiceReadRepository.GetPaymentInvoiceByNumberAsync(int number, CancellationToken cancellationToken)
              => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .Where(x => x.Number == number)
                .FirstAsync(cancellationToken);
    }
}
