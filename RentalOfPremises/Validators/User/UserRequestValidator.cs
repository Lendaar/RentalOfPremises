using RentalOfPremises.Api.ModelsRequest.User;
using FluentValidation;

namespace RentalOfPremises.Api.Validators.User
{
    /// <summary>
    /// Валидатор класса <see cref="UserRequest"/>
    /// </summary>
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="UserRequestValidator"/>
        /// </summary>
        public UserRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");

            RuleFor(x => x.LoginUser)
                .NotNull()
                .NotEmpty()
                .WithMessage("Логин не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Логин больше 50 символов");

            RuleFor(x => x.PasswordUser)
                .NotNull()
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Пароль больше 10 символов");

            RuleFor(x => x.RoleUser)
                .NotNull()
                .WithMessage("Роль не должен быть null");
        }
    }
}
