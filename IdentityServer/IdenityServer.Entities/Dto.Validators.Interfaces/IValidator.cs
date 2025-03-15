namespace IdentityServer.Entities.Dto.Validators.Interfaces;

public interface IValidator<T>
{
    void Validate(T entity);
}