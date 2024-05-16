using FluentValidation;
using RentalOfPremises.Api.ModelsRequest.Contract;
using RentalOfPremises.Repositories.Contracts.Interface;

namespace RentalOfPremises.Api.Validators.Contract
{
    /// <summary>
    /// Валидатор класса <see cref="ContractRequest"/>
    /// </summary>
    public class ContractRequestValidator : AbstractValidator<ContractRequest>
    {
        /// <summary>
        /// Валидатор класса <see cref="ContractRequestValidator"/>
        /// </summary>
        public ContractRequestValidator(ITenantReadRepository tenantReadRepository, IRoomReadRepository roomReadRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер договора не должно быть пустым или null");

            RuleFor(x => x.Payment)
                .NotNull()
                .NotEmpty()
                .WithMessage("Платеж не должно быть пустым или null");

            RuleFor(x => x.DateStart)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дата начала действия договора не должен быть пустым или null");

            RuleFor(x => x.DateEnd)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дата окончания действия договора не должна быть пустой или null");

            RuleFor(x => x.Tenant)
                .NotNull()
                .NotEmpty()
                .WithMessage("Арендатор не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var tenantsExists = await tenantReadRepository.AnyByIdAsync(id, CancellationToken);
                    return tenantsExists;
                })
                .WithMessage("Такого арендатор не существует!");

            RuleFor(x => x.Room)
                .NotNull()
                .NotEmpty()
                .WithMessage("Помещение не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var roomsExists = await roomReadRepository.AnyByIdAsync(id, CancellationToken);
                    return roomsExists;
                })
                .WithMessage("Такого помещение не существует!");
        }
    }
}
