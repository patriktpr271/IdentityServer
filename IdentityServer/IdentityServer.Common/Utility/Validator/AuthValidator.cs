using IdentityServer.Common.Exceptions;

namespace IdentityServer.Common.Utility.Validator;

public class AuthValidator
{
    public static void ThrowIfInvalidPassowrd(bool valid, string message)
    {
        if (!valid)
        {
            throw new InvalidPasswordException(message);
        }
    }
}