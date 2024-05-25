using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace RentalOfPremises.Repositories.Implementations
{
    public class ContractReadRepository : IContractReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public ContractReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Contract>> IContractReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Contract>()
                .NotDeletedAt()
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Contract?> IContractReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Contract>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<Contract>> IContractReadRepository.GetOneContractAsync(int number, CancellationToken cancellationToken)
              => reader.Read<Contract>()
                .NotDeletedAt()
                .Where(x => x.Number == number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<Contract>> IContractReadRepository.GetEndContractAsync(CancellationToken cancellationToken)
              => reader.Read<Contract>()
                .NotDeletedAt()
                .Where(x => x.DateEnd <= DateTimeOffset.Now)
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<int?> IContractReadRepository.GetMaxNumberAsync(CancellationToken cancellationToken)
              => reader.Read<Contract>()
                .NotDeletedAt()
                .MaxAsync(x => (int?)x.Number);
    }
}
