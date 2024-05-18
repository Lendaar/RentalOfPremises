using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Attribute;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Api.Models;
using RentalOfPremises.Api.ModelsRequest.User;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Users
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.userService = userService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех User
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<UserResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await userService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<UserResponse>>(result));
        }

        /// <summary>
        /// Получить User по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(UserResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await userService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<UserResponse>(item));
        }

        /// <summary>
        /// Создаёт новое User
        /// </summary>
        [HttpPost, Authorize]
        [ApiOk(typeof(UserResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateUserRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var userRequestModel = mapper.Map<UserRequestModel>(request);
            var result = await userService.AddAsync(userRequestModel, cancellationToken);
            return Ok(mapper.Map<UserResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся User
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(UserResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(UserRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<UserRequestModel>(request);
            var result = await userService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<UserResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся User
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(UserResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await userService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
