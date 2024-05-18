using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.Room;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работу с Room
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public RoomController(IRoomService roomService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.roomService = roomService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Room
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<RoomResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await roomService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<RoomResponse>>(result));
        }

        /// <summary>
        /// Получить Room по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(RoomResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await roomService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(item));
        }

        /// <summary>
        /// Создаёт новую Room
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(RoomResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var roomRequestModel = mapper.Map<RoomRequestModel>(request);
            var result = await roomService.AddAsync(roomRequestModel, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся Room
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(RoomResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(RoomRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<RoomRequestModel>(request);
            var result = await roomService.EditAsync(model, User?.Identity?.Name, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся Room
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(RoomResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await roomService.DeleteAsync(id, User?.Identity?.Name, cancellationToken);
            return Ok();
        }
    }
}
