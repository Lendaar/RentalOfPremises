using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="IUserWriteRepository"/>
    public class UserWriteRepository : BaseWriteRepository<User>,
        IUserWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserWriteRepository"/>
        /// </summary>
        public UserWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
