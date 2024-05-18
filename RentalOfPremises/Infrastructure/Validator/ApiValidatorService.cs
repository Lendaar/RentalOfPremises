using FluentValidation;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Shared;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Api.Validators.Contract;
using RentalOfPremises.Api.Validators.User;
using RentalOfPremises.Api.Validators.Tenant;
using RentalOfPremises.Api.Validators.Room;
using RentalOfPremises.Api.Validators.PaymentInvoice;
using RentalOfPremises.Api.Validators.Price;

namespace RentalOfPremises.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(
            ITenantReadRepository tenantReadRepository,
            IRoomReadRepository roomReadRepository,
            IPriceReadRepository priceReadRepository,
            IUserReadRepository userReadRepository)
        {
            Register<CreateContractRequestValidator>(tenantReadRepository, roomReadRepository);
            Register<ContractRequestValidator>(tenantReadRepository, roomReadRepository);

            Register<CreateUserRequestValidator>(userReadRepository);
            Register<UserRequestValidator>(userReadRepository);

            Register<CreateTenantRequestValidator>(tenantReadRepository);
            Register<TenantRequestValidator>(tenantReadRepository);

            Register<CreateRoomRequestValidator>();
            Register<RoomRequestValidator>();

            Register<CreatePaymentInvoiceValidator>(priceReadRepository);
            Register<PaymentInvoiceRequestValidator>(priceReadRepository);

            Register<CreatePriceRequestValidator>();
            Register<PriceRequestValidator>();
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new RentalOfPremisesValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
