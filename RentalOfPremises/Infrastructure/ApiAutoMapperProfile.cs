using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using RentalOfPremises.Api.Models;
using PremisesTypes_Services = RentalOfPremises.Services.Contracts.Enums.PremisesTypes;
using PremisesTypes_Api = RentalOfPremises.Api.Enums.PremisesTypes;
using RoleTypes_Services = RentalOfPremises.Services.Contracts.Enums.RoleTypes;
using RoleTypes_Api = RentalOfPremises.Api.Enums.RoleTypes;
using TenantTypes_Services = RentalOfPremises.Services.Contracts.Enums.TenantTypes;
using TenantTypes_Api = RentalOfPremises.Api.Enums.TenantTypes;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Api.ModelsRequest.User;
using RentalOfPremises.Services.Contracts.RequestModels;
using RentalOfPremises.Api.ModelsRequest.Tenant;
using RentalOfPremises.Api.ModelsRequest.Room;
using RentalOfPremises.Api.ModelsRequest.Price;
using RentalOfPremises.Api.ModelsRequest.PaymentInvoice;
using RentalOfPremises.Api.ModelsRequest.Contract;

namespace RentalOfPremises.Api.Infrastructure
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<UserModel, UserResponse>(MemberList.Destination);
            CreateMap<CreateUserRequest, UserRequestModel>(MemberList.Destination);
            CreateMap<UserRequest, UserRequestModel>(MemberList.Destination);

            CreateMap<TenantModel, TenantResponse>(MemberList.Destination);
            CreateMap<CreateTenantRequest, TenantRequestModel>(MemberList.Destination);
            CreateMap<TenantRequest, TenantRequestModel>(MemberList.Destination);

            CreateMap<RoomModel, RoomResponse>(MemberList.Destination);
            CreateMap<CreateRoomRequest, RoomRequestModel>(MemberList.Destination);
            CreateMap<RoomRequest, RoomRequestModel>(MemberList.Destination);

            CreateMap<PriceModel, PriceResponse>(MemberList.Destination);
            CreateMap<CreatePriceRequest, PriceRequestModel>(MemberList.Destination);
            CreateMap<PriceRequest, PriceRequestModel>(MemberList.Destination);

            CreateMap<PaymentInvoiceModel, PaymentInvoiceResponse>(MemberList.Destination);
            CreateMap<CreatePaymentInvoiceRequest, PaymentInvoiceRequestModel>(MemberList.Destination);
            CreateMap<PaymentInvoiceRequest, PaymentInvoiceRequestModel>(MemberList.Destination);

            CreateMap<ContractModel, ContractResponse>(MemberList.Destination)
                .ForMember(x => x.TenantTitle, opt => opt.MapFrom(x => x.Tenant.Title))
                .ForMember(x => x.TenantInn, opt => opt.MapFrom(x => x.Tenant.Inn))
                .ForMember(x => x.RoomLiter, opt => opt.MapFrom(x => x.Room.Liter))
                .ForMember(x => x.RoomNumber, opt => opt.MapFrom(x => x.Room.NumberRoom));
            CreateMap<CreateContractRequest, ContractRequestModel>(MemberList.Destination);
            CreateMap<ContractRequest, ContractRequestModel>(MemberList.Destination);

            CreateMap<PremisesTypes_Services, PremisesTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<RoleTypes_Services, RoleTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<TenantTypes_Services, TenantTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
        }
    }
}
