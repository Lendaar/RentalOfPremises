using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
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
    public class PriceService : IPriceService, IServiceAnchor
    {
        private readonly IPriceReadRepository priceReadRepository;
        private readonly IPriceWriteRepository priceWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PriceService(IPriceReadRepository priceReadRepository,
            IMapper mapper,
            IPriceWriteRepository priceWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.priceReadRepository = priceReadRepository;
            this.priceWriteRepository = priceWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PriceModel>> IPriceService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await priceReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PriceModel>>(result);
        }

        async Task<PriceModel?> IPriceService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await priceReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Price>(id);
            }
            return mapper.Map<PriceModel>(item);
        }

        async Task<PriceModel> IPriceService.AddAsync(PriceRequestModel price, string login, CancellationToken cancellationToken)
        {
            var item = new Price
            {
                Id = Guid.NewGuid(),
                Electricity = price.Electricity,
                WaterPl = price.WaterPl,
                WaterMi = price.WaterMi,
                PassPerson = price.PassPerson,
                PassLegСar = price.PassLegСar,
                PassGrСar = price.PassGrСar,
            };
            priceWriteRepository.Add(item, login);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PriceModel>(item);
        }

        async Task<PriceModel> IPriceService.EditAsync(PriceRequestModel source, string login, CancellationToken cancellationToken)
        {
            var targetCourse = await priceReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCourse == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Price>(source.Id);
            }

            targetCourse.Electricity = source.Electricity;
            targetCourse.WaterPl = source.WaterPl;
            targetCourse.WaterMi = source.WaterMi;
            targetCourse.PassPerson = source.PassPerson;
            targetCourse.PassLegСar = source.PassLegСar;
            targetCourse.PassGrСar = source.PassGrСar;

            priceWriteRepository.Update(targetCourse, login);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PriceModel>(targetCourse);
        }

        async Task IPriceService.DeleteAsync(Guid id, string login, CancellationToken cancellationToken)
        {
            var targetCourse = await priceReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetCourse == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<Price>(id);
            }
            priceWriteRepository.Delete(targetCourse, login);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
