using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Repositories.Implementations;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Implementations
{
    public class ContractService : IContractService, IServiceAnchor
    {
        private readonly IContractReadRepository contractReadRepository;
        private readonly IContractWriteRepository contractWriteRepository;
        private readonly ITenantReadRepository tenantReadRepository;
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IRoomWriteRepository roomWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ContractService(IContractReadRepository contractReadRepository,
            ITenantReadRepository tenantReadRepository,
            IRoomReadRepository roomReadRepository,
            IContractWriteRepository contractWriteRepository,
            IRoomWriteRepository roomWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.contractReadRepository = contractReadRepository;
            this.tenantReadRepository = tenantReadRepository;
            this.roomReadRepository = roomReadRepository;
            this.roomWriteRepository = roomWriteRepository;
            this.contractWriteRepository = contractWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<ContractModel>> IContractService.GetAllAsync(CancellationToken cancellationToken)
        {
            var contracts = await contractReadRepository.GetAllAsync(cancellationToken);
            var tenantId = contracts.Select(x => x.TenantId).Distinct();
            var roomId = contracts.Select(x => x.RoomId).Distinct();

            var rooms = await roomReadRepository.GetByIdsAsync(roomId, cancellationToken);
            var tetants = await tenantReadRepository.GetByIdsAsync(tenantId, cancellationToken);

            var listContractModel = new List<ContractModel>();
            foreach (var contractItem in contracts)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!rooms.TryGetValue(contractItem.RoomId, out var room))
                {
                    continue;
                }
                if (!tetants.TryGetValue(contractItem.TenantId, out var tenant))
                {
                    continue;
                }

                var contract = mapper.Map<ContractModel>(contractItem);

                contract.Tenant = mapper.Map<TenantModel>(tenant);
                contract.Room = mapper.Map<RoomModel>(room);
                listContractModel.Add(contract);
            }
            return listContractModel;
        }

        async Task<ContractModel?> IContractService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await contractReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Contract>(id);
            }
            var room = await roomReadRepository.GetByIdAsync(item.RoomId, cancellationToken);
            var tenant = await tenantReadRepository.GetByIdAsync(item.TenantId, cancellationToken);

            var contract = mapper.Map<ContractModel>(item);

            contract.Room = mapper.Map<RoomModel>(room);
            contract.Tenant = mapper.Map<TenantModel>(tenant);
            return contract;
        }

        async Task<ContractModel> IContractService.AddAsync(ContractRequestModel contract, CancellationToken cancellationToken)
        {
            var item = new Contract
            {
                Id = Guid.NewGuid(),
                Number = contract.Number,
                Payment = contract.Payment,
                TenantId = contract.Tenant,
                RoomId = contract.Room,
                DateStart = contract.DateStart,
                DateEnd = contract.DateEnd,
                Archive = false
            };

            var room = await roomReadRepository.GetByIdAsync(contract.Room, cancellationToken);
            room.Occupied = true;
            roomWriteRepository.Update(room);

            contractWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ContractModel>(item);
        }

        async Task<ContractModel> IContractService.EditAsync(ContractRequestModel source, CancellationToken cancellationToken)
        {
            var targetContractItem = await contractReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetContractItem == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Contract>(source.Id);
            }

            targetContractItem.Payment = source.Payment;
            targetContractItem.DateStart = source.DateStart;
            targetContractItem.DateEnd = source.DateEnd;
            targetContractItem.Archive = source.Archive;

            var tenant = await tenantReadRepository.GetByIdAsync(source.Tenant, cancellationToken);
            targetContractItem.TenantId = tenant!.Id;
            targetContractItem.Tenant = tenant;

            var room = await roomReadRepository.GetByIdAsync(source.Room, cancellationToken);
            targetContractItem.RoomId = room!.Id;
            targetContractItem.Room = room;

            contractWriteRepository.Update(targetContractItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ContractModel>(targetContractItem);
        }

        async Task IContractService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetContractItem = await contractReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetContractItem == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Contract>(id);
            }
            contractWriteRepository.Delete(targetContractItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
