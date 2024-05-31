using FluentValidation;
using RentalOfPremises.Api.ModelsRequest.Price;

namespace RentalOfPremises.Api.Validators.Price
{
    /// <summary>
    /// Валидатор класса <see cref="CreatePriceRequest"/>
    /// </summary>
    public class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreatePriceRequestValidator"/>
        /// </summary>
        public CreatePriceRequestValidator()
        {

            RuleFor(x => x.Electricity)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена на электричество не должно быть пустым или null");

            RuleFor(x => x.WaterPl)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена на водопотребление не должно быть пустым или null");

            RuleFor(x => x.WaterMi)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена на водоотведение не должна быть пустым или null");

            RuleFor(x => x.PassPerson)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена на пропуск на человека не должен быть пустым или null");

            RuleFor(x => x.PassLegСar)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена на пропуск на легковой автомобиль не должен быть пустым или null");

            RuleFor(x => x.PassGrСar)
               .NotNull()
               .NotEmpty()
               .WithMessage("Цена на пропуск на грузовой автомобиль не должен быть пустым или null");
        }
    }
}
