﻿using RentalOfPremises.Api.ModelsRequest.PaymentInvoice;
using RentalOfPremises.Repositories.Contracts.Interface;
using FluentValidation;

namespace RentalOfPremises.Api.Validators.PaymentInvoice
{
    /// <summary>
    /// Валидатор класса <see cref="PaymentInvoiceRequest"/>
    /// </summary>
    public class PaymentInvoiceRequestValidator : AbstractValidator<PaymentInvoiceRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="PaymentInvoiceRequestValidator"/>
        /// </summary>
        public PaymentInvoiceRequestValidator(IPriceReadRepository priceReadRepository)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.NumberContract)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер договора не должен быть пустым или null");

            RuleFor(x => x.PeriodPayment)
               .NotNull()
               .NotEmpty()
               .WithMessage("Период не должен быть пустой или null");

            RuleFor(x => x.Electricity)
               .NotNull()
               .NotEmpty()
               .WithMessage("Показания электричества не должны быть пустым или null");

            RuleFor(x => x.WaterPl)
                .NotNull()
                .NotEmpty()
                .WithMessage("Показания воды не должны быть пустым или null");

            RuleFor(x => x.WaterMi)
                .NotNull()
                .NotEmpty()
                .WithMessage("Водоотведение не должно быть пустым или null");

            RuleFor(x => x.PassPerson)
                .NotNull()
                .NotEmpty()
                .WithMessage("Кол-во пропусков на человека не должно быть пустым или null");
        }
    }
}
