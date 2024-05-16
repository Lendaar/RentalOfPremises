using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Enums;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Exceptions;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Implementations
{
    public class TenantService : ITenantService, IServiceAnchor
    {
        private readonly ITenantReadRepository tenantReadRepository;
        private readonly ITenantWriteRepository tenantWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TenantService(ITenantReadRepository tenantReadRepository,
            IMapper mapper,
            ITenantWriteRepository tenantWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.tenantReadRepository = tenantReadRepository;
            this.tenantWriteRepository = tenantWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TenantModel>> ITenantService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await tenantReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TenantModel>>(result);
        }

        async Task<TenantModel?> ITenantService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await tenantReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Tenant>(id);
            }
            return mapper.Map<TenantModel>(item);
        }

        async Task<TenantModel> ITenantService.AddAsync(TenantRequestModel tenant, CancellationToken cancellationToken)
        {
            var item = new Tenant
            {
                Id = Guid.NewGuid(),
                Title = tenant.Title,
                Name = tenant.Name,
                Surname = tenant.Surname,
                Patronymic = tenant.Patronymic,
                Type = (TenantTypes)tenant.Type,
                Inn = tenant.Inn,
                Kpp = tenant.Kpp,
                Address = tenant.Address,
                Rs = tenant.Rs,
                Ks = tenant.Ks,
                Bik = tenant.Bik,
                Bank = tenant.Bank,
                Okpo = tenant.Okpo,
                Ogrn = tenant.Ogrn,
                Telephone = tenant.Telephone,
                Email = tenant.Email
            };
            tenantWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TenantModel>(item);
        }

        async Task<TenantModel> ITenantService.EditAsync(TenantRequestModel source, CancellationToken cancellationToken)
        {
            var targetTenant = await tenantReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetTenant == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Tenant>(source.Id);
            }

            targetTenant.Title = source.Title;
            targetTenant.Name = source.Name;
            targetTenant.Surname = source.Surname;
            targetTenant.Patronymic = source.Patronymic;
            targetTenant.Type = (TenantTypes)source.Type;
            targetTenant.Inn = source.Inn;
            targetTenant.Kpp = source.Kpp;
            targetTenant.Address = source.Address;
            targetTenant.Rs = source.Rs;
            targetTenant.Ks = source.Ks;
            targetTenant.Bik = source.Bik;
            targetTenant.Bank = source.Bank;
            targetTenant.Okpo = source.Okpo;
            targetTenant.Ogrn = source.Ogrn;
            targetTenant.Telephone = source.Telephone;
            targetTenant.Email = source.Email;

            tenantWriteRepository.Update(targetTenant);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TenantModel>(targetTenant);
        }

        async Task ITenantService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTenant = await tenantReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetTenant == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Tenant>(id);
            }
            tenantWriteRepository.Delete(targetTenant);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
