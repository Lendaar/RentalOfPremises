using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Enums;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Implementations
{
    public class RoomService : IRoomService, IServiceAnchor
    {
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IRoomWriteRepository roomWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RoomService(IRoomReadRepository roomReadRepository,
            IMapper mapper,
            IRoomWriteRepository roomWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.roomReadRepository = roomReadRepository;
            this.roomWriteRepository = roomWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<RoomModel>> IRoomService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await roomReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<RoomModel>>(result);
        }

        async Task<RoomModel?> IRoomService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await roomReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new Exception();
            }
            return mapper.Map<RoomModel>(item);
        }

        async Task<RoomModel> IRoomService.AddAsync(RoomRequestModel room, CancellationToken cancellationToken)
        {
            var item = new Room
            {
                Id = Guid.NewGuid(),
                Liter = room.Liter,
                NumberRoom = room.NumberRoom,
                SquareRoom = room.SquareRoom,
                TypeRoom = (PremisesTypes)room.TypeRoom,
                Price = room.Price,
                Occupied = false,
            };
            roomWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RoomModel>(item);
        }

        async Task<RoomModel> IRoomService.EditAsync(RoomRequestModel source, CancellationToken cancellationToken)
        {
            var targetRoom = await roomReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetRoom == null)
            {
                throw new Exception();
            }

            targetRoom.Liter = source.Liter;
            targetRoom.NumberRoom = source.NumberRoom;
            targetRoom.SquareRoom = source.SquareRoom;
            targetRoom.TypeRoom = (PremisesTypes)source.TypeRoom;
            targetRoom.Price = source.Price;
            targetRoom.Occupied = source.Occupied;

            roomWriteRepository.Update(targetRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RoomModel>(targetRoom);
        }

        async Task IRoomService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetRoom = await roomReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetRoom == null)
            {
                throw new Exception();
            }
            if (targetRoom.DeletedAt.HasValue)
            {
                throw new Exception();
            }
            roomWriteRepository.Delete(targetRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
