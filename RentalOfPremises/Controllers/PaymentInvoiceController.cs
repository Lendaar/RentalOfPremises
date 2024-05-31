using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.PaymentInvoice;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с PaymentInvoice
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "PaymentInvoice")]
    public class PaymentInvoiceController : ControllerBase
    {
        private readonly IPaymentInvoiceService paymentInvoiceService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public PaymentInvoiceController(IPaymentInvoiceService paymentInvoiceService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.paymentInvoiceService = paymentInvoiceService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех PaymentInvoice
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<PaymentInvoiceResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await paymentInvoiceService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PaymentInvoiceResponse>>(result));
        }

        /// <summary>
        /// Получить Работника по PaymentInvoice
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(PaymentInvoiceResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await paymentInvoiceService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PaymentInvoiceResponse>(item));
        }

        /// <summary>
        /// Создаёт нового PaymentInvoice
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(PaymentInvoiceResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreatePaymentInvoiceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var paymentInvoiceRequestModel = mapper.Map<PaymentInvoiceRequestModel>(request);
            var result = await paymentInvoiceService.AddAsync(paymentInvoiceRequestModel, cancellationToken);
            return Ok(mapper.Map<PaymentInvoiceResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющищегося PaymentInvoice
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(PaymentInvoiceResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(PaymentInvoiceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<PaymentInvoiceRequestModel>(request);
            var result = await paymentInvoiceService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PaymentInvoiceResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийегося PaymentInvoice по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk(typeof(PaymentInvoiceResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await paymentInvoiceService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
