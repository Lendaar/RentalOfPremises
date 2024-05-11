using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="ITenantWriteRepository"/>
    public class TenantWriteRepository : BaseWriteRepository<Tenant>,
        ITenantWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TenantWriteRepository"/>
        /// </summary>
        public TenantWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
