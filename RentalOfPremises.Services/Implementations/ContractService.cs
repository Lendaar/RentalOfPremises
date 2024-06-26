﻿using AutoMapper;
using DinkToPdf;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
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

            var room = await roomReadRepository.GetByIdAsync(targetContractItem.RoomId, cancellationToken);
            if (targetContractItem == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Room>(targetContractItem.RoomId);
            }
            room.Occupied = false;
            roomWriteRepository.Update(room);
            contractWriteRepository.Delete(targetContractItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<int> IContractService.GetMaxNumberAsync(CancellationToken cancellationToken)
        {
            var maxNumber = await contractReadRepository.GetMaxNumberAsync(cancellationToken);
            if (maxNumber == null)
            {
                maxNumber = 1;
            }
            else
            {
                maxNumber++;
            }
            return (int)maxNumber;
        }

        async Task<HtmlToPdfDocument> IContractService.GetContractAsync(string path, int id, CancellationToken cancellationToken)
        {
            var contracts = await contractReadRepository.GetOneContractAsync(id, cancellationToken);
            var roomId = contracts.Select(x => x.RoomId).Distinct();
            var tenantId = contracts.Select(x => x.TenantId).Distinct();
            var rooms = await roomReadRepository.GetByIdsAsync(roomId, cancellationToken);
            var tenants = await tenantReadRepository.GetByIdsAsync(tenantId, cancellationToken);

            var listContractModel = new List<ContractModel>();
            foreach (var contractItem in contracts)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!rooms.TryGetValue(contractItem.RoomId, out var room))
                {
                    continue;
                }
                if (!tenants.TryGetValue(contractItem.TenantId, out var tenant))
                {
                    continue;
                }

                var contract = mapper.Map<ContractModel>(contractItem);
                contract.Tenant = mapper.Map<TenantModel>(tenant);
                contract.Room = mapper.Map<RoomModel>(room);
                listContractModel.Add(contract);
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();

                text = text.Replace("%number%", listContractModel[0].Number.ToString());
                var date = $"{listContractModel[0].DateStart.Date.ToShortDateString()} года";
                text = text.Replace("%date_start%", date);

                var type_tenant = listContractModel[0].Tenant.Type == 0 ? "Юридическое лицо" : "Индивидуальный преприниматель";
                var fio_tenant = $"{listContractModel[0].Tenant.Surname} {listContractModel[0].Tenant.Name} {listContractModel[0].Tenant.Patronymic}";
                text = text.Replace("%type_tenant%", type_tenant);
                text = text.Replace("%fio_tenant%", fio_tenant);

                var room_text = "";
                var room_price = "";
                var summa = 0.0m;
                foreach (var contractModel in listContractModel)
                {
                    var type = contractModel.Room.TypeRoom == 0 ? "Временное бытовое помещение" : "Cобственное нежилое помещение";
                    room_text += $"{type} литер {contractModel.Room.Liter} - {contractModel.Room.SquareRoom} м²; ";
                    summa += contractModel.Payment * Convert.ToDecimal(contractModel.Room.SquareRoom);
                    room_price += $"Стоимость 1м² литер {contractModel.Room.Liter} в месяц {contractModel.Payment} рублей;\n";
                }

                text = text.Replace("%rooms%", room_text);
                text = text.Replace("%all_rooms_prices%", room_price);
                text = text.Replace("%all_summa%", summa.ToString() + " рублей");

                var date_end = $"{listContractModel[0].DateEnd.Date.ToShortDateString()} года.";
                text = text.Replace("%date_end%", date_end);

                var requisites = listContractModel[0].Tenant.Inn.ToString();
                text = text.Replace("%inn%", requisites);

                requisites = listContractModel[0].Tenant.Address;
                text = text.Replace("%address%", requisites);

                requisites = listContractModel[0].Tenant.Rs;
                text = text.Replace("%rs%", requisites);

                requisites = listContractModel[0].Tenant.Ks;
                text = text.Replace("%ks%", requisites);

                requisites = listContractModel[0].Tenant.Bik;
                text = text.Replace("%bic%", requisites);

                requisites = listContractModel[0].Tenant.Bank;
                text = text.Replace("%bank%", requisites);

                requisites = listContractModel[0].Tenant.Okpo;
                text = text.Replace("%okpo%", requisites);

                requisites = listContractModel[0].Tenant.Ogrn;
                text = text.Replace("%ogrn%", requisites);

                var fio = listContractModel[0].Tenant.Name[0] + "." + listContractModel[0].Tenant?.Patronymic[0] + ". " + listContractModel[0].Tenant.Surname;
                text = text.Replace("%fio%", fio); ///fsdf

                requisites = listContractModel[0].Tenant.Telephone;
                text = text.Replace("%telephone%", requisites);

                requisites = listContractModel[0].Tenant.Email;
                text = text.Replace("%email%", requisites);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = $"Договор аренды №{id}",
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = text,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                return pdf;
            }
        }
    }
}
