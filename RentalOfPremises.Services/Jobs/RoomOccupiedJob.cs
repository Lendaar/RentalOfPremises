using Microsoft.Extensions.DependencyInjection;
using Quartz;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Services.Jobs
{
    public class RoomOccupiedJob : IJob
    {
        private readonly IContractReadRepository contractReadRepository;
        private readonly IContractWriteRepository contractWriteRepository;
        private readonly IRoomWriteRepository roomWriteRepository;
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RoomOccupiedJob(
            IContractReadRepository contractReadRepository,
            IContractWriteRepository contractWriteRepository,
            IRoomReadRepository roomReadRepository,
            IRoomWriteRepository roomWriteRepository,
            IUnitOfWork unitOfWork,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.contractReadRepository = contractReadRepository;
            this.contractWriteRepository = contractWriteRepository;
            this.roomReadRepository = roomReadRepository;
            this.roomWriteRepository = roomWriteRepository;
            this.unitOfWork = unitOfWork;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            ///occupied = false при истечении договора
            var contracts = await contractReadRepository.GetEndContractAsync(CancellationToken.None);
            var roomId = contracts.Select(x => x.RoomId).Distinct();
            var rooms = await roomReadRepository.GetByIdsAsync(roomId, CancellationToken.None);
            foreach (var room in rooms)
            {
                room.Value.Occupied = false;
                roomWriteRepository.Update(room.Value);
            }

            //Archive = true договора
            foreach (var contract in contracts)
            {
                contract.Archive = true;
                contractWriteRepository.Update(contract);
            }
            await unitOfWork.SaveChangesAsync(CancellationToken.None);
        }
    }
}
