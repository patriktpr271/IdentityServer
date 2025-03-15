using IdentityServer.Common.Utility.Validator;
using IdentityServer.Entities.Dto.Validators.Interfaces;
using IdentityServer.Entities.Dtos;

namespace IdentityServer.Entities.Dto.Validators;

public class CreateUserDtoValidator : IValidator<CreateUserDto>
{
    public void Validate(CreateUserDto dto)
    {
        InputValidator.ThrowIfNullOrEmpty(dto.Email, nameof(dto.Email));
        InputValidator.ThrowIfNullOrEmpty(dto.Password, nameof(dto.Password));
        InputValidator.ThrowIfNullOrEmpty(dto.Name, nameof(dto.Name));
        InputValidator.ThrowIfInvalidEmail(dto.Email);
    }
}