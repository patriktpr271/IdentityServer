using IdentityServer.Common.Exceptions;
using IdentityServer.Common.Utility.Validator;
using IdentityServer.Entities.Dto.Validators.Interfaces;
using IdentityServer.Entities.Factories;

namespace IdentityServer.BusinessLogic.Services.User
{
    using IdentityServer.BusinessLogic.Services.Interfaces;
    using IdentityServer.BusinessLogic.Services.Interfaces.User;
    using IdentityServer.DAL.Repositories.Interfaces;
    using IdentityServer.Entities.ApplicationUser;
    using IdentityServer.Entities.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<CreateUserDto> _createUserValidator;
        private readonly IValidator<UpdateUserDto> _updateUserValidator;

        public UserService(IUserRepository userRepository, PasswordHasher passwordHasher, ValidatorFactory validatorFactory)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _createUserValidator = validatorFactory.GetValidator<CreateUserDto>();
            _updateUserValidator = validatorFactory.GetValidator<UpdateUserDto>();
        }
        public async Task<UserDto> CreateUserAsnyc(CreateUserDto dto, CancellationToken cancellationToken)
        {
            _createUserValidator.Validate(dto);
            
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email,cancellationToken);
            EntityValidator.ThrowIfAlreadyExist(existingUser, $"User with email {dto.Email} already exists!");
            
            var hashedPassword = _passwordHasher.Hash(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = hashedPassword
            };
          
            await _userRepository.CreateAsync(user,cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        { 
            var userToDelete = await _userRepository.GetByIdAsync(id, cancellationToken); 
            EntityValidator.ThrowIfNull(userToDelete);
            await _userRepository.DeleteAsync(userToDelete.Id, cancellationToken);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            InputValidator.ThrowIfNullOrEmpty(email, nameof(email));
            InputValidator.ThrowIfInvalidEmail(email);
            
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);  
            
            EntityValidator.ThrowIfNull(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id,cancellationToken);
            
            EntityValidator.ThrowIfNull(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            EntityValidator.ThrowIfNull(user);
            
            _updateUserValidator.Validate(dto);
            
            user.Name = dto.Name ?? user.Name;
            user.PasswordHash = _passwordHasher.Hash(dto.Password) ?? user.PasswordHash;
            user.Email = dto.Email ?? user.Email;

            var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
            EntityValidator.ThrowIfNull(updatedUser);

            return new UserDto
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                Name = updatedUser.Name
            };

        }

        public async Task<bool> VerifyPassword(UserDto user, string password, CancellationToken cancellationToken)
        {
            var userWithHash = await _userRepository.GetByIdAsync(user.Id, cancellationToken);
            
            InputValidator.ThrowIfNullOrEmpty(password, nameof(password));

            EntityValidator.ThrowIfNull(userWithHash);
            
            bool verification = _passwordHasher.Verify(userWithHash.PasswordHash, password);

            AuthValidator.ThrowIfInvalidPassowrd(verification, $"Invalid pasasword for the e-mail: {user.Email}");
            
            return verification;
        }
    }
}
