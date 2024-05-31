using RentalOfPremises.Api.ModelsRequest.PaymentInvoice;
using RentalOfPremises.Repositories.Contracts.Interface;
using FluentValidation;

namespace RentalOfPremises.Api.Validators.PaymentInvoice
{
    /// <summary>
    /// Валидатор класса <see cref="CreatePaymentInvoiceRequest"/>
    /// </summary>
    public class CreatePaymentInvoiceValidator : AbstractValidator<CreatePaymentInvoiceRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreatePaymentInvoiceValidator"/>
        /// </summary>
        public CreatePaymentInvoiceValidator(IPriceReadRepository priceReadRepository)
        {
            RuleFor(x => x.NumberContract)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер договора не должен быть пустым или null");

            RuleFor(x => x.PeriodPayment)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null");

            RuleFor(x => x.Electricity)
               .NotNull()
               .NotEmpty()
               .WithMessage("Потребление электричества не должно быть пустым или null");

            RuleFor(x => x.WaterPl)
                .NotNull()
                .NotEmpty()
                .WithMessage("Потребление воды не должно быть пустым или null");

            RuleFor(x => x.WaterMi)
                .NotNull()
                .NotEmpty()
                .WithMessage("Водоотведение не должна быть пустым или null");

            RuleFor(x => x.PassPerson)
                .NotNull()
                .NotEmpty()
                .WithMessage("Кол-во пропусков на человека не должен быть пустым или null");

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Прейскурант не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var pricesExists = await priceReadRepository.AnyByIdAsync(id, CancellationToken);
                    return pricesExists;
                })
                .WithMessage("Такого прейскуранта не существует!");
        }
    }
}
