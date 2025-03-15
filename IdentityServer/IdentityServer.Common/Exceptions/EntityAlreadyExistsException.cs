namespace IdentityServer.Common.Exceptions;

public class EntityAlreadyExistsException(Type entityType, string? message) : Exception($"Entity of type {entityType.Name} already exists!\n" + message) {}