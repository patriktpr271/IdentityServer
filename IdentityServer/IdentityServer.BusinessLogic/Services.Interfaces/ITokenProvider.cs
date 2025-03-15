namespace IdentityServer.BusinessLogic.Services.Interfaces
{
    public interface ITokenProvider
    {
        public string Create(IdentityServer.Entities.Dtos.UserDto user);
    }
}
