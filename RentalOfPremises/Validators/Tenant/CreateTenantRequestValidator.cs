using FluentValidation;
using RentalOfPremises.Api.ModelsRequest.Tenant;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Api.Validators.Tenant
{
    /// <summary>
    /// Валидатор класса <see cref="CreateTenantRequest"/>
    /// </summary>
    public class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateTenantRequestValidator"/>
        /// </summary>
        public CreateTenantRequestValidator(ITenantReadRepository tenantReadRepository)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Наименование организации не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Наименование организации больше 50 символов");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя директора не должен быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Имя директора больше 50 символов");

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия директора не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Фамилия директора больше 50 символов");

            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Тип организации не должен быть null");

            RuleFor(x => x.Inn)
                .NotNull()
                .NotEmpty()
                .WithMessage("ИНН не должно быть пустым или null")
                .MustAsync(async (inn, CancellationToken) =>
                {
                    var tenantExists = await tenantReadRepository.AnyByInnAsync(inn, CancellationToken);
                    return !tenantExists;
                })
                .WithMessage("ИНН не уникален")
                .MaximumLength(12)
                .WithMessage("ИНН больше 12 символов");

            RuleFor(x => x.Kpp)
                .MaximumLength(9)
                .WithMessage("КПП больше 9 символов");

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Юридический адрес организации не должно быть пустым или null")
                .MaximumLength(150)
                .WithMessage("Юридический адрес организации больше 150 символов");

            RuleFor(x => x.Rs)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер расчётного счёта не должен быть пустым или null")
                .MaximumLength(20)
                .WithMessage("Номер расчётного счёта больше 20 символов");

            RuleFor(x => x.Ks)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер корреспондентского счёта не должен быть пустым или null")
                .MaximumLength(20)
                .WithMessage("Номер корреспондентского счёта больше 20 символов");

            RuleFor(x => x.Bik)
                .NotNull()
                .NotEmpty()
                .WithMessage("БИК не должно быть пустым или null")
                .MaximumLength(9)
                .WithMessage("БИК больше 9 символов");

            RuleFor(x => x.Bank)
                .NotNull()
                .NotEmpty()
                .WithMessage("Полное наименование банка не должно быть пустым или null")
                .MaximumLength(150)
                .WithMessage("Полное наименование банка больше 150 символов");

            RuleFor(x => x.Okpo)
                .NotNull()
                .NotEmpty()
                .WithMessage("ОКПО не должен быть пустым или null")
                .MustAsync(async (okpo, CancellationToken) =>
                {
                    var tenantExists = await tenantReadRepository.AnyByOkpoAsync(okpo, CancellationToken);
                    return !tenantExists;
                })
                .WithMessage("ОКПО не уникален")
                .MaximumLength(10)
                .WithMessage("ОКПО больше 10 символов");

            RuleFor(x => x.Ogrn)
                .NotNull()
                .NotEmpty()
                .WithMessage("ОГРН не должно быть пустым или null")
                .MustAsync(async (ogrn, CancellationToken) =>
                {
                    var tenantExists = await tenantReadRepository.AnyByOgrnAsync(ogrn, CancellationToken);
                    return !tenantExists;
                })
                .WithMessage("ОГРН не уникален")
                .MaximumLength(15)
                .WithMessage("ОГРН больше 15 символов");

            RuleFor(x => x.Telephone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон не должно быть пустым или null")
                .MustAsync(async (telephone, CancellationToken) =>
                {
                    var tenantExists = await tenantReadRepository.AnyByTelephoneAsync(telephone, CancellationToken);
                    return !tenantExists;
                })
                .WithMessage("Телефон не уникален")
                .MaximumLength(30)
                .WithMessage("Телефон больше 30 символов");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Электронная почта не должен быть пустым или null")
                .EmailAddress()
                .WithMessage("Требуется действительная почта!");
        }
    }
}
