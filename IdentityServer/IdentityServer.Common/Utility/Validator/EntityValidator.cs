using IdentityServer.Common.Exceptions;

namespace IdentityServer.Common.Utility.Validator;

public static class EntityValidator
{
    public static void ThrowIfNull<T>(T entity)
    {
        if (entity == null)
        {
            throw new EntityNullException(typeof(T));
        }
    }

    public static void ThrowIfAlreadyExist<T>(T entity, string? message = null)
    {
        if (entity != null)
        {
            throw new EntityAlreadyExistsException(typeof(T), message);
        }
    }
}