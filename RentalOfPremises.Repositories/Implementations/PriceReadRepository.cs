using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Common.Entity.Repositories;

namespace RentalOfPremises.Repositories.Implementations
{
    public class PriceReadRepository : IPriceReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public PriceReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Price>> IPriceReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Price>()
                .NotDeletedAt()
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Price?> IPriceReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Price>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Price>> IPriceReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Price>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.CreatedAt)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPriceReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Price>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
