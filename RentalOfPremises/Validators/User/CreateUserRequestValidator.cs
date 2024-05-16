using System.Text.RegularExpressions;
using FluentValidation;
using RentalOfPremises.Api.ModelsRequest.User;

namespace RentalOfPremises.Api.Validators.User
{
    /// <summary>
    /// Валидатор класса <see cref="CreateUserRequest"/>
    /// </summary>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateUserRequestValidator"/>
        /// </summary>
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.LoginUser)
                .NotNull()
                .NotEmpty()
                .WithMessage("Логин не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Логин больше 50 символов");

            RuleFor(x => x.PasswordUser)
                .Matches(@"[0-9]+")
                .WithMessage("Пароль должен содержать хотя бы одну цифру")
                .Matches(@"[А-ЯA-Z]+")
                .WithMessage("Пароль должен содержать хотя бы одну прописную букву")
                .Matches(@"[а-яa-z]+")
                .WithMessage("Пароль должен содержать хотя бы одну строчную букву")
                .Matches(@"[!@#\$%\^\&*\)\(+=._-]+")
                .WithMessage("Пароль должен содержать хотя бы один спецсимвол")
                .NotNull()
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Пароль больше 50 символов");

            RuleFor(x => x.RoleUser)
                .NotNull()
                .WithMessage("Роль не должен быть null");
        }
    }
}
