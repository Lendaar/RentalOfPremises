using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;
using Contract = RentalOfPremises.Context.Contracts.Models.Contract;

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

        async Task<string> IContractService.GetContractAsync(string path, int id, CancellationToken cancellationToken)
        {
            var contracts = await contractReadRepository.GetOneContractAsync(id, cancellationToken);
            var roomId = contracts.Select(x => x.RoomId).Distinct();
            var tenantId = contracts.Select(x => x.TenantId).Distinct();
            var rooms = await roomReadRepository.GetByIdsAsync(roomId, cancellationToken);
            var tenants = await tenantReadRepository.GetByIdsAsync(tenantId, cancellationToken);

            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();

                text = text.Replace("%number%", contracts.First().Number.ToString());
                var date = $"{contracts.First().DateStart.Day}.{contracts.First().DateStart.Month}.{contracts.First().DateStart.Year} года";
                text = text.Replace("%date_start%", date);

                var type_tenant = "Индивидуальный преприниматель";
                if (tenants.First().Value.Type == 0)
                {
                    type_tenant = "Юридическое лицо";
                }
                var fio_tenant = $"{tenants.First().Value.Surname} {tenants.First().Value.Name} {tenants.First().Value.Patronymic}";
                text = text.Replace("%type_tenant%", type_tenant);
                text = text.Replace("%fio_tenant%", fio_tenant);

                var room_text = "";
                var room_price = "";
                foreach (var room in rooms)
                {
                    var type = "Cобственное нежилое помещение";
                    if (room.Value.TypeRoom == 0)
                    {
                        type = "Временное бытовое помещение";
                    }
                    room_text += $"{type} литер {room.Value.Liter} - {room.Value.SquareRoom} м2; ";
                }
                text = text.Replace("%rooms%", room_text);

                var date_end = $"{contracts.First().DateEnd.Day}.{contracts.First().DateEnd.Month}.{contracts.First().DateEnd.Year} года.";
                text = text.Replace("%date_end%", date_end);

                decimal summa = 0;
                foreach (var contract in contracts)
                {
                    summa += contract.Payment * Convert.ToDecimal(rooms[contract.RoomId].SquareRoom);
                    room_price += $"Стоимость 1м2 литер {rooms[contract.RoomId].Liter} в месяц {contract.Payment} рублей;\n";
                }

                text = text.Replace("%all_summa%", summa.ToString() + " рублей");
                text = text.Replace("%all_rooms_prices%", room_price);

                return text;
            }
        }
    }
}
