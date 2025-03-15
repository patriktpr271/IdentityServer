namespace IdentityServer.Common.Exceptions;

public class EntityNullException(Type entityType) : Exception($"Entity of type {entityType.Name} cannot be null") {}