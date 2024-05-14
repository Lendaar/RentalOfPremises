﻿using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Enums;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.Repositories.Contracts;
using RentalOfPremises.Repositories.Contracts.Interface;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Contracts.Interface;
using RentalOfPremises.Services.Contracts.Models;
using RentalOfPremises.Services.Contracts.RequestModels;

namespace RentalOfPremises.Services.Implementations
{
    public class UserService : IUserService, IServiceAnchor
    {
        private readonly IUserReadRepository userReadRepository;
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUserReadRepository userReadRepository,
            IMapper mapper,
            IUserWriteRepository userWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.userReadRepository = userReadRepository;
            this.userWriteRepository = userWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<UserModel>> IUserService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await userReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<UserModel>>(result);
        }

        async Task<UserModel?> IUserService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await userReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new Exception();
            }
            return mapper.Map<UserModel>(item);
        }

        async Task<UserModel> IUserService.AddAsync(UserRequestModel user, CancellationToken cancellationToken)
        {
            var item = new User
            {
                Id = Guid.NewGuid(),
                LoginUser = user.LoginUser,
                PasswordUser = user.PasswordUser,
                RoleUser = (RoleTypes)user.RoleUser,
            };
            userWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserModel>(item);
        }

        async Task<UserModel> IUserService.EditAsync(UserRequestModel source, CancellationToken cancellationToken)
        {
            var targetUser = await userReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetUser == null)
            {
                throw new Exception();
            }

            targetUser.LoginUser = source.LoginUser;
            targetUser.PasswordUser = source.PasswordUser;
            targetUser.RoleUser = (RoleTypes)source.RoleUser;

            userWriteRepository.Update(targetUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserModel>(targetUser);
        }

        async Task IUserService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetUser = await userReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetUser == null)
            {
                throw new Exception();
            }
            if (targetUser.DeletedAt.HasValue)
            {
                throw new Exception();
            }
            userWriteRepository.Delete(targetUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}