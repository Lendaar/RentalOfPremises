using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Enums;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Repositories.Implementations
{
    public class UserReadRepository : IUserReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public UserReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<User>> IUserReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .OrderBy(x => x.RoleUser)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<User?> IUserReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<User>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, User>> IUserReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
             => reader.Read<User>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.RoleUser)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IUserReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<User>()
                 .NotDeletedAt()
                 .ById(id)
                 .AnyAsync(cancellationToken);

        Task<User?> IUserReadRepository.GetByLoginAsync(string login, CancellationToken cancellationToken)
             => reader.Read<User>()
                 .NotDeletedAt()
                 .Where(x => x.LoginUser == login)
                 .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<User>> IUserReadRepository.GetAllAdministratorsAsync(CancellationToken cancellationToken)
             => reader.Read<User>()
                 .NotDeletedAt()
                 .Where(x => x.RoleUser == RoleTypes.Administrator)
                 .ToReadOnlyCollectionAsync(cancellationToken);

        Task<bool> IUserReadRepository.AnyByLoginAsync(string login, CancellationToken cancellationToken)
             => reader.Read<User>()
                 .NotDeletedAt()
                 .AnyAsync(x => x.LoginUser == login, cancellationToken);

        bool IUserReadRepository.AnyByLoginForChange(Guid id, string login)
             => reader.Read<User>()
                 .NotDeletedAt()
                 .Any(x => x.LoginUser == login && x.Id != id);
    }
}
