using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.Contract;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Contract
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Contract")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService contractService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHost;
        private IConverter converter;

        public ContractController(IContractService contractService, IMapper mapper, IApiValidatorService validatorService, IWebHostEnvironment webHost, IConverter converter)
        {
            this.contractService = contractService;
            this.validatorService = validatorService;
            this.mapper = mapper;
            this.webHost = webHost;
            this.converter = converter;
        }

        /// <summary>
        /// Получить список всех Contract
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<ContractResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await contractService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ContractResponse>>(result));
        }

        /// <summary>
        /// СФормировать договор аренды
        /// </summary>
        [HttpGet("Document")]
        [ApiOk(typeof(IEnumerable<ContractResponse>))]
        public async Task<IActionResult> GetDoc([Required] int id, CancellationToken cancellationToken)
        {
            var path = webHost.WebRootPath + "/Contract_Shablon.html";
            var result = await contractService.GetContractAsync(path, id, cancellationToken);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF_Contract",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = result,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = converter.Convert(pdf);
            return File(file, "application/pdf", $"Договор аренды #{id}.pdf");
        }

        /// <summary>
        /// Получить Contract по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(ContractResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await contractService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<ContractResponse>(item));
        }

        /// <summary>
        /// Создаёт новое Contract
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(ContractResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateContractRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var contractRequestModel = mapper.Map<ContractRequestModel>(request);
            var result = await contractService.AddAsync(contractRequestModel, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<ContractResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся Contract
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(ContractResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(ContractRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<ContractRequestModel>(request);
            var result = await contractService.EditAsync(model, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<ContractResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся Contract
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(ContractResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await contractService.DeleteAsync(id, User?.Identity?.Name, cancellationToken);
            return Ok();
        }
    }
}
