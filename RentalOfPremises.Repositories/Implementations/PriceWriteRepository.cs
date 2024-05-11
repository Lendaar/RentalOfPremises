using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="IPriceWriteRepository"/>
    public class PriceWriteRepository : BaseWriteRepository<Price>,
        IPriceWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PriceWriteRepository"/>
        /// </summary>
        public PriceWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
