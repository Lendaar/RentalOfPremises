﻿using Microsoft.EntityFrameworkCore;
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
                .OrderBy(x => x.NumberContract)
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
                .OrderBy(x => x.NumberContract)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPaymentInvoiceReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<PaymentInvoice>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}