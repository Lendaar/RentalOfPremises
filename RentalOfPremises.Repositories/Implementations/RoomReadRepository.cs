using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Repositories.Implementations
{
    public class RoomReadRepository : IRoomReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public RoomReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Room>> IRoomReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Room>()
               .NotDeletedAt()
               .OrderBy(x => x.Liter)
               .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Room?> IRoomReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Room>()
               .NotDeletedAt()
               .ById(id)
               .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Room>> IRoomReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Room>()
               .NotDeletedAt()
               .ByIds(ids)
               .OrderBy(x => x.Liter)
               .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IRoomReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Room>()
               .NotDeletedAt()
               .ById(id)
               .AnyAsync(cancellationToken);
    }
}
