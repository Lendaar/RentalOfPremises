using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Repositories.Implementations
{
    public class TenantReadRepository : ITenantReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public TenantReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Tenant>> ITenantReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Tenant>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Tenant?> ITenantReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Tenant>> ITenantReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Tenant>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> ITenantReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
