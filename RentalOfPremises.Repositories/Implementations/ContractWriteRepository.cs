using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="IContractWriteRepository"/>
    public class ContractWriteRepository : BaseWriteRepository<Contract>,
        IContractWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ContractWriteRepository"/>
        /// </summary>
        public ContractWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
