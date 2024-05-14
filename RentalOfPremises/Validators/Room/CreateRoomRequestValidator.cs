using RentalOfPremises.Api.ModelsRequest.Room;
using FluentValidation;

namespace RentalOfPremises.Api.Validators.Room
{
    /// <summary>
    /// Валидатор класса <see cref="CreateRoomRequest"/>
    /// </summary>
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateRoomRequestValidator"/>
        /// </summary>
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.Liter)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("Литер не должна быть пустой или null")
                 .MaximumLength(2)
                 .WithMessage("Литер больше 2 символов");

            RuleFor(x => x.NumberRoom)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер помещения не должно быть пустым или null");

            RuleFor(x => x.SquareRoom)
                .NotNull()
                .NotEmpty()
                .WithMessage("Площадь не должна быть пустой или null");

            RuleFor(x => x.TypeRoom)
                .NotNull()
                .NotEmpty()
                .WithMessage("Тип помещения не должен быть пустой или null");
        }
    }
}
