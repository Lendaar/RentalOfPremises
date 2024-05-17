﻿using AutoMapper;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Enums;
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
                throw new RentalOfPremisesEntityNotFoundException<User>(id);
            }
            return mapper.Map<UserModel>(item);
        }

        async Task<UserModel> IUserService.AddAsync(UserRequestModel user, CancellationToken cancellationToken)
        {
            var item = new User
            {
                Id = Guid.NewGuid(),
                LoginUser = user.LoginUser,
                PasswordUser = BCrypt.Net.BCrypt.HashPassword(user.PasswordUser),
                RoleUser = (RoleTypes)user.RoleUser,
            };
            userWriteRepository.Add(item, item.LoginUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserModel>(item);
        }

        async Task<UserModel> IUserService.EditAsync(UserRequestModel source, CancellationToken cancellationToken)
        {
            var targetUser = await userReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetUser == null)
            {
                throw new RentalOfPremisesEntityNotFoundException<User>(source.Id);
            }

            targetUser.LoginUser = source.LoginUser;
            targetUser.PasswordUser = BCrypt.Net.BCrypt.HashPassword(source.PasswordUser);
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
                throw new RentalOfPremisesEntityNotFoundException<User>(id);
            }
            userWriteRepository.Delete(targetUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<UserModel?> IUserService.GetByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken)
        {
            var result = await userReadRepository.GetByLoginAsync(login, cancellationToken);
            if (result == null)
            {
                throw new RentalOfPremisesInvalidOperationException("USER_NOT_FOUND");
            }
            if (BCrypt.Net.BCrypt.Verify(password, result.PasswordUser))
            {
                return mapper.Map<UserModel>(result);
            }
            throw new RentalOfPremisesInvalidOperationException("USER_NOT_FOUND");
        }
    }
}
