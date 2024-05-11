using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Common.Entity.Repositories;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

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
                .OrderBy(x => x.DateStart)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Contract?> IContractReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Contract>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
