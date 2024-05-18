using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.Tenant;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Tenant
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Tenant")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService tenantService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TenantController"/>
        /// </summary>
        public TenantController(ITenantService tenantService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.tenantService = tenantService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Tenant
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<TenantResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await tenantService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<TenantResponse>>(result));
        }

        /// <summary>
        /// Получить Tenant по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(TenantResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await tenantService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<TenantResponse>(item));
        }

        /// <summary>
        /// Создаёт новый Tenant
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(TenantResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var tenantRequestModel = mapper.Map<TenantRequestModel>(request);
            var result = await tenantService.AddAsync(tenantRequestModel, cancellationToken);
            return Ok(mapper.Map<TenantResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся Tenant
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(TenantResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(TenantRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<TenantRequestModel>(request);
            var result = await tenantService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TenantResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся Tenant
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(TenantResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await tenantService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
