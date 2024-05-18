using FluentValidation;
using RentalOfPremises.Api.ModelsRequest.User;
using RentalOfPremises.Repositories.Contracts.Interface;

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
        public UserRequestValidator(IUserReadRepository userReadRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");

            RuleFor(x => x.LoginUser)
                .NotNull()
                .NotEmpty()
                .WithMessage("Логин не должно быть пустым или null")
                .Must((x, _) =>
                {
                    var userExists = userReadRepository.AnyByLoginForChange(x.Id, x.LoginUser);
                    return !userExists;
                })
                .WithMessage("Такой логин уже используется")
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
