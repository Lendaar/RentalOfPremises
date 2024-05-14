using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Services.Contracts.Models;
using PremisesTypes_Contracts = RentalOfPremises.Context.Contracts.Enums.PremisesTypes;
using PremisesTypes_Services = RentalOfPremises.Services.Contracts.Enums.PremisesTypes;
using RoleTypes_Contracts = RentalOfPremises.Context.Contracts.Enums.RoleTypes;
using RoleTypes_Services = RentalOfPremises.Services.Contracts.Enums.RoleTypes;
using TenantTypes_Contracts = RentalOfPremises.Context.Contracts.Enums.TenantTypes;
using TenantTypes_Services = RentalOfPremises.Services.Contracts.Enums.TenantTypes;

namespace RentalOfPremises.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<PaymentInvoice, PaymentInvoiceModel>(MemberList.Destination)
                .ForMember(x => x.Price, next => next.Ignore());

            CreateMap<Tenant, TenantModel>(MemberList.Destination);

            CreateMap<Room, RoomModel>(MemberList.Destination);

            CreateMap<User, UserModel>(MemberList.Destination);

            CreateMap<Price, PriceModel>(MemberList.Destination);

            CreateMap<Contract, ContractModel>(MemberList.Destination)
                .ForMember(x => x.Room, next => next.Ignore())
                .ForMember(x => x.Tenant, next => next.Ignore());

            CreateMap<PremisesTypes_Contracts, PremisesTypes_Services>().ConvertUsingEnumMapping(x => x.MapByName()).ReverseMap();
            CreateMap<RoleTypes_Contracts, RoleTypes_Services>().ConvertUsingEnumMapping(x => x.MapByName()).ReverseMap();
            CreateMap<TenantTypes_Contracts, TenantTypes_Services>().ConvertUsingEnumMapping(x => x.MapByName()).ReverseMap();
        }
    }
}
