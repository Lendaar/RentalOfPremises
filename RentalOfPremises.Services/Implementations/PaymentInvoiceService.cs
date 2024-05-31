using AutoMapper;
using AutoMapper.Execution;
using DinkToPdf;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Repositories.Implementations;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Enums;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;
using RentalOfPremises.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace RentalOfPremises.Services.Implementations
{
    public class PaymentInvoiceService : IPaymentInvoiceService, IServiceAnchor
    {
        private readonly IPaymentInvoiceReadRepository paymentInvoiceReadRepository;
        private readonly IPaymentInvoiceWriteRepository paymentInvoiceWriteRepository;
        private readonly IPriceReadRepository priceReadRepository;
        private readonly ITenantReadRepository tenantReadRepository;
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IContractReadRepository contractReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PaymentInvoiceService(IPaymentInvoiceReadRepository paymentInvoiceReadRepository,
            IPriceReadRepository priceReadRepository,
            IPaymentInvoiceWriteRepository paymentInvoiceWriteRepository,
            IContractReadRepository contractReadRepository,
            ITenantReadRepository tenantReadRepository,
            IRoomReadRepository roomReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.paymentInvoiceReadRepository = paymentInvoiceReadRepository;
            this.priceReadRepository = priceReadRepository;
            this.paymentInvoiceWriteRepository = paymentInvoiceWriteRepository;
            this.contractReadRepository = contractReadRepository;
            this.tenantReadRepository = tenantReadRepository;
            this.roomReadRepository = roomReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PaymentInvoiceModel>> IPaymentInvoiceService.GetAllAsync(CancellationToken cancellationToken)
        {
            var payments = await paymentInvoiceReadRepository.GetAllAsync(cancellationToken);
            var pricetId = payments.Select(x => x.PriceId).Distinct();
            var prices = await priceReadRepository.GetByIdsAsync(pricetId, cancellationToken);

            List<Contract> contractsInPayments = new List<Contract>();
            foreach (var payment in payments)
            {
                var contract = await contractReadRepository.GetOneContractAsync(payment.NumberContract, cancellationToken);
                contractsInPayments.AddRange(contract.ToList());
            }
            var contractId = contractsInPayments.Where(x => x.DeletedAt == null).Select(x => x.Number).Distinct();
            var contracts = await contractReadRepository.GetByIdsAsync(contractId, cancellationToken);

            var listPaymentsModel = new List<PaymentInvoiceModel>();
            foreach (var paymentItem in payments)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!prices.TryGetValue(paymentItem.PriceId, out var price))
                {
                    continue;
                }
                if (!contracts.Any(x => x.Number == paymentItem.NumberContract))
                {
                    continue;
                }

                var payment = mapper.Map<PaymentInvoiceModel>(paymentItem);

                payment.Price = mapper.Map<PriceModel>(price);
                listPaymentsModel.Add(payment);
            }
            return listPaymentsModel;
        }

        async Task<PaymentInvoiceModel?> IPaymentInvoiceService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await paymentInvoiceReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<PaymentInvoice>(id);
            }
            var price = await priceReadRepository.GetByIdAsync(item.PriceId, cancellationToken);
            var payment = mapper.Map<PaymentInvoiceModel>(item);
            payment.Price = mapper.Map<PriceModel>(price);
            return payment;
        }

        async Task<PaymentInvoiceModel> IPaymentInvoiceService.AddAsync(PaymentInvoiceRequestModel payment, CancellationToken cancellationToken)
        {
            var valid = await paymentInvoiceReadRepository.AnyByNumberContractAndPeriodAsync(payment.NumberContract, payment.PeriodPayment, cancellationToken);
            if (valid != null)
            {
                throw new RentalOfPremisesInvalidOperationException("Счет для оплаты к договору с этим номером и периодом оплаты уже существует");
            }

            var maxNumber = await paymentInvoiceReadRepository.GetMaxNumberAsync(cancellationToken);
            if (maxNumber == null)
            {
                maxNumber = 1;
            }
            else
            {
                maxNumber++;
            }

            var price = await priceReadRepository.GetAllAsync(cancellationToken);
            if (price.Count == 0)
            {
                throw new RentalOfPremisesInvalidOperationException("Отсутствует прейскурант!");
            }

            var item = new PaymentInvoice
            {
                Id = Guid.NewGuid(),
                Number = (int)(maxNumber),
                NumberContract = payment.NumberContract,
                PeriodPayment = payment.PeriodPayment,
                Electricity = payment.Electricity,
                WaterPl = payment.WaterPl,
                WaterMi = payment.WaterMi,
                PassPerson = payment.PassPerson,
                PassLegСar = payment.PassLegСar,
                PassGrСar = payment.PassGrСar,
                PriceId = price.Last().Id
            };

            paymentInvoiceWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PaymentInvoiceModel>(item);
        }

        async Task<PaymentInvoiceModel> IPaymentInvoiceService.EditAsync(PaymentInvoiceRequestModel source, CancellationToken cancellationToken)
        {
            var targetPaymentItem = await paymentInvoiceReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetPaymentItem == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<PaymentInvoice>(source.Id);
            }

            targetPaymentItem.PeriodPayment = source.PeriodPayment;
            targetPaymentItem.Electricity = source.Electricity;
            targetPaymentItem.WaterPl = source.WaterPl;
            targetPaymentItem.WaterMi = source.WaterMi;
            targetPaymentItem.PassPerson = source.PassPerson;
            targetPaymentItem.PassLegСar = source.PassLegСar;
            targetPaymentItem.PassGrСar = source.PassGrСar;

            var price = await priceReadRepository.GetByIdAsync(source.Price, cancellationToken);
            targetPaymentItem.PriceId = price!.Id;
            targetPaymentItem.Price = price;

            paymentInvoiceWriteRepository.Update(targetPaymentItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PaymentInvoiceModel>(targetPaymentItem);
        }

        async Task IPaymentInvoiceService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPaymentItem = await paymentInvoiceReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPaymentItem == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<PaymentInvoice>(id);
            }
            paymentInvoiceWriteRepository.Delete(targetPaymentItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<HtmlToPdfDocument> IPaymentInvoiceService.GetPaymentInvoiceAsync(string path, int id, CancellationToken cancellationToken)
        {
            var payment = await paymentInvoiceReadRepository.GetPaymentInvoiceByNumberAsync(id, cancellationToken);
            var price = await priceReadRepository.GetByIdAsync(payment.PriceId, cancellationToken);

            var contracts = await contractReadRepository.GetOneContractAsync(payment.NumberContract, cancellationToken);
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

            using (StreamReader reader = new StreamReader(path + "/Payment_Shablon.html"))
            {
                var text = await reader.ReadToEndAsync();

                var local = listContractModel[0].DateStart.LocalDateTime;
                var razn = payment.PeriodPayment - local.Month;
                var year = razn < 0 ? local.Year + 1 : local.Year;

                text = text.Replace("%number%", payment.Number.ToString());
                text = text.Replace("%date_create%", payment.CreatedAt.Date.ToShortDateString());

                var type_tenant = listContractModel[0].Tenant.Type == 0 ? "Юридическое лицо" : "Индивидуальный преприниматель";
                text = text.Replace("%type_tenant%", type_tenant);
                text = text.Replace("%title_tenant%", $"{listContractModel[0].Tenant.Title}");
                text = text.Replace("%inn_tenant%", listContractModel[0].Tenant.Inn);
                text = text.Replace("%kpp_tenant%", listContractModel[0].Tenant.Type == 0 ? $" КПП {listContractModel[0].Tenant.Kpp}," : string.Empty);
                text = text.Replace("%address_tenant%", listContractModel[0].Tenant.Address);

                text = text.Replace("%number_contract%", payment.NumberContract.ToString());
                text = text.Replace("%date_create_contract%", listContractModel[0].DateStart.Date.ToShortDateString());
                text = text.Replace("%period%", GetElementsFromEnum.PerevodDescription((Months)payment.PeriodPayment)
                    + " " + year + " г.");

                var summa = 0.0m;
                text = text.Replace("%electricity%", payment.Electricity.ToString());
                text = text.Replace("%electricity_price%", price.Electricity.ToString());
                text = text.Replace("%electricity_summa%", (payment.Electricity * price.Electricity).ToString());
                summa += payment.Electricity * price.Electricity;

                text = text.Replace("%waterPl%", payment.WaterPl.ToString());
                text = text.Replace("%waterPl_price%", price.WaterPl.ToString());
                text = text.Replace("%waterPl_summa%", (payment.WaterPl * price.WaterPl).ToString());
                summa += payment.WaterPl * price.WaterPl;

                text = text.Replace("%waterMi%", payment.WaterMi.ToString());
                text = text.Replace("%waterMi_price%", price.WaterMi.ToString());
                text = text.Replace("%waterMi_summa%", (payment.WaterMi * price.WaterMi).ToString());
                summa += payment.WaterMi * price.WaterMi;

                text = text.Replace("%passPerson%", payment.PassPerson.ToString());
                text = text.Replace("%passPerson_price%", price.PassPerson.ToString());
                text = text.Replace("%passPerson_summa%", (payment.PassPerson * price.PassPerson).ToString());
                summa += payment.PassPerson * price.PassPerson;

                text = text.Replace("%passLegСar%", payment.PassLegСar.ToString());
                text = text.Replace("%passLegСar_price%", price.PassLegСar.ToString());
                text = text.Replace("%passLegСar_summa%", (payment.PassLegСar * price.PassLegСar).ToString());
                summa += Convert.ToDecimal(payment.PassLegСar * price.PassLegСar);

                text = text.Replace("%passGrСar%", payment.PassGrСar.ToString());
                text = text.Replace("%passGrСar_price%", price.PassGrСar.ToString());
                text = text.Replace("%passGrСar_summa%", (payment.PassGrСar * price.PassGrСar).ToString());
                summa += Convert.ToDecimal(payment.PassGrСar * price.PassGrСar);

                var strInTable = string.Empty;
                using (StreamReader streamReader = new StreamReader(path + "/str_table.html"))
                {
                    strInTable = await streamReader.ReadToEndAsync();
                }
                var allStringsWithRooms = string.Empty;
                var position = 7;

                foreach (var contractModel in listContractModel)
                {
                    var newString = strInTable;
                    newString = newString.Replace("%position%", position.ToString());
                    newString = newString.Replace("%square_room%", contractModel.Room.SquareRoom.ToString());
                    newString = newString.Replace("%payment%", contractModel.Payment.ToString());

                    var newYear = razn < 0 ? 1 : 0;
                    newString = newString.Replace("%period_start%", local.AddMonths(razn - 1).AddYears(newYear).ToShortDateString());
                    newString = newString.Replace("%period_end%", local.AddMonths(razn).AddYears(newYear).ToShortDateString());

                    var sumPayment = contractModel.Payment * Convert.ToDecimal(contractModel.Room.SquareRoom);
                    newString = newString.Replace("%summa%", sumPayment.ToString());
                    allStringsWithRooms += newString;
                    summa += sumPayment;
                    position++;
                }
                text = text.Replace("%all_summa%", summa.ToString() + " рублей");
                text = text.Replace("%rooms%", allStringsWithRooms);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = $"Счет на оплату №{id}",
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
