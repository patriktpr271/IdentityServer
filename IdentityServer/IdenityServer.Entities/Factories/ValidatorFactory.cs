using IdentityServer.Common.Exceptions;
using IdentityServer.Entities.Dto.Validators;
using IdentityServer.Entities.Dto.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Entities.Factories;


public class ValidatorFactory(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IValidator<T> GetValidator<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();

    }

}