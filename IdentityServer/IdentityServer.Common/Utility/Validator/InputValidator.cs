using IdentityServer.Common.Exceptions;

namespace IdentityServer.Common.Utility.Validator;

public static class InputValidator
{
    public static void ThrowIfNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidInputException($"{parameterName} cannot be null or empty.");
        }
    }

    public static void ThrowIfInvalidEmail(string email)
    {
        //basic way to check if the e-mail "looks" like an e-mail
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new InvalidInputException("Invalid email format.");
        }
    }
}