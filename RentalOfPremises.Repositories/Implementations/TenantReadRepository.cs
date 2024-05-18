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
        Task<bool> ITenantReadRepository.AnyByInnAsync(string inn, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .AnyAsync(x => x.Inn == inn, cancellationToken);
        bool ITenantReadRepository.AnyByInnForChange(Guid id, string inn)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .Any(x => x.Inn == inn && x.Id != id);
        Task<bool> ITenantReadRepository.AnyByOkpoAsync(string okpo, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .AnyAsync(x => x.Okpo == okpo, cancellationToken);
        bool ITenantReadRepository.AnyByOkpoForChange(Guid id, string okpo)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .Any(x => x.Okpo == okpo && x.Id != id);
        Task<bool> ITenantReadRepository.AnyByOgrnAsync(string ogrn, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .AnyAsync(x => x.Ogrn == ogrn, cancellationToken);
        bool ITenantReadRepository.AnyByOgrnForChange(Guid id, string ogrn)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .Any(x => x.Okpo == ogrn && x.Id != id);
        Task<bool> ITenantReadRepository.AnyByTelephoneAsync(string telephone, CancellationToken cancellationToken)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .AnyAsync(x => x.Telephone == telephone, cancellationToken);
        bool ITenantReadRepository.AnyByTelephoneForChange(Guid id, string telephone)
             => reader.Read<Tenant>()
                .NotDeletedAt()
                .Any(x => x.Telephone == telephone && x.Id != id);
    }
}
