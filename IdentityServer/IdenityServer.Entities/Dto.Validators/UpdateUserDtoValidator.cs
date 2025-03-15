using IdentityServer.Common.Utility.Validator;
using IdentityServer.Entities.Dto.Validators.Interfaces;
using IdentityServer.Entities.Dtos;

namespace IdentityServer.Entities.Dto.Validators;

public class UpdateUserDtoValidator : IValidator<UpdateUserDto>
{
    public void Validate(UpdateUserDto dto)
    {
        if(dto.Email != null) InputValidator.ThrowIfInvalidEmail(dto.Email);

        if (dto.Password != null) InputValidator.ThrowIfNullOrEmpty(dto.Password, nameof(dto.Password));
        
        if(dto.Name != null)  InputValidator.ThrowIfNullOrEmpty(dto.Name, nameof(dto.Name));
    }
}