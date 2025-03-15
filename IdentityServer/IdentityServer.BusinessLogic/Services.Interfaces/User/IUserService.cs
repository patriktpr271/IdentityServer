namespace IdentityServer.BusinessLogic.Services.Interfaces.User
{
    using IdentityServer.Entities.ApplicationUser;
    using IdentityServer.Entities.Dtos;
    public interface IUserService
    {
        public Task<UserDto> CreateUserAsnyc(CreateUserDto dto, CancellationToken cancellationToken);

        public Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<UserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        public Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken);

        public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);

        public Task<bool> VerifyPassword(UserDto user, string password, CancellationToken cancellationToken);
    }
}

    

