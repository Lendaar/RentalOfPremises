﻿using AutoMapper;
using AutoMapper.Execution;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;
using System.Xml;

namespace RentalOfPremises.Services.Implementations
{
    public class PaymentInvoiceService : IPaymentInvoiceService, IServiceAnchor
    {
        private readonly IPaymentInvoiceReadRepository paymentInvoiceReadRepository;
        private readonly IPaymentInvoiceWriteRepository paymentInvoiceWriteRepository;
        private readonly IPriceReadRepository priceReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PaymentInvoiceService(IPaymentInvoiceReadRepository paymentInvoiceReadRepository,
            IPriceReadRepository priceReadRepository,
            IPaymentInvoiceWriteRepository paymentInvoiceWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.paymentInvoiceReadRepository = paymentInvoiceReadRepository;
            this.priceReadRepository = priceReadRepository;
            this.paymentInvoiceWriteRepository = paymentInvoiceWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PaymentInvoiceModel>> IPaymentInvoiceService.GetAllAsync(CancellationToken cancellationToken)
        {
            var payments = await paymentInvoiceReadRepository.GetAllAsync(cancellationToken);
            var pricetId = payments.Select(x => x.PriceId).Distinct();
            var prices = await priceReadRepository.GetByIdsAsync(pricetId, cancellationToken);

            var listPaymentsModel = new List<PaymentInvoiceModel>();
            foreach (var paymentItem in payments)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!prices.TryGetValue(paymentItem.PriceId, out var price))
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
                throw new Exception();
            }
            var price = await priceReadRepository.GetByIdAsync(item.PriceId, cancellationToken);
            var payment = mapper.Map<PaymentInvoiceModel>(item);
            payment.Price = mapper.Map<PriceModel>(price);
            return payment;
        }

        async Task<PaymentInvoiceModel> IPaymentInvoiceService.AddAsync(PaymentInvoiceRequestModel payment, CancellationToken cancellationToken)
        {
            var payments = await paymentInvoiceReadRepository.GetAllAsync(cancellationToken);

            var item = new PaymentInvoice
            {
                Id = Guid.NewGuid(),
                NumberContract = payment.NumberContract,
                PeriodPayment = payment.PeriodPayment,
                Electricity = payment.Electricity,
                WaterPl = payment.WaterPl,
                WaterMi = payment.WaterMi,
                PassPerson = payment.PassPerson,
                PassLegСar = payment.PassLegСar,
                PriceId = payment.Price
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
                throw new Exception();
            }

            targetPaymentItem.PeriodPayment = source.PeriodPayment;
            targetPaymentItem.Electricity = source.Electricity;
            targetPaymentItem.WaterPl = source.WaterPl;
            targetPaymentItem.WaterMi = source.WaterMi;
            targetPaymentItem.PassPerson = source.PassPerson;
            targetPaymentItem.PassLegСar = source.PassLegСar;

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
                throw new Exception();
            }
            if (targetPaymentItem.DeletedAt.HasValue)
            {
                throw new Exception();
            }
            paymentInvoiceWriteRepository.Delete(targetPaymentItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}