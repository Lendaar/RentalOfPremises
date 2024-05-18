using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.Price;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Price
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Price")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService priceService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public PriceController(IPriceService priceService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.priceService = priceService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Price
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<PriceResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await priceService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PriceResponse>>(result));
        }

        /// <summary>
        /// Получить Price по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(PriceResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await priceService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PriceResponse>(item));
        }

        /// <summary>
        /// Создаёт новую Price
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(PriceResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreatePriceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var priceRequestModel = mapper.Map<PriceRequestModel>(request);
            var result = await priceService.AddAsync(priceRequestModel, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<PriceResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся Price
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(PriceResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(PriceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<PriceRequestModel>(request);
            var result = await priceService.EditAsync(model, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<PriceResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся Price
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(PriceResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await priceService.DeleteAsync(id, User?.Identity?.Name, cancellationToken);
            return Ok();
        }
    }
}
