using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories.Implementations
{
    /// <inheritdoc cref="IRoomWriteRepository"/>
    public class RoomWriteRepository : BaseWriteRepository<Room>,
        IRoomWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoomWriteRepository"/>
        /// </summary>
        public RoomWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
